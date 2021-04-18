FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 

COPY Common/*.csproj ./
COPY PromotionsSG.API.Login/*.csproj ./

RUN dotnet restore PromotionsSG.API.Login.csproj

# copy everything else and build app
COPY Common/. ./Common/
COPY PromotionsSG.API.Login/. ./PromotionsSG.API.Login/
#
WORKDIR /app/PromotionsSG.API.Login
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 
#
COPY --from=build /app/PromotionsSG.API.Login/out ./
ENTRYPOINT ["dotnet", "PromotionsSG.API.Login.dll"]