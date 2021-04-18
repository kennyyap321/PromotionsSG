FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 

COPY Common/*.csproj ./
COPY PromotionsSG.API.Search/*.csproj ./

RUN dotnet restore PromotionsSG.API.Search.csproj

# copy everything else and build app
COPY Common/. ./Common/
COPY PromotionsSG.API.Search/. ./PromotionsSG.API.Search/
#
WORKDIR /app/PromotionsSG.API.Search
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 
#
COPY --from=build /app/PromotionsSG.API.Search/out ./
ENTRYPOINT ["dotnet", "PromotionsSG.API.Search.dll"]