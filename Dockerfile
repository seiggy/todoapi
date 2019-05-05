FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ToDoApi/ToDoApi.csproj", "ToDoApi/"]
COPY ["Microsoft.Identity.Web/Microsoft.Identity.Web.csproj", "Microsoft.Identity.Web/"]
COPY ["ToDoApi.Core/ToDoApi.Core.csproj", "ToDoApi.Core/"]
COPY ["ToDoApi.InMemory/ToDoApi.InMemory.csproj", "ToDoApi.InMemory/"]
RUN dotnet restore "ToDoApi/ToDoApi.csproj"
COPY . .
WORKDIR "/src/ToDoApi"
RUN dotnet build "ToDoApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ToDoApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ToDoApi.dll"]