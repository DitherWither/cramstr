FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BlazorWasmUI/BlazorWasmUI.csproj", "BlazorWasmUI/"]
RUN dotnet restore "BlazorWasmUI/BlazorWasmUI.csproj"
COPY . .
WORKDIR "/src/BlazorWasmUI"
RUN dotnet build "BlazorWasmUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorWasmUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorWasmUI.dll"]
