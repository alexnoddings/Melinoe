using Blazorise;
using Fluxor;
using Melinoe.Client.State;
using Melinoe.Client.State.ResetGame;
using Melinoe.Client.State.UpdateEvidence;
using Melinoe.Client.State.UpdateName;
using Melinoe.Client.State.UpdateObjectives;
using Melinoe.Shared.Evidence;
using Melinoe.Shared.Ghosts;
using Melinoe.Shared.Objectives;
using Melinoe.Shared.Possibilities;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Melinoe.Client.Components
{
    public partial class GameDialogue
    {
        [Inject]
        public IState<GameState> State { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }
        
        [Inject]
        public IStringLocalizer<App> Localiser { get; set; }

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

        private TextColor GetHeaderTextColor(Possibility possibility) =>
            possibility switch
            {
                Possibility.Definite => TextColor.Success,
                Possibility.Possible => TextColor.Warning,
                Possibility.NotPossible => TextColor.Danger
            };

        private TextColor GetBodyTextColor(Possibility possibility) =>
            possibility switch
            {
                Possibility.Definite => TextColor.Body,
                Possibility.Possible => TextColor.Warning,
                Possibility.NotPossible => TextColor.Muted
            };
    }
}
