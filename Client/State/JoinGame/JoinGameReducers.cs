using Fluxor;

namespace Melinoe.Client.State.JoinGame
{
    public static class JoinGameReducers
    {
        [ReducerMethod]
        public static GameState ReduceJoinGameAction(GameState state, JoinGameAction action) =>
            GameState.Connecting(action.Code, action.UserName);

        [ReducerMethod]
        public static GameState ReduceJoinGameResult(GameState state, JoinGameResultAction resultAction) =>
            resultAction.GameState;
    }
}
