FROM microsoft/dotnet:2.2-sdk AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
ARG Configuration=Release
WORKDIR /src
COPY *.sln ./
COPY SalaryComparer/SalaryComparer.csproj SalaryComparer/
COPY SalaryComparer.Core/SalaryComparer.Core.csproj SalaryComparer.Core/
COPY SalaryComparer.Core.Tests/SalaryComparer.Core.Tests.csproj SalaryComparer.Core.Tests/
RUN dotnet restore /nologo
COPY . ./
WORKDIR /src/SalaryComparer
RUN dotnet build --configuration $Configuration --output /app --no-restore /nologo

FROM build AS test
WORKDIR /src
ARG Configuration=Release
RUN dotnet test --configuration $Configuration --no-restore /nologo

FROM build AS publish
ARG Configuration=Release
RUN dotnet publish --configuration $Configuration --output /app --no-restore /nologo

FROM base AS final
WORKDIR /app
COPY --from=publish /app ./
ENTRYPOINT ["dotnet", "SalaryComparer.dll"]
