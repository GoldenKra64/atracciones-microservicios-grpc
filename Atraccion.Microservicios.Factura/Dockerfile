FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY */*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*} && mv $file ${file%.*}/; done
RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /src


COPY --from=build /publish .

ENV ASPNETCORE_URLS=http://+:${PORT}


ENTRYPOINT ["dotnet", "Atraccion.Microservicios.Factura.Api.dll"]