namespace Melinoe.Game;

public class SyncedGame
{
	private class Evidence : IEvidence
	{
		public EvidenceType Type { get; }
		public EvidenceState State { get; set; }

		public Evidence(EvidenceType type)
		{
			Type = type;
			State = EvidenceState.Unknown;
		}
	}

	private class Ghost : IGhost
	{
		public GhostType Type { get; }
		public GhostState State { get; set; }
		public EvidenceType RequiredEvidence { get; }

		public Ghost(GhostType type, EvidenceType evidence)
		{
			Type = type;
			State = GhostState.Potential;
			RequiredEvidence = evidence;
		}
	}

	public GameType Type { get; set; }

	private readonly Evidence[] _evidences;
	public IEnumerable<IEvidence> Evidences => _evidences.AsEnumerable();

	private readonly Ghost[] _ghosts;
	public IEnumerable<IGhost> Ghosts => _ghosts.AsEnumerable();

	public event Func<Task>? OnUpdated;

	public SyncedGame(GameType type)
	{
		Type = type;
		_evidences = Enum.GetValues<EvidenceType>().Select(evidenceType => new Evidence(evidenceType)).ToArray();
		_ghosts = AllGhosts.Array;
	}

	public async Task UpdateEvidenceAsync(EvidenceType type, EvidenceState newState)
	{
		var evidence = _evidences.FirstOrDefault(evidence => evidence.Type == type);
		if (evidence is null)
			return;

		evidence.State = newState;
		if (OnUpdated is not null)
			await OnUpdated.Invoke();
	}

	private static class AllGhosts
	{
		public static Ghost[] Array => new Ghost[]
		{
			new(GhostType.Banshee, EvidenceType.GhostOrbs | EvidenceType.Fingerprints | EvidenceType.DotsProjector),
			new(GhostType.Demon, EvidenceType.FreezingTemperatures | EvidenceType.Fingerprints | EvidenceType.GhostWriting),
			new(GhostType.Goryo, EvidenceType.EmfLevel5 | EvidenceType.Fingerprints | EvidenceType.DotsProjector),
			new(GhostType.Hantu, EvidenceType.GhostOrbs | EvidenceType.FreezingTemperatures | EvidenceType.Fingerprints),
			new(GhostType.Jinn, EvidenceType.EmfLevel5 | EvidenceType.FreezingTemperatures | EvidenceType.Fingerprints),
			new(GhostType.Mare, EvidenceType.GhostOrbs | EvidenceType.SpiritBox | EvidenceType.GhostWriting),
			new(GhostType.Myling, EvidenceType.EmfLevel5 | EvidenceType.Fingerprints | EvidenceType.GhostWriting),
			new(GhostType.Obake, EvidenceType.EmfLevel5 | EvidenceType.GhostOrbs | EvidenceType.Fingerprints),
			new(GhostType.Oni, EvidenceType.EmfLevel5 | EvidenceType.FreezingTemperatures | EvidenceType.DotsProjector),
			new(GhostType.Onryo, EvidenceType.GhostOrbs | EvidenceType.SpiritBox | EvidenceType.FreezingTemperatures),
			new(GhostType.Phantom, EvidenceType.SpiritBox | EvidenceType.FreezingTemperatures | EvidenceType.DotsProjector),
			new(GhostType.Poltergeist, EvidenceType.SpiritBox | EvidenceType.Fingerprints | EvidenceType.GhostWriting),
			new(GhostType.Raiju, EvidenceType.EmfLevel5 | EvidenceType.GhostOrbs | EvidenceType.DotsProjector),
			new(GhostType.Revenant, EvidenceType.GhostOrbs | EvidenceType.FreezingTemperatures | EvidenceType.GhostWriting),
			new(GhostType.Shade, EvidenceType.EmfLevel5 | EvidenceType.FreezingTemperatures | EvidenceType.GhostWriting),
			new(GhostType.Spirit, EvidenceType.EmfLevel5 | EvidenceType.SpiritBox | EvidenceType.GhostWriting),
			new(GhostType.Mimic, EvidenceType.GhostOrbs | EvidenceType.SpiritBox | EvidenceType.FreezingTemperatures | EvidenceType.Fingerprints),
			new(GhostType.Twins, EvidenceType.EmfLevel5 | EvidenceType.SpiritBox | EvidenceType.FreezingTemperatures),
			new(GhostType.Wraith, EvidenceType.EmfLevel5 | EvidenceType.SpiritBox | EvidenceType.DotsProjector),
			new(GhostType.Yokai, EvidenceType.GhostOrbs | EvidenceType.SpiritBox | EvidenceType.DotsProjector),
			new(GhostType.Yure, EvidenceType.GhostOrbs | EvidenceType.FreezingTemperatures | EvidenceType.DotsProjector),
		};
	}
}
