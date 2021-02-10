using System.Collections.Generic;
using Melinoe.Shared.Evidence;
using Melinoe.Shared.Ghosts;
using Melinoe.Shared.Objectives;

namespace Melinoe.Shared.State
{
    public class GameStateDto
    {
        public string Code { get; set; }

        public List<string> Players { get; set; }

        public GhostFirstName? FirstName { get; set; }
        public GhostLastName? LastName { get; set; }
        public Objective Objectives { get; set; }

        public Dictionary<EvidenceType, EvidenceState> EvidenceStates { get; set; }

        public List<EvidencePossibility> EvidencePossibilities { get; set; }
        public List<GhostPossibility> GhostPossibilities { get; set; }
    }
}
