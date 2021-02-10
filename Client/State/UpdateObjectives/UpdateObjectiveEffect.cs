using System.Threading.Tasks;
using Fluxor;
using Melinoe.Client.Services;

namespace Melinoe.Client.State.UpdateObjectives
{
    public class UpdateObjectiveEffect : Effect<UpdateObjectiveAction>
    {
        private readonly GameService _gameService;

        public UpdateObjectiveEffect(GameService gameService)
        {
            _gameService = gameService;
        }

        protected override Task HandleAsync(UpdateObjectiveAction action, IDispatcher dispatcher)
        {
            return _gameService.UpdateObjectiveAsync(action.Objective, action.IsEnabled);
        }
    }
}
