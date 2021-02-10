namespace Melinoe.Client.State.JoinGame
{
    public class JoinGameAction
    {
        public string Code { get; }
        public string UserName { get; }

        public JoinGameAction(string code, string userName)
        {
            Code = code;
            UserName = userName;
        }
    }
}
