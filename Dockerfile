FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["EmployeeAPI/EmployeeAPI.csproj", "EmployeeAPI/"]
RUN dotnet restore "EmployeeAPI/EmployeeAPI.csproj"

COPY . .
WORKDIR "/src/EmployeeAPI"
RUN dotnet publish "EmployeeAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_PRINT_TELEMETRY_MESSAGE=false

EXPOSE 5000
ENTRYPOINT ["dotnet", "EmployeeAPI.dll"]
