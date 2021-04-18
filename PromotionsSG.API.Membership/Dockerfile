FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 

COPY Common/*.csproj ./
COPY PromotionsSG.API.Membership/*.csproj ./

RUN dotnet restore PromotionsSG.API.Membership.csproj

# copy everything else and build app
COPY Common/. ./Common/
COPY PromotionsSG.API.Membership/. ./PromotionsSG.API.Membership/
#
WORKDIR /app/PromotionsSG.API.Membership
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 
#
COPY --from=build /app/PromotionsSG.API.Membership/out ./
ENTRYPOINT ["dotnet", "PromotionsSG.API.Membership.dll"]