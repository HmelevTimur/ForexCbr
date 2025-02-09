# Используем образ с .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем проект
COPY ForexCbr/ForexCbr.csproj ./ForexCbr/
WORKDIR /app/ForexCbr
RUN dotnet restore

# Копируем остальные файлы
WORKDIR /app
COPY . ./ 
WORKDIR /app/ForexCbr

# Собираем и публикуем приложение
RUN dotnet publish -c Release -o /out

# Используем образ с .NET Runtime для выполнения
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./ 

# Убедимся, что программа запущена с интерактивным терминалом
ENTRYPOINT ["dotnet", "ForexCbr.dll"]
