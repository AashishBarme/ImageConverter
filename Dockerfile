# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /API

COPY ImageConverter.sln  ./
COPY API/*.csproj ./API/
COPY Converter/*.csproj ./Converter/
COPY Console/*.csproj ./Console/
RUN dotnet restore 
COPY . .

WORKDIR /API
RUN dotnet build -c Release -o /publish
#RUN dotnet publish ../API/API.csproj -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
EXPOSE 5154
ENTRYPOINT ["dotnet", "API.dll"]