using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fluxor;
using Melinoe.Client.Extensions;
using Melinoe.Client.State;
using Melinoe.Client.State.EvidenceUpdated;
using Melinoe.Client.State.GameReset;
using Melinoe.Client.State.JoinGame;
using Melinoe.Client.State.NameUpdated;
using Melinoe.Client.State.ObjectivesUpdated;
using Melinoe.Client.State.PlayersUpdated;
using Melinoe.Shared.Evidence;
using Melinoe.Shared.Ghosts;
using Melinoe.Shared.Objectives;
using Melinoe.Shared.SignalR;
using Melinoe.Shared.State;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Melinoe.Client.Services
{
    public class GameService : IGameHubEvents
    {
        private readonly HubConnection _hubConnection;

        private readonly IServiceProvider _serviceProvider;

        // An issue in mono or Fluxor prevents scoped services consuming an IDispatcher at application start.
        // As some Effects consume GameService, this will cause the application to crash.
        // For the time being, a service provider is used instead.
        private IDispatcher Dispatcher => _serviceProvider.GetRequiredService<IDispatcher>();

        public GameService(NavigationManager navigationManager, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Uri hubUri = navigationManager.ToAbsoluteUri(HubUrls.GameHub);
            _hubConnection =
                new HubConnectionBuilder()
                    .WithAutomaticReconnect()
                    .WithUrl(hubUri)
                    .Build();

            _hubConnection.Bind<IGameHubEvents>(this);
        }

        public async Task JoinAsync(string gameCode, string userName)
        {
            await EnsureStartedAsync();
            var gameStateDto = await _hubConnection.InvokeAsync<GameStateDto>(nameof(IGameHub.JoinAsync), gameCode, userName);
            var gameState = new GameState(SynchronisationState.Connected, gameStateDto.Code, gameStateDto.Players, gameStateDto.FirstName, gameStateDto.LastName, gameStateDto.Objectives, gameStateDto.EvidenceStates, gameStateDto.EvidencePossibilities, gameStateDto.GhostPossibilities);
            Dispatcher.Dispatch(new JoinGameResultAction(gameState));
        }

        public async Task UpdateEvidenceAsync(EvidenceType evidenceType, EvidenceState state)
        {
            await EnsureStartedAsync();
            await _hubConnection.SendAsync(nameof(IGameHub.UpdateEvidenceStateAsync), evidenceType, state);
        }

        public async Task UpdateFirstNameAsync(GhostFirstName? firstName)
        {
            await EnsureStartedAsync();
            await _hubConnection.SendAsync(nameof(IGameHub.UpdateFirstNameAsync), firstName);
        }

        public async Task UpdateLastNameAsync(GhostLastName? lastName)
        {
            await EnsureStartedAsync();
            await _hubConnection.SendAsync(nameof(IGameHub.UpdateLastNameAsync), lastName);
        }

        public async Task UpdateObjectiveAsync(Objective objective, bool isEnabled)
        {
            await EnsureStartedAsync();
            await _hubConnection.SendAsync(nameof(IGameHub.UpdateObjectiveAsync), objective, isEnabled);
        }

        public async Task ResetAsync()
        {
            await EnsureStartedAsync();
            await _hubConnection.SendAsync(nameof(IGameHub.ResetAsync));
        }

        public Task OnPlayersUpdated(List<string> players) => 
            DispatchTask(new PlayersUpdatedAction(players));

        public Task OnEvidenceUpdated(Dictionary<EvidenceType, EvidenceState> evidenceStates, List<EvidencePossibility> evidencePossibilities, List<GhostPossibility> ghostPossibilities) =>
            DispatchTask(new EvidenceUpdatedAction(evidenceStates, evidencePossibilities, ghostPossibilities));

        public Task OnFirstNameUpdated(GhostFirstName? firstName) => 
            DispatchTask(new FirstNameUpdatedAction(firstName));

        public Task OnLastNameUpdated(GhostLastName? lastName) =>
            DispatchTask(new LastNameUpdatedAction(lastName));

        public Task OnObjectivesUpdated(Objective objective) =>
            DispatchTask(new ObjectivesUpdatedAction(objective));

        public Task OnStateReset(GameStateDto newStateDto) =>
            DispatchTask(new GameResetAction(new GameState(SynchronisationState.Connected, newStateDto.Code, newStateDto.Players, newStateDto.FirstName, newStateDto.LastName, newStateDto.Objectives, newStateDto.EvidenceStates, newStateDto.EvidencePossibilities, newStateDto.GhostPossibilities)));

        private Task DispatchTask(object action)
        {
            Dispatcher.Dispatch(action);
            return Task.CompletedTask;
        }

        private Task EnsureStartedAsync() =>
            _hubConnection.State == HubConnectionState.Disconnected
                ? _hubConnection.StartAsync()
                : Task.CompletedTask;
    }
}
