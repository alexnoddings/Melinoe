using System.Threading.Tasks;
using Fluxor;
using Melinoe.Client.Services;

namespace Melinoe.Client.State.UpdateName
{
    public class UpdateFirstNameEffect : Effect<UpdateFirstNameAction>
    {
        private readonly GameService _gameService;

        public UpdateFirstNameEffect(GameService gameService)
        {
            _gameService = gameService;
        }

        protected override Task HandleAsync(UpdateFirstNameAction action, IDispatcher dispatcher) => 
            _gameService.UpdateFirstNameAsync(action.FirstName);
    }
}
