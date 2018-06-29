FROM microsoft/dotnet:2.1-sdk
WORKDIR /app
COPY . .
WORKDIR /app/Test_App
RUN ls
RUN dotnet publish Test_App.csproj -c Release
WORKDIR /app/Test_App/bin/Release/netcoreapp2.1/publish/
FROM microsoft/dotnet:2.1-aspnetcore-runtime
ENTRYPOINT /bin/bash -c "dotnet Test_App.dll"
