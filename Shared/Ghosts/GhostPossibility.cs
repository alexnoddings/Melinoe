using Melinoe.Shared.Possibilities;

namespace Melinoe.Shared.Ghosts
{
    public class GhostPossibility
    {
        public Ghost Ghost { get; }
        public Possibility Possibility { get; }

        public GhostPossibility(Ghost ghost, Possibility possibility)
        {
            Ghost = ghost;
            Possibility = possibility;
        }
    }
}
