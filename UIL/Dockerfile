FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UIL/UIL.csproj", "UIL/"]
RUN dotnet restore "UIL/UIL.csproj"
COPY . .
WORKDIR "/src/UIL"
RUN dotnet build "UIL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UIL.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UIL.dll"]
