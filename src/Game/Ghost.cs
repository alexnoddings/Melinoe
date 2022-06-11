namespace Melinoe.Game;

public enum GhostType
{
	Banshee,
	Demon,
	Deogen,
	Goryo,
	Hantu,
	Jinn,
	Mare,
	Moroi,
	Myling,
	Obake,
	Oni,
	Onryo,
	Phantom,
	Poltergeist,
	Raiju,
	Revenant,
	Shade,
	Spirit,
	Thaye,
	Mimic,
	Twins,
	Wraith,
	Yokai,
	Yurei,
}

public enum GhostState
{
	Potential,
	NotPossible,
	Definite
}

public interface IGhost
{
	public GhostType Type { get; }
	public GhostState State { get; }

	public bool IsRuledOut { get; }

	// What evidence the ghost has
	public EvidenceType RequiredEvidence { get; }

	// What evidence the ghost ALWAYS has
	// E.g. the Mimic will always have ghost orbs, regardless of difficulty,
	// so no ghost orbs would guarantee no Mimic
	public EvidenceType AbsoluteEvidence { get; }
}
