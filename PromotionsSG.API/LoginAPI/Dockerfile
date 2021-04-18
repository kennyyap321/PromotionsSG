FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 
#
# copy csproj and restore as distinct layers
#COPY *.sln .
COPY LoginAPI/*.csproj ./LoginAPI/
COPY Common/*.csproj ./Common/

COPY LoginAPI/*.csproj ./
COPY Common/*.csproj ./
#COPY Experiment.Sample/*.csproj ./Experiment.Sample/
 
#
RUN dotnet restore LoginAPI.csproj
#
# copy everything else and build app
COPY LoginAPI/. ./LoginAPI/
COPY Common/. ./Common/
#COPY Experiment.Sample/. ./Experiment.Sample/ 
#
WORKDIR /app/LoginAPI
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 
#
COPY --from=build /app/LoginAPI/out ./
ENTRYPOINT ["dotnet", "LoginAPI.dll"]