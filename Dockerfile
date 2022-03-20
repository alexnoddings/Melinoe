FROM mcr.microsoft.com/dotnet/aspnet:7.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-focal AS build
WORKDIR /src
COPY ["src/Melinoe.csproj", "src/"]
RUN dotnet restore "src/Melinoe.csproj"
COPY . .
WORKDIR "/src/src"
RUN dotnet build "Melinoe.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Melinoe.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Melinoe.dll"]
