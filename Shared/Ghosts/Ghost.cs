using System.Collections.Generic;
using Melinoe.Shared.Evidence;

namespace Melinoe.Shared.Ghosts
{
    public class Ghost
    {
        public string Name { get; }
        public EvidenceType RequiredEvidence { get; }

        public Ghost(string name, EvidenceType requiredEvidence)
        {
            Name = name;
            RequiredEvidence = requiredEvidence;
        }

        public static readonly IReadOnlyList<Ghost> AllGhosts = new Ghost[]
        {
            new("Banshee", EvidenceType.EmfLevel5 | EvidenceType.Fingerprints | EvidenceType.FreezingTemperatures),
            new("Demon", EvidenceType.FreezingTemperatures | EvidenceType.GhostWriting | EvidenceType.SpiritBox),
            new("Jinn", EvidenceType.EmfLevel5 | EvidenceType.GhostOrbs | EvidenceType.SpiritBox),
            new("Mare", EvidenceType.FreezingTemperatures | EvidenceType.GhostOrbs | EvidenceType.SpiritBox),
            new("Oni", EvidenceType.EmfLevel5 | EvidenceType.GhostWriting | EvidenceType.SpiritBox),
            new("Phantom", EvidenceType.EmfLevel5 | EvidenceType.FreezingTemperatures | EvidenceType.GhostOrbs),
            new("Poltergeist", EvidenceType.Fingerprints | EvidenceType.GhostOrbs | EvidenceType.SpiritBox),
            new("Revenant", EvidenceType.EmfLevel5 | EvidenceType.Fingerprints | EvidenceType.GhostWriting),
            new("Shade", EvidenceType.EmfLevel5 | EvidenceType.GhostOrbs | EvidenceType.GhostWriting),
            new("Spirit", EvidenceType.Fingerprints | EvidenceType.GhostWriting | EvidenceType.SpiritBox),
            new("Wraith", EvidenceType.Fingerprints | EvidenceType.FreezingTemperatures | EvidenceType.SpiritBox),
            new("Yurei", EvidenceType.FreezingTemperatures | EvidenceType.GhostOrbs | EvidenceType.GhostWriting),
        };
    }
}
