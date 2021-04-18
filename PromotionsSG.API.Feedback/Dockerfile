FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 

COPY Common/*.csproj ./
COPY PromotionsSG.API.Feedback/*.csproj ./

RUN dotnet restore PromotionsSG.API.Feedback.csproj

# copy everything else and build app
COPY Common/. ./Common/
COPY PromotionsSG.API.Feedback/. ./PromotionsSG.API.Feedback/
#
WORKDIR /app/PromotionsSG.API.Feedback
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 
#
COPY --from=build /app/PromotionsSG.API.Feedback/out ./
ENTRYPOINT ["dotnet", "PromotionsSG.API.Feedback.dll"]