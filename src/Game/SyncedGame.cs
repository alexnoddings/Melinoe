using System.Diagnostics;

namespace Melinoe.Game;

public class SyncedGame
{
	[DebuggerDisplay("{Type} ({State})")]
	private sealed class Evidence : IEvidence
	{
		public EvidenceType Type { get; }
		public EvidenceState State { get; set; }

		public Evidence(EvidenceType type)
		{
			Type = type;
			State = EvidenceState.Unknown;
		}
	}

	[DebuggerDisplay("{Type} ({State}): {RequiredEvidence}")]
	private sealed class Ghost : IGhost
	{
		public GhostType Type { get; }
		public GhostState State { get; set; }
		public bool IsRuledOut { get; set; }
		public EvidenceType RequiredEvidence { get; }
		public EvidenceType AbsoluteEvidence { get; }

		public Ghost(GhostType type, EvidenceType evidence, EvidenceType absoluteEvidence = EvidenceType.None)
		{
			Type = type;
			State = GhostState.Potential;
			RequiredEvidence = evidence;
			AbsoluteEvidence = absoluteEvidence;
		}
	}

	public GameType Type { get; private set; }

	private readonly Evidence[] _evidences;
	public IEnumerable<IEvidence> Evidences => _evidences.AsEnumerable();

	private readonly Ghost[] _ghosts;
	public IEnumerable<IGhost> Ghosts => _ghosts.AsEnumerable();

	public event Func<Task>? OnUpdated;

	public SyncedGame(GameType type)
	{
		Type = type;
		_evidences = Enum.GetValues<EvidenceType>()
			.Where(evidenceType => evidenceType is not EvidenceType.None)
			.Select(evidenceType => new Evidence(evidenceType))
			.ToArray();
		_ghosts = AllGhosts.Array;
	}

	public async Task UpdateEvidenceAsync(EvidenceType type, EvidenceState newState)
	{
		var evidence = _evidences.FirstOrDefault(evidence => evidence.Type == type);
		if (evidence is null)
			return;

		evidence.State = newState;
		RecalculateGhostStates();
		if (OnUpdated is not null)
			await OnUpdated.Invoke();
	}

	public async Task UpdateIsRuledOutAsync(IGhost ghost, bool isRuledOut)
	{
		var localGhost = _ghosts.FirstOrDefault(localGhost => localGhost.Type == ghost.Type);
		if (localGhost is null)
			return;

		localGhost.IsRuledOut = isRuledOut;
		RecalculateGhostStates();
		if (OnUpdated is not null)
			await OnUpdated.Invoke();
	}

	public async Task ResetGameAsync()
	{
		foreach (var evidence in _evidences)
			evidence.State = EvidenceState.Unknown;

		foreach (var ghost in _ghosts)
			ghost.IsRuledOut = false;

		RecalculateGhostStates();
		if (OnUpdated is not null)
			await OnUpdated.Invoke();
	}

	public async Task UpdateTypeAsync(GameType newType)
	{
		Type = newType;
		RecalculateGhostStates();
		if (OnUpdated is not null)
			await OnUpdated.Invoke();
	}

	private void RecalculateGhostStates()
	{
		foreach (var ghost in _ghosts)
			ghost.State = GhostState.Potential;

		var maximumMissing = Type == GameType.Nightmare ? 1 : 0;

		EvidenceType evidenceOfType(EvidenceState state) =>
			_evidences
			.Where(evidence => evidence.State == state)
			.Aggregate(EvidenceType.None, (acc, current) => acc | current.Type);

		EvidenceType presentEvidence = evidenceOfType(EvidenceState.Present);
		EvidenceType missingEvidence = evidenceOfType(EvidenceState.Missing);

		foreach (var ghost in _ghosts)
		{
			if (ghost.IsRuledOut)
			{
				ghost.State = GhostState.NotPossible;
				continue;
			}

			// If any of the present evidence is not in a
			// ghost's required, then it cannot be that ghost
			// E.g. if the Banshee needs DOTS, Fingerprints, and Orbs,
			// and EMF is selected, then it can't be the Bahshee
			if ((ghost.RequiredEvidence & presentEvidence) != presentEvidence)
			{
				ghost.State = GhostState.NotPossible;
				continue;
			}

			// If any of the absolute evidence is not present,
			// then it cannot be that ghost
			// E.g. if the Mimic has Ghost Orbs as an absolute,
			// and Ghost Orbs are missing (even on Nightmare),
			// then it cannot be the Mimic
			if ((ghost.AbsoluteEvidence & missingEvidence) != EvidenceType.None)
			{
				ghost.State = GhostState.NotPossible;
				continue;
			}

			var missingEvidenceCount =
				Enum.GetValues<EvidenceType>()
				.Where(evidenceType => evidenceType is not EvidenceType.None)
				.Count(evidenceType =>
					ghost.RequiredEvidence.HasFlag(evidenceType)
					&& missingEvidence.HasFlag(evidenceType)
				);

			// If any ghosts have more evidence missing than maximumMissing, it's not possible
			// E.g. if the Banshee needs DOTS, Fingerprints, and Orbs, then
			// - On nightmare any one can be in the missing list, but not any more
			// - On normal, any missing means it is not possible
			if (missingEvidenceCount > maximumMissing)
				ghost.State = GhostState.NotPossible;
		}

		if (_ghosts.Count(ghost => ghost.State == GhostState.Potential) == 1)
			_ghosts.First(ghos => ghos.State == GhostState.Potential).State = GhostState.Definite;
	}

