namespace Melinoe.Server.Game
{
    public record GameUser
    {
        public string ConnectionId { get; init; }
        public string GameCode { get; init; }
        public string UserName { get; init; }

        public GameUser(string connectionId, string gameCode, string userName)
        {
            ConnectionId = connectionId;
            GameCode = gameCode;
            UserName = userName;
        }
    }
}
