using Melinoe.Shared.Ghosts;

namespace Melinoe.Client.State.UpdateName
{
    public class UpdateLastNameAction
    {
        public GhostLastName LastName { get; }

        public UpdateLastNameAction(GhostLastName lastName)
        {
            LastName = lastName;
        }
    }
}
