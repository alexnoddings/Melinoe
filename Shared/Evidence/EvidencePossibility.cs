using Melinoe.Shared.Possibilities;

namespace Melinoe.Shared.Evidence
{
    public class EvidencePossibility
    {
        public EvidenceType Type { get; }
        public Possibility Possibility {  get; }
        public EvidenceState OriginalState { get; }

        public EvidencePossibility(EvidenceType type, Possibility possibility, EvidenceState originalState)
        {
            Type = type;
            Possibility = possibility;
            OriginalState = originalState;
        }
    }
}
