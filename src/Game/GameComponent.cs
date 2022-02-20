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
			throw new ArgumentNullException(nameof(Subscription));

		if (Subscription != _previousSubscription)
		{
			if (_previousSubscription is not null)
				_previousSubscription.Game.OnUpdated -= OnGameUpdatedAsync;
			_previousSubscription = Subscription;
		}
	}

	protected async Task OnGameUpdatedAsync() => 
		await InvokeAsync(StateHasChanged);

	public void Dispose() =>
		Subscription.Game.OnUpdated -= OnGameUpdatedAsync;
}
