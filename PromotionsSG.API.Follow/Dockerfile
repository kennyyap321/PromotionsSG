FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 

COPY Common/*.csproj ./
COPY PromotionsSG.API.Follow/*.csproj ./

RUN dotnet restore PromotionsSG.API.Follow.csproj

# copy everything else and build app
COPY Common/. ./Common/
COPY PromotionsSG.API.Follow/. ./PromotionsSG.API.Follow/
#
WORKDIR /app/PromotionsSG.API.Follow
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 
#
COPY --from=build /app/PromotionsSG.API.Follow/out ./
ENTRYPOINT ["dotnet", "PromotionsSG.API.Follow.dll"]