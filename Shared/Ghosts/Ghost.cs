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
            new("Banshee", EvidenceType.GhostOrbs | EvidenceType.Fingerprints | EvidenceType.DotsProjector),
            new("Demon", EvidenceType.FreezingTemperatures | EvidenceType.Fingerprints | EvidenceType.GhostWriting),
            new("Goryo", EvidenceType.EmfLevel5 | EvidenceType.Fingerprints | EvidenceType.DotsProjector),
            new("Hantu", EvidenceType.GhostOrbs | EvidenceType.FreezingTemperatures | EvidenceType.Fingerprints),
            new("Jinn", EvidenceType.EmfLevel5 | EvidenceType.FreezingTemperatures | EvidenceType.Fingerprints),
            new("Mare", EvidenceType.GhostOrbs | EvidenceType.SpiritBox | EvidenceType.GhostWriting),
            new("Myling", EvidenceType.EmfLevel5 | EvidenceType.Fingerprints | EvidenceType.GhostWriting),
            new("Oni", EvidenceType.EmfLevel5 | EvidenceType.FreezingTemperatures | EvidenceType.DotsProjector),
            new("Phantom", EvidenceType.SpiritBox | EvidenceType.Fingerprints | EvidenceType.DotsProjector),
            new("Poltergeist", EvidenceType.SpiritBox | EvidenceType.Fingerprints | EvidenceType.GhostWriting),
            new("Revenant", EvidenceType.GhostOrbs | EvidenceType.FreezingTemperatures | EvidenceType.GhostWriting),
            new("Shade", EvidenceType.EmfLevel5 | EvidenceType.FreezingTemperatures | EvidenceType.GhostWriting),
            new("Spirit", EvidenceType.EmfLevel5 | EvidenceType.SpiritBox | EvidenceType.GhostWriting),
            new("Wraith", EvidenceType.EmfLevel5 | EvidenceType.SpiritBox | EvidenceType.DotsProjector),
            new("Yokai", EvidenceType.GhostOrbs | EvidenceType.SpiritBox | EvidenceType.DotsProjector),
            new("Yurei", EvidenceType.GhostOrbs | EvidenceType.FreezingTemperatures | EvidenceType.DotsProjector),
        };
    }
}
