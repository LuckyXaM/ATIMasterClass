FROM microsoft/dotnet:2.1-sdk as build
WORKDIR /app
COPY . .
WORKDIR /app/Test_App
RUN dotnet publish -c Release
FROM microsoft/dotnet:2.1-aspnetcore-runtime as final
COPY --from=build /app .
WORKDIR /Test_App/bin/Release/netcoreapp2.1/publish/
ENTRYPOINT ["dotnet", "Test_App.dll"]