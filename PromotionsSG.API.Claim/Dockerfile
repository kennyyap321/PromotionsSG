FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 

COPY Common/*.csproj ./
COPY PromotionsSG.API.Claim/*.csproj ./

RUN dotnet restore PromotionsSG.API.ClaimAPI.csproj

# copy everything else and build app
COPY Common/. ./Common/
COPY PromotionsSG.API.Claim/. ./PromotionsSG.API.ClaimAPI/
#
WORKDIR /app/PromotionsSG.API.ClaimAPI
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 
#
COPY --from=build /app/PromotionsSG.API.ClaimAPI/out ./
ENTRYPOINT ["dotnet", "PromotionsSG.API.ClaimAPI.dll"]