using System.Collections.Generic;
using Melinoe.Shared.Evidence;
using Melinoe.Shared.Ghosts;

namespace Melinoe.Client.State.EvidenceUpdated
{
    public class EvidenceUpdatedAction
    {
        public Dictionary<EvidenceType, EvidenceState> EvidenceStates { get; }
        public List<EvidencePossibility> EvidencePossibilities { get; }
        public List<GhostPossibility> GhostPossibilities { get; }

        public EvidenceUpdatedAction(Dictionary<EvidenceType, EvidenceState> evidenceStates, List<EvidencePossibility> evidencePossibilities, List<GhostPossibility> ghostPossibilities)
        {
            EvidenceStates = evidenceStates;
            EvidencePossibilities = evidencePossibilities;
            GhostPossibilities = ghostPossibilities;
        }
    }
}
