using System.Collections.Generic;
using Melinoe.Shared.Evidence;
using Melinoe.Shared.Ghosts;
using Melinoe.Shared.Objectives;

namespace Melinoe.Client.State
{
    public record GameState
    {
        public SynchronisationState SyncState { get; init; }

        public string Code { get; init; }
        public List<string> Players { get; init; }

        public GhostFirstName? FirstName { get; init; }
        public GhostLastName? LastName { get; init; }
        public Objective Objectives { get; init; }

        public Dictionary<EvidenceType, EvidenceState> EvidenceStates { get; init; }

        public List<EvidencePossibility> EvidencePossibilities { get; init; }
        public List<GhostPossibility> GhostPossibilities { get; init; }

        public GameState(SynchronisationState syncState, string code, List<string> players, GhostFirstName? firstName, GhostLastName? lastName, Objective objectives, Dictionary<EvidenceType, EvidenceState> evidenceStates, List<EvidencePossibility> evidencePossibilities, List<GhostPossibility> ghostPossibilities)
        {
            SyncState = syncState;
            Code = code;
            Players = players;
            FirstName = firstName;
            LastName = lastName;
            Objectives = objectives;
            EvidenceStates = evidenceStates;
            EvidencePossibilities = evidencePossibilities;
            GhostPossibilities = ghostPossibilities;
        }

        public static GameState Disconnected() =>
            new(SynchronisationState.Disconnected, string.Empty, new List<string>(), null, null, (Objective) 0, new Dictionary<EvidenceType, EvidenceState>(), new List<EvidencePossibility>(), new List<GhostPossibility>());

        public static GameState Connecting(string gameCode, string userName) =>
            new(SynchronisationState.Connecting, gameCode, new List<string> {userName}, null, null, (Objective)0, new Dictionary<EvidenceType, EvidenceState>(), new List<EvidencePossibility>(), new List<GhostPossibility>());
    }
}
