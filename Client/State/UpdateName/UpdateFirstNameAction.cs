using Melinoe.Shared.Ghosts;

namespace Melinoe.Client.State.UpdateName
{
    public class UpdateFirstNameAction
    {
        public GhostFirstName FirstName { get; }

        public UpdateFirstNameAction(GhostFirstName firstName)
        {
            FirstName = firstName;
        }
    }
}
