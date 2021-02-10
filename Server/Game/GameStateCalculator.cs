using System;
using System.Collections.Generic;
using System.Linq;
using Melinoe.Server.Game;
using Melinoe.Shared.Evidence;
using Melinoe.Shared.Ghosts;
using Melinoe.Shared.Objectives;
using Melinoe.Shared.Possibilities;

namespace Melinoe.Server.Services
{
    public static class GameStateCalculator
    {
        private const EvidenceType NoEvidence = (EvidenceType)0x0;
        private const EvidenceType AllEvidence = (EvidenceType)0x111111;
        private const Objective NoObjectives = (Objective)0x0;

        public static GameState Default(string gameCode, IEnumerable<GameUser>? users = null) => new()
        {
            GameCode = gameCode,
            Users = users?.ToList() ?? new List<GameUser>(),

            FirstName = null,
            LastName = null,
            Objectives = NoObjectives,

            EvidenceStates = Enum.GetValues<EvidenceType>().ToDictionary(evidence => evidence, _ => EvidenceState.Unsure),
            EvidencePossibilities = Enum.GetValues<EvidenceType>().Select(evidence => new EvidencePossibility(evidence, Possibility.Possible, EvidenceState.Unsure)).ToList(),
            GhostPossibilities = Ghost.AllGhosts.Select(ghost => new GhostPossibility(ghost, Possibility.Possible)).ToList()
        };

        public static GameState UpdateEvidence(GameState gameState, EvidenceType evidence, EvidenceState state)
        {
            var newEvidenceStates = new Dictionary<EvidenceType, EvidenceState>(gameState.EvidenceStates);
            newEvidenceStates[evidence] = state;

            (List<EvidencePossibility> evidencePossibilities, List<GhostPossibility> ghostPossibilities) = GetPossibilities(newEvidenceStates);
            
            return gameState with
            {
                EvidenceStates = newEvidenceStates, EvidencePossibilities = evidencePossibilities, GhostPossibilities = ghostPossibilities
            };
        }

        public static (List<EvidencePossibility>, List<GhostPossibility>) GetPossibilities(Dictionary<EvidenceType, EvidenceState> evidenceStates)
        {
            EvidenceType evidenceHas =
                evidenceStates
                    .Where(kv => kv.Value == EvidenceState.Present)
                    .Select(kv => kv.Key)
                    .Aggregate(NoEvidence, (agg, val) => agg | val);

            EvidenceType evidenceNot =
                evidenceStates
                    .Where(kv => kv.Value == EvidenceState.NotPresent)
                    .Select(kv => kv.Key)
                    .Aggregate(NoEvidence, (agg, val) => agg | val);

            List<Ghost> ghostsPossible = Ghost.AllGhosts.Where(g => (g.RequiredEvidence & evidenceNot) == 0).Where(g => (g.RequiredEvidence & evidenceHas) == evidenceHas).ToList();
            List<Ghost> ghostsNotPossible = Ghost.AllGhosts.Where(g => !ghostsPossible.Contains(g)).ToList();

            EvidenceType evidenceDefinite =
                evidenceStates
                    .Where(kv => kv.Value == EvidenceState.Present)
                    .Select(kv => kv.Key)
                    .Aggregate(NoEvidence, (agg, val) => agg | val);

            evidenceDefinite |= ghostsPossible
                .Select(g => g.RequiredEvidence)
                .Aggregate(AllEvidence, (agg, val) => agg & val);

            EvidenceType possibleEvidence =
                ghostsPossible
                    .Select(g => g.RequiredEvidence)
                    .Aggregate(NoEvidence, (agg, val) => agg | val);
            possibleEvidence &= ~evidenceDefinite;

            List<EvidencePossibility> evidencePossibilities = new();
            foreach (EvidenceType type in Enum.GetValues<EvidenceType>())
            {
                Possibility possibility;
                if ((evidenceDefinite & type) > 0)
                    possibility = Possibility.Definite;
                else if ((possibleEvidence & type) > 0)
                    possibility = Possibility.Possible;
                else
                    possibility = Possibility.NotPossible;
                evidencePossibilities.Add(new EvidencePossibility(type, possibility, evidenceStates[type]));
            }

            var ghostPossibilities = new List<GhostPossibility>();
            switch (ghostsPossible.Count)
            {
                case 0:
                    ghostPossibilities.AddRange(Ghost.AllGhosts.Select(g => new GhostPossibility(g, Possibility.NotPossible)));
                    evidencePossibilities.Clear();
                    evidencePossibilities.AddRange(evidenceStates.Select(kv => new EvidencePossibility(kv.Key, kv.Value == EvidenceState.Present ? Possibility.Definite : kv.Value == EvidenceState.Unsure ? Possibility.Possible : Possibility.NotPossible, kv.Value)));
                    break;
                case 1:
                    Ghost definiteGhost = ghostsPossible[0];
                    ghostPossibilities.Add(new GhostPossibility(definiteGhost, Possibility.Definite));
                    ghostPossibilities.AddRange(ghostsNotPossible.Select(g => new GhostPossibility(g, Possibility.NotPossible)));
                    evidencePossibilities.Clear();
                    evidencePossibilities.AddRange(evidenceStates.Select(kv => new EvidencePossibility(kv.Key, definiteGhost.RequiredEvidence.HasFlag(kv.Key) ? Possibility.Definite : Possibility.NotPossible, kv.Value)));
                    break;
                default:
                    ghostPossibilities.AddRange(ghostsPossible.Select(g => new GhostPossibility(g, Possibility.Possible)));
                    ghostPossibilities.AddRange(ghostsNotPossible.Select(g => new GhostPossibility(g, Possibility.NotPossible)));
                    break;
            }

            return (evidencePossibilities, ghostPossibilities.OrderBy(g => g.Ghost.Name).ToList());
        }
    }
}
