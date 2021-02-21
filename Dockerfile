FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Server/Melinoe.Server.csproj", "Server/"]
COPY ["Shared/Melinoe.Shared.csproj", "Shared/"]
COPY ["Client/Melinoe.Client.csproj", "Client/"]
RUN dotnet restore "Server/Melinoe.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "Melinoe.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Melinoe.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Melinoe.Server.dll"]
