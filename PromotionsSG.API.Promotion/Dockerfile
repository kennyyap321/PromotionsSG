FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 

COPY Common/*.csproj ./
COPY PromotionsSG.API.Promotion/*.csproj ./

RUN dotnet restore PromotionsSG.API.Promotion.csproj

# copy everything else and build app
COPY Common/. ./Common/
COPY PromotionsSG.API.Promotion/. ./PromotionsSG.API.Promotion/
#
WORKDIR /app/PromotionsSG.API.Promotion
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 
#
COPY --from=build /app/PromotionsSG.API.Promotion/out ./
ENTRYPOINT ["dotnet", "PromotionsSG.API.Promotion.dll"]