#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RecipeCrawler.Web/RecipeCrawler.Web.csproj", "RecipeCrawler.Web/"]
COPY ["RecipeCrawler.Core/RecipeCrawler.Core.csproj", "RecipeCrawler.Core/"]
COPY ["RecipeCrawler.Data/RecipeCrawler.Data.csproj", "RecipeCrawler.Data/"]
RUN dotnet restore "RecipeCrawler.Web/RecipeCrawler.Web.csproj"
COPY . .
WORKDIR "/src/RecipeCrawler.Web"
RUN dotnet build "RecipeCrawler.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RecipeCrawler.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RecipeCrawler.Web.dll"]