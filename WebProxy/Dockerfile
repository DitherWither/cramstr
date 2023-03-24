FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebProxy/WebProxy.csproj", "WebProxy/"]
RUN dotnet restore "WebProxy/WebProxy.csproj"
COPY . .
WORKDIR "/src/WebProxy"
RUN dotnet build "WebProxy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebProxy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebProxy.dll"]
