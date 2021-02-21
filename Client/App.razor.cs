using Blazorise;

namespace Melinoe.Client
{
    public partial class App
    {
        private const string ColourPrimary = "#71BB00";
        private const string ColourSecondary = "#00BBA8";
        private const string ColourWarning = "#BBA800";
        private const string ColourDanger = "#BB0014";

        private Theme Theme { get; } = new()
        {
            BackgroundOptions = new ThemeBackgroundOptions(),
            ColorOptions =
                new ThemeColorOptions
                {
                    Primary = ColourPrimary,
                    Secondary = ColourSecondary,
                    Warning = ColourWarning,
                    Danger = ColourDanger
                },
            TextColorOptions = new ThemeTextColorOptions
            {
                Primary = ColourPrimary,
                Secondary = ColourSecondary,
                Warning = ColourWarning,
                Danger = ColourDanger
            }
        };
    }
}
