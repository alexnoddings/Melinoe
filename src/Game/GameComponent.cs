using Microsoft.AspNetCore.Components;

namespace Melinoe.Game;

public class GameComponent : ComponentBase, IDisposable
{
	[Parameter, EditorRequired]
	public IGameSubscription Subscription { get; set; } = default!;
	private IGameSubscription? _previousSubscription;

	protected SyncedGame Game => Subscription.Game;

	protected override void OnParametersSet()
	{
		if (Subscription is null)
			throw new InvalidOperationException($"{nameof(Subscription)} cannot be null.");

		if (Subscription != _previousSubscription)
		{
			if (_previousSubscription is not null)
				_previousSubscription.Game.OnUpdated -= OnGameUpdatedAsync;
			_previousSubscription = Subscription;
		}
	}

	protected async Task OnGameUpdatedAsync() => 
		await InvokeAsync(StateHasChanged);

	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
			Subscription.Game.OnUpdated -= OnGameUpdatedAsync;
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
