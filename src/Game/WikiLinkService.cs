namespace Melinoe.Game;

public class WikiLinkService
{
	public const string WikiLinkBase = "https://phasmophobia.fandom.com/wiki";

	public string GetWikiLinkFor(GhostType ghostType) =>
		ghostType switch
		{
			GhostType.Mimic or GhostType.Twins => $"{WikiLinkBase}/The_{ghostType}",
			_ => $"{WikiLinkBase}/{ghostType}",
		};

	public string GetWikiLinkFor(IGhost ghost) =>
		GetWikiLinkFor(ghost.Type);

	public string GetWikiLinkFor(EvidenceType evidenceType) =>
		evidenceType switch
		{
			EvidenceType.DotsProjector => $"{WikiLinkBase}/D.O.T.S_Projector_(Evidence)",
			EvidenceType.EmfLevel5 => $"{WikiLinkBase}/EMF_Level_5",
			EvidenceType.Fingerprints => $"{WikiLinkBase}/Fingerprints",
			EvidenceType.FreezingTemperatures => $"{WikiLinkBase}/Freezing_Temperatures",
			EvidenceType.GhostOrbs => $"{WikiLinkBase}/Ghost_Orb",
			EvidenceType.GhostWriting => $"{WikiLinkBase}/Ghost_Writing",
			EvidenceType.SpiritBox => $"{WikiLinkBase}/Spirit_Box_(Evidence)",
			_ => WikiLinkBase,
		};

	public string GetWikiLinkFor(IEvidence evidence) =>
		GetWikiLinkFor(evidence.Type);
}
