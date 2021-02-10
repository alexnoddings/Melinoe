using System.Threading.Tasks;
using Fluxor;
using Melinoe.Client.Services;

namespace Melinoe.Client.State.JoinGame
{
    public class JoinGameEffect : Effect<JoinGameAction>
    {
        private readonly GameService _gameService;

        public JoinGameEffect(GameService gameService)
        {
            _gameService = gameService;
        }

        protected override Task HandleAsync(JoinGameAction action, IDispatcher dispatcher) => 
            _gameService.JoinAsync(action.Code, action.UserName);
    }
}
