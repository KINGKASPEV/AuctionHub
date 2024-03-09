#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install RabbitMQ
RUN apt-get update && apt-get install -y rabbitmq-server

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AuctionHub/AuctionHub.csproj", "AuctionHub/"]
COPY ["AuctionHub.Infrastructure/AuctionHub.Infrastructure.csproj", "AuctionHub.Infrastructure/"]
COPY ["AuctionHub.Application/AuctionHub.Application.csproj", "AuctionHub.Application/"]
COPY ["AuctionHub.Domain/AuctionHub.Domain.csproj", "AuctionHub.Domain/"]
RUN dotnet restore "AuctionHub/AuctionHub.csproj"
COPY . .
WORKDIR "/src/AuctionHub"
RUN dotnet build "AuctionHub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuctionHub.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Start RabbitMQ and your .NET application
ENTRYPOINT service rabbitmq-server start && dotnet AuctionHub.dll
#   ENTRYPOINT ["dotnet", "AuctionHub.dll"]

# See https://aka.ms/customizecontainer to learn how to customize your debug container
# and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
#WORKDIR /app
#EXPOSE 80/tcp
#EXPOSE 443/tcp
#
## Install RabbitMQ
#RUN apt-get update && apt-get install -y rabbitmq-server
#EXPOSE 5672 15672
#
#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#WORKDIR /src
#COPY ["AuctionHub/AuctionHub.csproj", "AuctionHub/"]
#COPY ["AuctionHub.Infrastructure/AuctionHub.Infrastructure.csproj", "AuctionHub.Infrastructure/"]
#COPY ["AuctionHub.Application/AuctionHub.Application.csproj", "AuctionHub.Application/"]
#COPY ["AuctionHub.Domain/AuctionHub.Domain.csproj", "AuctionHub.Domain/"]
#RUN dotnet restore "AuctionHub/AuctionHub.csproj"
#COPY . .
#WORKDIR "/src/AuctionHub"
#RUN dotnet build "AuctionHub.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "AuctionHub.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
## Start RabbitMQ in the background and then your .NET application
#ENTRYPOINT service rabbitmq-server start && dotnet AuctionHub.dll

# Use the aspnet runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

# Set the working directory inside the container
WORKDIR /app

# Expose ports 80 and 443
EXPOSE 80/tcp
EXPOSE 443/tcp

# Install RabbitMQ
RUN apt-get update && apt-get install -y rabbitmq-server

# Expose RabbitMQ ports
EXPOSE 5672 15672

# Switch to the SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory
WORKDIR /src

# Copy the project files to the container
COPY ["AuctionHub/AuctionHub.csproj", "AuctionHub/"]
COPY ["AuctionHub.Infrastructure/AuctionHub.Infrastructure.csproj", "AuctionHub.Infrastructure/"]
COPY ["AuctionHub.Application/AuctionHub.Application.csproj", "AuctionHub.Application/"]
COPY ["AuctionHub.Domain/AuctionHub.Domain.csproj", "AuctionHub.Domain/"]

# Restore NuGet packages
RUN dotnet restore "AuctionHub/AuctionHub.csproj"

# Copy the entire solution
COPY . .

# Set working directory to the project folder
WORKDIR "/src/AuctionHub"

# Build the application in Release mode
RUN dotnet build "AuctionHub.csproj" -c Release -o /app/build

# Switch to a new stage for publishing
FROM build AS publish

# Publish the application
RUN dotnet publish "AuctionHub.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Switch back to the base stage
FROM base AS final

# Set working directory
WORKDIR /app

# Copy the published files to the final image
COPY --from=publish /app/publish .

# Start RabbitMQ and your .NET application
CMD service rabbitmq-server start && dotnet AuctionHub.dll



## Use the aspnet runtime as the base image
#FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
## Install RabbitMQ
#RUN apt-get update && apt-get install -y rabbitmq-server
#
## Switch to the SDK image for building the application
#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#WORKDIR /src
#COPY ["AuctionHub/AuctionHub.csproj", "AuctionHub/"]
#COPY ["AuctionHub.Infrastructure/AuctionHub.Infrastructure.csproj", "AuctionHub.Infrastructure/"]
#COPY ["AuctionHub.Application/AuctionHub.Application.csproj", "AuctionHub.Application/"]
#COPY ["AuctionHub.Domain/AuctionHub.Domain.csproj", "AuctionHub.Domain/"]
#RUN dotnet restore "AuctionHub/AuctionHub.csproj"
#COPY . .
#WORKDIR "/src/AuctionHub"
#RUN dotnet build "AuctionHub.csproj" -c Release -o /app/build
#
## Switch to a new stage for publishing
#FROM build AS publish
#RUN dotnet publish "AuctionHub.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
## Switch back to the base stage
#FROM base AS final
#WORKDIR /app
#
## Copy the published files to the final image
#COPY --from=publish /app/publish .
#
## Start RabbitMQ and your .NET application
#CMD rabbitmq-server -detached && dotnet AuctionHub.dll


