FROM microsoft/dotnet:2.2-sdk AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
ARG Configuration=Release
WORKDIR /src
COPY *.sln ./
COPY SalaryComparer/SalaryComparer.csproj SalaryComparer/
RUN dotnet restore
COPY . .
WORKDIR /src/SalaryComparer
RUN dotnet build -c $Configuration -o /app

FROM build AS publish
ARG Configuration=Release
RUN dotnet publish -c $Configuration -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SalaryComparer.dll"]
