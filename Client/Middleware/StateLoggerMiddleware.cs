using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Melinoe.Client.Middleware
{
    public class StateLoggerMiddleware : Fluxor.Middleware
    {
        private readonly ILogger<StateLoggerMiddleware> _logger;

        public StateLoggerMiddleware(ILogger<StateLoggerMiddleware> logger)
        {
            _logger = logger;
        }

        public override void BeforeDispatch(object action) => 
            _logger.LogInformation("Dispatching {ActionName}: {ActionData}", action.GetType().Name, JsonConvert.SerializeObject(action));
    }
}
