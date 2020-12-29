#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["HouseholdPlan.Server/HouseholdPlan.Server.csproj", "HouseholdPlan.Server/"]
COPY ["HouseholdPlan.Core/HouseholdPlan.Core.csproj", "HouseholdPlan.Core/"]
COPY ["HouseholdPlan.Data/HouseholdPlan.Data.csproj", "HouseholdPlan.Data/"]
RUN dotnet restore "HouseholdPlan.Server/HouseholdPlan.Server.csproj"
COPY . .
WORKDIR "/src/HouseholdPlan.Server"
RUN dotnet build "HouseholdPlan.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HouseholdPlan.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HouseholdPlan.Server.dll"]