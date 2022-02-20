namespace Melinoe.Game;

public enum GhostType
{
	Banshee,
	Demon,
	Goryo,
	Hantu,
	Jinn,
	Mare,
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
	Mimic,
	Twins,
	Wraith,
	Yokai,
	Yure,
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
	public EvidenceType RequiredEvidence { get; }
}

