namespace Melinoe.Client.State.JoinGame
{
    public class JoinGameResultAction
    {
        public GameState GameState { get; }

        public JoinGameResultAction(GameState gameState)
        {
            GameState = gameState;
        }
    }
}
