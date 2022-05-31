FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BOTAY_console_edition/BOTAY_console_edition.csproj", "BOTAY_console_edition/"]
RUN dotnet restore "BOTAY_console_edition/BOTAY_console_edition.csproj"
COPY . .
WORKDIR "/src/BOTAY_console_edition"
RUN dotnet build "BOTAY_console_edition.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BOTAY_console_edition.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BOTAY_console_edition.dll"]
