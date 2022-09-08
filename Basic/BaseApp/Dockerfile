#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BaseApp/BaseApp.csproj", "BaseApp/"]
RUN dotnet restore "BaseApp/BaseApp.csproj"
COPY . .
WORKDIR "/src/BaseApp"
RUN dotnet build "BaseApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BaseApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BaseApp.dll"]