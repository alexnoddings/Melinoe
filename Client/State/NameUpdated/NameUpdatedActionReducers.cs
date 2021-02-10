using Fluxor;

namespace Melinoe.Client.State.NameUpdated
{
    public static class NameUpdatedActionReducers
    {
        [ReducerMethod]
        public static GameState ReduceFirstNameUpdatedAction(GameState state, FirstNameUpdatedAction action) =>
            state with {FirstName = action.FirstName};

        [ReducerMethod]
        public static GameState ReduceFirstNameUpdatedAction(GameState state, LastNameUpdatedAction action) =>
            state with { LastName = action.LastName };
    }
}
