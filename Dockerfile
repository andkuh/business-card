# Define build-time arguments
ARG FeedName
ARG UserName
ARG PersonalAccessToken
ARG FeedPath

# Use the build-time arguments in the Dockerfile
ENV FEED_NAME=$FeedName
ENV USERNAME=$UserName
ENV ACCESS_TOKEN=$PersonalAccessToken
ENV FEED_PATH=$FeedPath

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

RUN dotnet add source FEED_PATH --name FEED_NAME --username USERNAME --password ACCESS_TOKEN

WORKDIR /src
COPY ["BusinessCard.csproj", "./"]
RUN dotnet restore "BusinessCard.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "BusinessCard.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BusinessCard.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BusinessCard.dll"]
