FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source/greenhouse_app

COPY . .
RUN dotnet restore -r linux-arm

RUN dotnet publish -c release -o /app -r linux-arm --self-contained false --no-restore

FROM mcr.microsoft.com/dotnet/runtime:6.0-bullseye-slim-arm32v7
WORKDIR /app
COPY --from=build /app/ .
ENTRYPOINT ["dotnet", "greenhouse_app.dll"]


