using Fluxor;
using Melinoe.Client.State;
using Microsoft.AspNetCore.Components;

namespace Melinoe.Client.Pages
{
    public partial class Index
    {
        [Inject]
        public IState<GameState> GameState { get; set; }

        private GameState Game => GameState.Value;
    }
}
