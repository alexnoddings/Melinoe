using Melinoe.Shared.Ghosts;

namespace Melinoe.Client.State.NameUpdated
{
    public class FirstNameUpdatedAction
    {
        public GhostFirstName? FirstName { get; }

        public FirstNameUpdatedAction(GhostFirstName? firstName)
        {
            FirstName = firstName;
        }
    }
}
