using Melinoe.Shared.Objectives;

namespace Melinoe.Client.State.ObjectivesUpdated
{
    public class ObjectivesUpdatedAction
    {
        public Objective Objectives { get; }

        public ObjectivesUpdatedAction(Objective objectives)
        {
            Objectives = objectives;
        }
    }
}
