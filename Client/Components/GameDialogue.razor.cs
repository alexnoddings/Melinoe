using Fluxor;
using Melinoe.Client.State;
using Melinoe.Client.State.ResetGame;
using Melinoe.Client.State.UpdateEvidence;
using Melinoe.Client.State.UpdateName;
using Melinoe.Client.State.UpdateObjectives;
using Melinoe.Shared.Evidence;
using Melinoe.Shared.Ghosts;
using Melinoe.Shared.Objectives;
using Microsoft.AspNetCore.Components;

namespace Melinoe.Client.Components
{
    public partial class GameDialogue
    {
        [Inject]
        public IState<GameState> State { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        public GameState Game => State.Value;

        private void UpdateEvidence(EvidenceType type, EvidenceState state) => 
            Dispatcher.Dispatch(new UpdateEvidenceAction(type, state));

        private void UpdateFirstName(GhostFirstName name) => 
            Dispatcher.Dispatch(new UpdateFirstNameAction(name));

        private void UpdateLastName(GhostLastName name) => 
            Dispatcher.Dispatch(new UpdateLastNameAction(name));

        private void UpdateObjective(Objective objective, bool isEnabled) =>
            Dispatcher.Dispatch(new UpdateObjectiveAction(objective, isEnabled));

        private void ResetGame() =>
            Dispatcher.Dispatch(new ResetGameAction());
    }
}
