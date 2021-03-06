FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN ["rm", "/etc/localtime"]
RUN ["ln", "-s", "/usr/share/zoneinfo/America/Sao_Paulo", "/etc/localtime"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY ["src/Mimic.API/Mimic.WebApi.csproj", "src/Mimic.API/"]
COPY ["src/Mimic.Application/Mimic.Application.csproj", "src/Mimic.Application/"]
COPY ["src/Mimic.Domain/Mimic.Domain.csproj", "src/Mimic.Domain/"]
COPY ["src/Mimic.Infra.Data/Mimic.Infra.Data.csproj", "src/Mimic.Infra.Data/"]

RUN dotnet restore src/Mimic.API/Mimic.WebApi.csproj
COPY . .
WORKDIR /src/src/Mimic.API
RUN dotnet build Mimic.WebApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Mimic.WebApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Mimic.WebApi.dll"]