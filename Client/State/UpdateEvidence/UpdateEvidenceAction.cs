using Melinoe.Shared.Evidence;

namespace Melinoe.Client.State.UpdateEvidence
{
    public class UpdateEvidenceAction
    {
        public EvidenceType EvidenceType { get; }
        public EvidenceState State { get; }

        public UpdateEvidenceAction(EvidenceType evidenceType, EvidenceState state)
        {
            EvidenceType = evidenceType;
            State = state;
        }
    }
}
