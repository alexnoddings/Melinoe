using Fluxor;

namespace Melinoe.Client.State
{
    public class GameFeature : Feature<GameState>
    {
        public override string GetName() => nameof(GameFeature);

        protected override GameState GetInitialState() =>
            GameState.Disconnected();
    }
}
