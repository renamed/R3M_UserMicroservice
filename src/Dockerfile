FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY R3M_User_WebApi/R3M_User_WebApi.csproj R3M_User_WebApi/R3M_User_WebApi.csproj
COPY /R3M_User_ApiModels/R3M_User_ApiModels.csproj R3M_User.ApiModels/R3M_User_ApiModels.csproj
COPY /R3M_User_App/R3M_User_App.csproj R3M_User_App/R3M_User_App.csproj
COPY /R3M_User_Domain/R3M_User_Domain.csproj R3M_User_Domain/R3M_User_Domain.csproj
COPY /R3M_User_ExternalServices/R3M_User_ExternalServices.csproj R3M_User_ExternalServices/R3M_User_ExternalServices.csproj
COPY /R3M_User_Services/R3M_User_Services.csproj R3M_User_Services/R3M_User_Services.csproj


RUN dotnet restore R3M_User_WebApi/R3M_User_WebApi.csproj

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out R3M_User_WebApi/R3M_User_WebApi.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "R3M_User_WebApi.dll"]