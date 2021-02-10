using System.Collections.Generic;

namespace Melinoe.Client.State.PlayersUpdated
{
    public class PlayersUpdatedAction
    {
        public List<string> Players { get; }

        public PlayersUpdatedAction(List<string> players)
        {
            Players = players;
        }
    }
}
