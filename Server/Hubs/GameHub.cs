using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Melinoe.Server.Game;
using Melinoe.Server.Services;
using Melinoe.Shared.Evidence;
using Melinoe.Shared.Ghosts;
using Melinoe.Shared.Objectives;
using Melinoe.Shared.SignalR;
using Melinoe.Shared.State;
using Microsoft.AspNetCore.SignalR;

namespace Melinoe.Server.Hubs
{
    public class GameHub : Hub<IGameHubEvents>, IGameHub
    {
        private readonly GameService _gameService;

        public GameHub(GameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<GameStateDto> JoinAsync(string gameCode, string userName)
        {
            if (!Regex.IsMatch(gameCode, @"^[0-9]{6}$"))
                throw new HubException("Invalid game code.");

            if (!Regex.IsMatch(userName, @"^[a-zA-Z0-9]{4,20}$"))
                throw new HubException("Invalid username.");

            GameState gameState = await _gameService.JoinAsync(Context.ConnectionId, gameCode, userName);
            await Groups.AddToGroupAsync(Context.ConnectionId, gameCode);
            await Clients.GroupExcept(gameCode, Context.ConnectionId).OnPlayersUpdated(gameState.Users.Select(u => u.UserName).ToList());
            return new GameStateDto
            {
                Code = gameState.GameCode,
                Players = gameState.Users.Select(u => u.UserName).ToList(),
                FirstName = gameState.FirstName,
                LastName = gameState.LastName, 
                Objectives = gameState.Objectives,
                EvidencePossibilities = gameState.EvidencePossibilities, 
                EvidenceStates = gameState.EvidenceStates,
                GhostPossibilities = gameState.GhostPossibilities
            };
        }

        public async Task UpdateEvidenceStateAsync(EvidenceType evidenceType, EvidenceState evidenceState)
        {
            GameState? newState = await _gameService.UpdateEvidenceStateAsync(Context.ConnectionId, evidenceType, evidenceState);
            if (newState is null)
                throw new HubException("Invalid input.");

            await Clients.Group(newState.GameCode).OnEvidenceUpdated(newState.EvidenceStates, newState.EvidencePossibilities, newState.GhostPossibilities);
        }

        public async Task UpdateFirstNameAsync(GhostFirstName firstName)
        {
            GameState? newState = await _gameService.UpdateFirstNameAsync(Context.ConnectionId, firstName);
            if (newState is null)
                throw new HubException("Invalid input.");

            await Clients.Group(newState.GameCode).OnFirstNameUpdated(newState.FirstName);
        }

        public async Task UpdateLastNameAsync(GhostLastName lastName)
        {
            GameState? newState = await _gameService.UpdateLastNameAsync(Context.ConnectionId, lastName);
            if (newState is null)
                throw new HubException("Invalid input.");

            await Clients.Group(newState.GameCode).OnLastNameUpdated(newState.LastName);
        }

        public async Task UpdateObjectiveAsync(Objective objective, bool isEnabled)
        {
            GameState? newState = await _gameService.UpdateObjectiveAsync(Context.ConnectionId, objective, isEnabled);
            if (newState is null)
                throw new HubException("Invalid input.");

            await Clients.Group(newState.GameCode).OnObjectivesUpdated(newState.Objectives);
        }

        public async Task ResetAsync()
        {
            GameState? newState = await _gameService.ResetAsync(Context.ConnectionId);

            if (newState is null)
                throw new HubException("Invalid input.");

            GameStateDto dto = new()
            {
                Code = newState.GameCode,
                Players = newState.Users.Select(u => u.UserName).ToList(),
                FirstName = newState.FirstName,
                LastName = newState.LastName,
                Objectives = newState.Objectives,
                EvidencePossibilities = newState.EvidencePossibilities,
                EvidenceStates = newState.EvidenceStates,
                GhostPossibilities = newState.GhostPossibilities
            };
            await Clients.Group(newState.GameCode).OnStateReset(dto);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            GameState gameState = await _gameService.LeaveAsync(Context.ConnectionId);
            if (gameState != null)
                await Clients.Group(gameState.GameCode).OnPlayersUpdated(gameState.Users.Select(u => u.UserName).ToList());
        }
    }
}
