using System.Threading.Tasks;
using Fluxor;
using Melinoe.Client.Services;

namespace Melinoe.Client.State.ResetGame
{
    public class ResetGameEffect : Effect<ResetGameAction>
    {
        private readonly GameService _gameService;

        public ResetGameEffect(GameService gameService)
        {
            _gameService = gameService;
        }

        protected override Task HandleAsync(ResetGameAction action, IDispatcher dispatcher) =>
            _gameService.ResetAsync();
    }
}
