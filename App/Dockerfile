FROM microsoft/aspnetcore-build:2.0
WORKDIR /app
COPY . .
WORKDIR /app/Test_App
RUN dotnet restore
RUN dotnet publish -c Release
WORKDIR /app/Test_App/bin/Release/netcoreapp2.0/publish/
ENTRYPOINT /bin/bash -c "dotnet Test_App.dll"
