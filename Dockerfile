#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM debian:buster
#
## Set debconf to run non-interactively
#RUN echo 'debconf debconf/frontend select Noninteractive' | debconf-set-selections
#
# Install base dependencies
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base


WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

RUN apt-get update -yq 
RUN apt-get install curl gnupg -yq 
RUN curl -sL https://deb.nodesource.com/setup_18.x | bash -
RUN apt-get install -y nodejs

COPY ["./RecipeCrawler.Web/RecipeCrawler.Web.csproj", "RecipeCrawler.Web/"]
COPY ["./RecipeCrawler.Core/RecipeCrawler.Core.csproj", "RecipeCrawler.Core/"]
COPY ["./RecipeCrawler.Data/RecipeCrawler.Data.csproj", "RecipeCrawler.Data/"]
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