	private static class AllGhosts
	{
		public static Ghost[] Array => new Ghost[]
		{
			new(GhostType.Banshee, EvidenceType.GhostOrbs | EvidenceType.Fingerprints | EvidenceType.DotsProjector),
			new(GhostType.Demon, EvidenceType.FreezingTemperatures | EvidenceType.Fingerprints | EvidenceType.GhostWriting),
			new(GhostType.Goryo, EvidenceType.EmfLevel5 | EvidenceType.Fingerprints | EvidenceType.DotsProjector, absoluteEvidence: EvidenceType.DotsProjector),
			new(GhostType.Hantu, EvidenceType.GhostOrbs | EvidenceType.FreezingTemperatures | EvidenceType.Fingerprints, absoluteEvidence: EvidenceType.FreezingTemperatures),
			new(GhostType.Jinn, EvidenceType.EmfLevel5 | EvidenceType.FreezingTemperatures | EvidenceType.Fingerprints),
			new(GhostType.Mare, EvidenceType.GhostOrbs | EvidenceType.SpiritBox | EvidenceType.GhostWriting),
			new(GhostType.Myling, EvidenceType.EmfLevel5 | EvidenceType.Fingerprints | EvidenceType.GhostWriting),
			new(GhostType.Obake, EvidenceType.EmfLevel5 | EvidenceType.GhostOrbs | EvidenceType.Fingerprints, absoluteEvidence: EvidenceType.Fingerprints),
			new(GhostType.Oni, EvidenceType.EmfLevel5 | EvidenceType.FreezingTemperatures | EvidenceType.DotsProjector),
			new(GhostType.Onryo, EvidenceType.GhostOrbs | EvidenceType.SpiritBox | EvidenceType.FreezingTemperatures),
			new(GhostType.Phantom, EvidenceType.SpiritBox | EvidenceType.Fingerprints | EvidenceType.DotsProjector),
			new(GhostType.Poltergeist, EvidenceType.SpiritBox | EvidenceType.Fingerprints | EvidenceType.GhostWriting),
			new(GhostType.Raiju, EvidenceType.EmfLevel5 | EvidenceType.GhostOrbs | EvidenceType.DotsProjector),
			new(GhostType.Revenant, EvidenceType.GhostOrbs | EvidenceType.FreezingTemperatures | EvidenceType.GhostWriting),
			new(GhostType.Shade, EvidenceType.EmfLevel5 | EvidenceType.FreezingTemperatures | EvidenceType.GhostWriting),
			new(GhostType.Spirit, EvidenceType.EmfLevel5 | EvidenceType.SpiritBox | EvidenceType.GhostWriting),
			new(GhostType.Mimic, EvidenceType.GhostOrbs | EvidenceType.SpiritBox | EvidenceType.FreezingTemperatures | EvidenceType.Fingerprints, absoluteEvidence: EvidenceType.GhostOrbs),
			new(GhostType.Twins, EvidenceType.EmfLevel5 | EvidenceType.SpiritBox | EvidenceType.FreezingTemperatures),
			new(GhostType.Wraith, EvidenceType.EmfLevel5 | EvidenceType.SpiritBox | EvidenceType.DotsProjector),
			new(GhostType.Yokai, EvidenceType.GhostOrbs | EvidenceType.SpiritBox | EvidenceType.DotsProjector),
			new(GhostType.Yurei, EvidenceType.GhostOrbs | EvidenceType.FreezingTemperatures | EvidenceType.DotsProjector),
		};
	}
}
