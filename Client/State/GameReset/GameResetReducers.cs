using Fluxor;

namespace Melinoe.Client.State.GameReset
{
    public static class GameResetReducers
    {
        [ReducerMethod]
        public static GameState ReduceGameResetAction(GameState state, GameResetAction action) =>
            action.NewGameState;
    }
}
