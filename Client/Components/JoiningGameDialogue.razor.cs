using Fluxor;
using Melinoe.Client.State;
using Microsoft.AspNetCore.Components;

namespace Melinoe.Client.Components
{
    public partial class JoiningGameDialogue
    {
        [Inject]
        public IState<GameState> State { get; set; }

        public GameState Game => State.Value;
    }
}
