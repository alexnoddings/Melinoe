namespace Melinoe.Client.State.GameReset
{
    public class GameResetAction
    {
        public GameState NewGameState { get; }

        public GameResetAction(GameState newGameState)
        {
            NewGameState = newGameState;
        }
    }
}
