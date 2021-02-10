using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Melinoe.Client.Extensions
{
    public static class HubConnectionExtensions
    {
        public static HubConnection Bind<THubClient>(this HubConnection hubConnection, THubClient client)
        {
            foreach (MethodInfo hubMethod in typeof(THubClient).GetMethods())
            {
                Type[] methodParamTypes = hubMethod.GetParameters().Select(param => param.ParameterType).ToArray();
                MethodInfo? clientMethod = client.GetType().GetMethod(hubMethod.Name, methodParamTypes);
                if (clientMethod == null)
                    throw new ArgumentException($"Client {client.GetType()} does not provide method {hubMethod.Name} ({string.Join(", ", methodParamTypes.Select(t => t.Name))})");

                Type returnType = clientMethod.ReturnType;
                Func<object?[], Task> handler;
                if (returnType == typeof(Task) || returnType == typeof(Task<>))
                    handler = parameters => (Task) clientMethod.Invoke(client, parameters);
                else
                    handler = parameters => { clientMethod.Invoke(client, parameters); return Task.CompletedTask; };

                hubConnection.On(clientMethod.Name, methodParamTypes, parameters => handler(parameters));
            }

            return hubConnection;
        }
    }
}
