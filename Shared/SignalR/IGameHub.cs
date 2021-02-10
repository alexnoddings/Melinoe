using System.Threading.Tasks;
using Melinoe.Shared.Evidence;
using Melinoe.Shared.Ghosts;
using Melinoe.Shared.Objectives;
using Melinoe.Shared.State;

namespace Melinoe.Shared.SignalR
{
    public interface IGameHub
    {
        Task<GameStateDto> JoinAsync(string gameCode, string userName);
        Task UpdateEvidenceStateAsync(EvidenceType evidenceType, EvidenceState evidenceState);
        Task UpdateFirstNameAsync(GhostFirstName name);
        Task UpdateLastNameAsync(GhostLastName name);
        Task UpdateObjectiveAsync(Objective objective, bool isEnabled);
        Task ResetAsync();
    }
}