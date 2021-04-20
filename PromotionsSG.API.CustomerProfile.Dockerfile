FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 

COPY Common/*.csproj ./
COPY PromotionsSG.API.CustomerProfile/*.csproj ./

RUN dotnet restore PromotionsSG.API.CustomerProfile.csproj

# copy everything else and build app
COPY Common/. ./Common/
COPY PromotionsSG.API.CustomerProfile/. ./PromotionsSG.API.CustomerProfile/
#
WORKDIR /app/PromotionsSG.API.CustomerProfile
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 
#
COPY --from=build /app/PromotionsSG.API.CustomerProfile/out ./
ENTRYPOINT ["dotnet", "PromotionsSG.API.CustomerProfile.dll"]