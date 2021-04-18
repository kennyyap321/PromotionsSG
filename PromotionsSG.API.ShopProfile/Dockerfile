FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 

COPY Common/*.csproj ./
COPY PromotionsSG.API.ShopProfile/*.csproj ./

RUN dotnet restore PromotionsSG.API.ShopProfileAPI.csproj

# copy everything else and build app
COPY Common/. ./Common/
COPY PromotionsSG.API.ShopProfile/. ./PromotionsSG.API.ShopProfileAPI/
#
WORKDIR /app/PromotionsSG.API.ShopProfileAPI
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 
#
COPY --from=build /app/PromotionsSG.API.ShopProfileAPI/out ./
ENTRYPOINT ["dotnet", "PromotionsSG.API.ShopProfileAPI.dll"]