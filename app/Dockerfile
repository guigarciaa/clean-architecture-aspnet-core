FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80

# Copy all foldes, files inside CleanArch/ and Paste in . inside of docker image.
COPY CleanArch/ .
# Restore all packages.
RUN dotnet restore "/app/CleanArch.Application/CleanArch.Application.csproj"
RUN dotnet restore "/app/CleanArch.Domain/CleanArch.Domain.csproj"
RUN dotnet restore "/app/CleanArch.Infra.Data/CleanArch.Infra.Data.csproj"
RUN dotnet restore "/app/CleanArch.Infra.IoC/CleanArch.Infra.IoC.csproj"
RUN dotnet restore "/app/CleanArch.MVC/CleanArch.MVC.csproj"

# Build Project
RUN dotnet build

# Build and Publish builded release files.
RUN dotnet publish -c Release -o /out --no-restore

# Build runtime image.
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "CleanArch.MVC.dll"]