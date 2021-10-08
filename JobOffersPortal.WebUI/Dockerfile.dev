#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["JobOffersPortal.WebUI/WebUI.csproj", "JobOffersPortal.WebUI/"]
COPY ["JobOffersPortal.Application/Application.csproj", "JobOffersPortal.Application/"]
COPY ["JobOffersPortal.Domain/Domain.csproj", "JobOffersPortal.Domain/"]
COPY ["JobOffersPortal.Infrastructure/Infrastructure.csproj", "JobOffersPortal.Infrastructure/"]
RUN dotnet restore "JobOffersPortal.WebUI/WebUI.csproj"
COPY . .
WORKDIR "/src/JobOffersPortal.WebUI"
RUN dotnet build "WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebUI.dll"]