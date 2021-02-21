using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blazorise;
using Fluxor;
using Melinoe.Client.State.JoinGame;
using Microsoft.AspNetCore.Components;

namespace Melinoe.Client.Components
{
    public partial class JoinGameDialogue
    {
        private class JoinGameModel
        {
            [Required]
            [StringLength(maximumLength: 6, MinimumLength = 6, ErrorMessage = "Game code must be exactly 6 digits.")]
            public string GameCode { get; set; } = string.Empty;

            [Required]
            [StringLength(maximumLength: 20, MinimumLength = 2, ErrorMessage = "Username must be between 4 and 20 alphanumeric characters.")]
            public string UserName { get; set; } = string.Empty;
        }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        private bool CanSubmit { get; set; } = false;

        private JoinGameModel Model { get; } = new();

        private Validations _validations;
        private Validation _game_code_validator;
        private Validation _user_name_validator;

        public void Connect()
        {
            if (!_validations.ValidateAll())
                return;

            _validations.ClearAll();
            Dispatcher.Dispatch(new JoinGameAction(Model.GameCode, Model.UserName));
        }

        private void RevalidateSubmit()
        {
            CanSubmit = IsValidGameCode(Model.GameCode) && IsValidUserName(Model.UserName);
        }

        private static bool IsValidGameCode(string gameCode) => gameCode is not null && Regex.IsMatch(gameCode, @"^[0-9]{6}$");

        private static void IsValidGameCode(ValidatorEventArgs args) =>
            args.Status = IsValidGameCode(args.Value as string) 
                ? ValidationStatus.Success 
                : ValidationStatus.Error;

        private static bool IsValidUserName(string userName) => userName is not null && Regex.IsMatch(userName, @"^[a-zA-Z0-9]{4,20}$");

        private static void IsValidUserName(ValidatorEventArgs args) =>
            args.Status = IsValidUserName(args.Value as string)
                ? ValidationStatus.Success
                : ValidationStatus.Error;
    }
}
