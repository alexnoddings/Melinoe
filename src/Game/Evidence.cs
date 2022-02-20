namespace Melinoe.Game;

[Flags]
public enum EvidenceType
{
	DotsProjector = 1 << 1,
	EmfLevel5 = 1 << 2,
	Fingerprints = 1 << 3,
	FreezingTemperatures = 1 << 4,
	GhostOrbs = 1 << 5,
	GhostWriting = 1 << 6,
	SpiritBox = 1 << 7,
}

public enum EvidenceState
{
	Unknown,
	Present,
	Missing
}

public interface IEvidence
{
	public EvidenceType Type { get; }
	public EvidenceState State { get; }
}
