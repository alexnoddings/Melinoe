using System;
using Microsoft.AspNetCore.SignalR;

namespace Melinoe.Client.Extensions
{
    public static class HubExceptionExtensions
    {
        private const string ErrorMessageStartFlag = "HubException: ";

        public static string GetErrorMessage(this HubException hubException)
        {
            // SignalR's HubExceptions' messages are formatted as "An unexpected error occurred invoking 'Method' on the server. HubException: ...".
            // This strips the first part, getting the hub's actual error message.
            int errorMessageStart = hubException.Message.IndexOf(ErrorMessageStartFlag, StringComparison.OrdinalIgnoreCase) + ErrorMessageStartFlag.Length;
            return hubException.Message.Substring(errorMessageStart);
        }
    }
}
