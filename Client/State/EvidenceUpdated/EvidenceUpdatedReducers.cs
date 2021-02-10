using Fluxor;

namespace Melinoe.Client.State.EvidenceUpdated
{
    public static class EvidenceUpdatedReducers
    {
        [ReducerMethod]
        public static GameState ReduceEvidenceUpdatedAction(GameState state, EvidenceUpdatedAction action) =>
            state with
            {
                EvidenceStates = action.EvidenceStates,
                EvidencePossibilities = action.EvidencePossibilities,
                GhostPossibilities = action.GhostPossibilities
            };
    }
}
