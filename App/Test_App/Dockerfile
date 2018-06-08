FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Test_App/Test_App.csproj Test_App/
RUN dotnet restore Test_App/Test_App.csproj
COPY . .
WORKDIR /src/Test_App
RUN dotnet build Test_App.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Test_App.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Test_App.dll"]