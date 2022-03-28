## Ejemplo con docker

La aplicacion se creo utilizando Vue.JS para el frontend y .NET 6 para el backend.
Se trata de una simple aplicacion para crear una lista de tareas pendientes y eliminarlas conforme se van realizando.


![](C:\Users\c_oma\AppData\Roaming\marktext\images\2022-03-27-20-13-06-image.png)



---



## Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENV DOTNET_EnableDiagnostics=0
ENTRYPOINT ["dotnet", "MicroService.dll"]
```

## docker-compose.yml

```yml
version: "1.0.1"
services:
  todo:
    build: .
    image: todo_sample
    container_name: todo
    ports:
      - "3000:80"
```

## Como ejecutar

Para ejecutar solo es necesario clonar el repositorio y ejecutar el comando

```bash
docker-compose up -d
```

Esto permitira que ahora sea visible en la ruta `localhost:3000`



## [Demo](https://docker.dev.cbnao.com/)


