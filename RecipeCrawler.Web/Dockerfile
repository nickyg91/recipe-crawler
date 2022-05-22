#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# set the base image to Debian
# https://www.brianchristner.io/docker-image-base-os-size-comparison/
# https://hub.docker.com/_/debian/
FROM debian:jessie-slim

# Set debconf to run non-interactively
RUN echo 'debconf debconf/frontend select Noninteractive' | debconf-set-selections

# Install base dependencies
RUN apt-get update && apt-get upgrade -y
RUN apt-get install -y -q --no-install-recommends \
        apt-transport-https \
        build-essential \
        ca-certificates \
        curl \
        git \
        libssl-dev \
        python \
        rsync \
        software-properties-common \
        devscripts \
        autoconf \
        ssl-cert \
    && apt-get clean

# update the repository sources list
# and install dependencies
RUN curl -sL https://deb.nodesource.com/setup_18.x | bash -
RUN apt-get install -y nodejs

# confirm installation
RUN node -v
RUN npm -v

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