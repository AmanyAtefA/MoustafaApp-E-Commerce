# ---------- ANGULAR BUILD ----------
FROM node:20 AS angular-build

WORKDIR /app

COPY moustafaapp.client ./moustafaapp.client

WORKDIR /app/moustafaapp.client

RUN npm install
RUN npm run build -- --configuration production


# ---------- DOTNET BUILD ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-build

WORKDIR /src

COPY MoustafaApp.Server/MoustafaApp.Server.csproj MoustafaApp.Server/
RUN dotnet restore MoustafaApp.Server/MoustafaApp.Server.csproj

COPY . .

WORKDIR /src/MoustafaApp.Server

RUN dotnet publish -c Release -o /app/publish


# ---------- FINAL IMAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=dotnet-build /app/publish .

COPY --from=angular-build /app/moustafaapp.client/dist/moustafaapp.client/browser ./wwwroot

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "MoustafaApp.Server.dll"]