using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Melinoe.Server.Game;
using Melinoe.Shared.Evidence;
using Melinoe.Shared.Ghosts;
using Melinoe.Shared.Objectives;
using Melinoe.Shared.Possibilities;

namespace Melinoe.Server.Services
{
    public class GameService
    {
        private Dictionary<string, GameUser> Users { get; } = new();
        private Dictionary<string, GameState> Games { get; } = new();

        public async Task<GameState> JoinAsync(string connectionId, string gameCode, string userName)
        {
            if (Users.ContainsKey(connectionId))
                return null;

            var user = new GameUser(connectionId, gameCode, userName);
            Users.Add(connectionId, user);
            GameState gameState;
            if (Games.ContainsKey(gameCode))
            {
                gameState = Games[gameCode];
            }
            else
            {
                gameState = GameStateCalculator.Default(gameCode);
                Games.Add(gameCode, gameState);
            }
            gameState.Users.Add(user);
            return gameState;
        }

        public async Task<GameState?> UpdateEvidenceStateAsync(string connectionId, EvidenceType evidenceType, EvidenceState evidenceState)
        {
            GameState? oldGameState = GetGameState(connectionId);
            if (oldGameState is null)
                return null;

            GameState newGameState = GameStateCalculator.UpdateEvidence(oldGameState, evidenceType, evidenceState);
            if (newGameState.GhostPossibilities.All(g => g.Possibility == Possibility.NotPossible))
                // New state is invalid, don't update it
                return null;

            Games[oldGameState.GameCode] = newGameState;
            return newGameState;
        }

        public async Task<GameState?> UpdateFirstNameAsync(string connectionId, GhostFirstName firstName)
        {
            GameState? oldGameState = GetGameState(connectionId);
            if (oldGameState is null)
                return null;

            if (!Enum.IsDefined(firstName))
                return null;

            GameState newGameState = oldGameState with {FirstName = firstName};

            Games[oldGameState.GameCode] = newGameState;
            return newGameState;
        }

        public async Task<GameState?> UpdateLastNameAsync(string connectionId, GhostLastName lastName)
        {
            GameState? oldGameState = GetGameState(connectionId);
            if (oldGameState is null)
                return null;

            if (!Enum.IsDefined(lastName))
                return null;

            GameState newGameState = oldGameState with { LastName = lastName };

            Games[oldGameState.GameCode] = newGameState;
            return newGameState;
        }

        public async Task<GameState?> UpdateObjectiveAsync(string connectionId, Objective objective, bool isEnabled)
        {
            GameState? oldGameState = GetGameState(connectionId);
            if (oldGameState is null)
                return null;

            if (!Enum.IsDefined(objective))
                return null;

            GameState newGameState;
            if (isEnabled)
                newGameState = oldGameState with {Objectives = oldGameState.Objectives | objective};
            else
                newGameState = oldGameState with {Objectives = oldGameState.Objectives & ~objective};

            Games[oldGameState.GameCode] = newGameState;
            return newGameState;
        }

        public async Task<GameState?> ResetAsync(string connectionId)
        {
            GameState? oldGameState = GetGameState(connectionId);
            if (oldGameState is null)
                return null;

            string gameCode = oldGameState.GameCode;
            GameState newGameState = GameStateCalculator.Default(gameCode, oldGameState.Users);

            Games[gameCode] = newGameState;
            return newGameState;
        }

        private GameState? GetGameState(string connectionId)
        {
            if (!Users.ContainsKey(connectionId))
                return null;

            GameUser gameUser = Users[connectionId];
            if (!Games.ContainsKey(gameUser.GameCode))
            {
                Users.Remove(connectionId);
                return null;
            }

            return Games[gameUser.GameCode];
        }

        public async Task<GameState> LeaveAsync(string connectionId)
        {
            Users.Remove(connectionId, out GameUser user);
            if (user is null)
                return null;

            string gameCode = user.GameCode;
            if (!Games.ContainsKey(gameCode))
                return null;

            GameState gameState = Games[gameCode];
            if (gameState.Users.Count <= 1)
            {
                Games.Remove(gameCode);
                return null;
            }

            gameState.Users.Remove(user);
            return gameState;
        }
    }
}
