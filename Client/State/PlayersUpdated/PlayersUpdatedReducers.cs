using Fluxor;

namespace Melinoe.Client.State.PlayersUpdated
{
    public static class PlayersUpdatedReducers
    {
        [ReducerMethod]
        public static GameState ReducePlayersUpdatedAction(GameState state, PlayersUpdatedAction action) =>
            state with {Players = action.Players};
    }
}
