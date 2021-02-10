using System;

namespace Melinoe.Shared.Evidence
{
    [Flags]
    public enum EvidenceType
    {
        EmfLevel5 = 1 << 1,
        Fingerprints = 1 << 2,
        FreezingTemperatures = 1 << 3,
        GhostOrbs = 1 << 4,
        GhostWriting = 1 << 5,
        SpiritBox = 1 << 6
    }
}
