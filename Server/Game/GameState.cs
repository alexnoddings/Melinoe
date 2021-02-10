using System.Collections.Generic;
using Melinoe.Shared.Evidence;
using Melinoe.Shared.Ghosts;
using Melinoe.Shared.Objectives;

namespace Melinoe.Server.Game
{
    public record GameState
    {
        public string GameCode { get; init; } = string.Empty;
        public List<GameUser> Users { get; init; } = new();

        public GhostFirstName? FirstName { get; init; } = null;
        public GhostLastName? LastName { get; init; } = null;
        public Objective Objectives { get; init; } = (Objective)0;

        public Dictionary<EvidenceType, EvidenceState> EvidenceStates { get; init; } = new();
        public List<EvidencePossibility> EvidencePossibilities { get; init; } = new();
        public List<GhostPossibility> GhostPossibilities { get; init; } = new();
    }
}
