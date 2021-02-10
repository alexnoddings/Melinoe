using Melinoe.Shared.Ghosts;

namespace Melinoe.Client.State.NameUpdated
{
    public class LastNameUpdatedAction
    {
        public GhostLastName? LastName { get; }

        public LastNameUpdatedAction(GhostLastName? lastName)
        {
            LastName = lastName;
        }
    }
}
