using Melinoe.Shared.Objectives;

namespace Melinoe.Client.State.UpdateObjectives
{
    public class UpdateObjectiveAction
    {
        public Objective Objective { get; }
        public bool IsEnabled { get; }

        public UpdateObjectiveAction(Objective objective, bool isEnabled)
        {
            Objective = objective;
            IsEnabled = isEnabled;
        }
    }
}
