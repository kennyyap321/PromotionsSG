FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
RUN apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev
WORKDIR /app 
#
#RUN apt-get update \
    #&& apt-get install -y --allow-unauthenticated \
        #libc6-dev \
        #libgdiplus \
        #libx11-dev \
     #&& rm -rf /var/lib/apt/lists/*

COPY Common/*.csproj ./
COPY PromotionsSG.Presentation.WebPortal/*.csproj ./

RUN dotnet restore PromotionsSG.Presentation.WebPortal.csproj

# copy everything else and build app
COPY Common/. ./Common/
COPY PromotionsSG.Presentation.WebPortal/. ./PromotionsSG.Presentation.WebPortal/
#
WORKDIR /app/PromotionsSG.Presentation.WebPortal
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
RUN apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev
WORKDIR /app 
#
COPY --from=build /app/PromotionsSG.Presentation.WebPortal/out ./
ENTRYPOINT ["dotnet", "PromotionsSG.Presentation.WebPortal.dll"]