namespace Melinoe.Game;

public interface IGameSubscription : IDisposable
{
	public int GameCode { get; }
	public SyncedGame Game { get; }
}

public class GameCollection
{
	private class GameInfo 
	{
		public List<GameSubscription> Subscriptions { get; } = new();
		public SyncedGame Game { get; }

		public GameInfo(SyncedGame game)
		{
			Game = game ?? throw new ArgumentNullException(nameof(game));
		}
	}

	private class GameSubscription : IGameSubscription
	{
		public int GameCode { get; }
		public SyncedGame Game { get; }
		private Action<GameSubscription> Disposer { get; }

		public GameSubscription(int gameCode, SyncedGame game, Action<GameSubscription> disposer)
		{
			GameCode = gameCode;
			Game = game ?? throw new ArgumentNullException(nameof(game));
			Disposer = disposer ?? throw new ArgumentNullException(nameof(disposer));
		}

		public void Dispose() => Disposer(this);
	}

	private static Random Random { get; } = new();

	private Dictionary<int, GameInfo> Games { get; } = new();

	public IGameSubscription Create()
	{
		var code = GetNextGameCode();
		var game = new SyncedGame(GameType.Normal);
		var gameInfo = new GameInfo(game);

		var subscription = new GameSubscription(code, game, RemoveSubscription);
		Games.Add(code, gameInfo);
		gameInfo.Subscriptions.Add(subscription);
		return subscription;
	}

	private int GetNextGameCode()
	{
		var gameCode = -1;
		for (int i = 0; i < 512; i++)
		{
			var newCode = Random.Next(100_000, 999_999);
			if (Games.ContainsKey(newCode))
				continue;
			gameCode = newCode;
			break;
		}

		if (gameCode != -1)
			return gameCode;

		for (int i = 100_000; i <= 999_999; i++)
		{
			if (Games.ContainsKey(i))
				continue;
			gameCode = i;
			break;
		}

		if (gameCode == -1)
			throw new InvalidOperationException("Game collection exhausted.");

		return gameCode;
	}

	public IGameSubscription? Get(int gameCode)
	{
		if (!Games.TryGetValue(gameCode, out GameInfo? gameInfo))
			return null;

		var subscription = new GameSubscription(gameCode, gameInfo.Game, RemoveSubscription);
		gameInfo.Subscriptions.Add(subscription);
		return subscription;
	}

	private void RemoveSubscription(GameSubscription subscription)
	{
		if (!Games.TryGetValue(subscription.GameCode, out GameInfo? gameInfo))
			return;

		gameInfo.Subscriptions.Remove(subscription);
		if (gameInfo.Subscriptions.Count == 0)
			Games.Remove(subscription.GameCode);
	}
}
