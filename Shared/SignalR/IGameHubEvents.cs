using System.Collections.Generic;
using System.Threading.Tasks;
using Melinoe.Shared.Evidence;
using Melinoe.Shared.Ghosts;
using Melinoe.Shared.Objectives;
using Melinoe.Shared.State;

namespace Melinoe.Shared.SignalR
{
    public interface IGameHubEvents
    {
        public Task OnPlayersUpdated(List<string> players);
        public Task OnEvidenceUpdated(Dictionary<EvidenceType, EvidenceState> evidenceStates, List<EvidencePossibility> evidencePossibilities, List<GhostPossibility> ghostPossibilities);
        public Task OnFirstNameUpdated(GhostFirstName? name);
        public Task OnLastNameUpdated(GhostLastName? name);
        public Task OnObjectivesUpdated(Objective objective);
        public Task OnStateReset(GameStateDto newState);
    }
}
