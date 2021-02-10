using Fluxor;
using System.Threading.Tasks;
using Melinoe.Client.Services;

namespace Melinoe.Client.State.UpdateName
{
    public class UpdateLastNameEffect : Effect<UpdateLastNameAction>
    {
        private readonly GameService _gameService;

        public UpdateLastNameEffect(GameService gameService)
        {
            _gameService = gameService;
        }

        protected override Task HandleAsync(UpdateLastNameAction action, IDispatcher dispatcher) =>
            _gameService.UpdateLastNameAsync(action.LastName);
    }
}
