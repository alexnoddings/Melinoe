using System.Threading.Tasks;
using Fluxor;
using Melinoe.Client.Services;

namespace Melinoe.Client.State.UpdateEvidence
{
    public class UpdateEvidenceEffect : Effect<UpdateEvidenceAction>
    {
        private readonly GameService _gameService;

        public UpdateEvidenceEffect(GameService gameService)
        {
            _gameService = gameService;
        }

        protected override Task HandleAsync(UpdateEvidenceAction action, IDispatcher dispatcher)
        {
            return _gameService.UpdateEvidenceAsync(action.EvidenceType, action.State);
        }
    }
}
