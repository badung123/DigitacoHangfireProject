FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Digitaco.Jobs/Digitaco.Jobs.csproj Digitaco.Jobs/
RUN dotnet restore Digitaco.Jobs/Digitaco.Jobs.csproj
COPY . .
WORKDIR /src/Digitaco.Jobs
RUN dotnet build Digitaco.Jobs.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Digitaco.Jobs.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Digitaco.Jobs.dll"]
