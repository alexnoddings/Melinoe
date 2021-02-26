using System;
using Microsoft.Extensions.Localization;

namespace Melinoe.Client.Extensions
{
    public static class LocaliserExtensions
    {
        public static string Get(this IStringLocalizer localiser, object obj)
        {
            if (localiser is null)
                throw new ArgumentNullException(nameof(localiser));

            if (obj is null)
                throw new ArgumentNullException(nameof(obj));

            return localiser.GetString(obj.ToString() ?? string.Empty);
        }
    }
}
