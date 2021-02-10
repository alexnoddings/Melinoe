using Fluxor;

namespace Melinoe.Client.State.ObjectivesUpdated
{
    public static class ObjectivesUpdatedReducers
    {
        [ReducerMethod]
        public static GameState ReduceObjectivesUpdatedAction(GameState state, ObjectivesUpdatedAction action) =>
            state with {Objectives = action.Objectives};
    }
}
