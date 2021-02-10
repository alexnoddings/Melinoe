using Fluxor;
using Melinoe.Client.State.JoinGame;
using Microsoft.AspNetCore.Components;

namespace Melinoe.Client.Components
{
    public partial class JoinGameDialogue
    {
        public string GameCode { get; set; }
        public string UserName { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        public void Connect() => 
            Dispatcher.Dispatch(new JoinGameAction(GameCode, UserName));
    }
}
