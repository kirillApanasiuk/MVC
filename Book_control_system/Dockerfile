#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Book_control_system/Book_control_system.csproj", "Book_control_system/"]
RUN dotnet restore "Book_control_system/Book_control_system.csproj"
COPY . .
WORKDIR "/src/Book_control_system"
RUN dotnet build "Book_control_system.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Book_control_system.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Book_control_system.dll"]