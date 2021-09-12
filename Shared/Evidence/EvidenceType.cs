using System;

namespace Melinoe.Shared.Evidence
{
    [Flags]
    public enum EvidenceType
    {
        EmfLevel5 = 1 << 1,
        GhostOrbs = 1 << 2,
        SpiritBox = 1 << 3,
        FreezingTemperatures = 1 << 4,
        Fingerprints = 1 << 5,
        GhostWriting = 1 << 6,
        DotsProjector = 1 << 7,
    }
}
