# SalaryComparer

Simple tool to compare two salaries using Scottish tax rates for 2019/2020.

## Usage

Call the project directly:

```powershell
# dotnet run --project .\SalaryComparer <salary> <pension-contribution-rate> <employer-pension-contribution-rate> <salary> <pension-contribution-rate> <employer-pension-contribution-rate>
dotnet run --project .\SalaryComparer 20000 0.1 0.05 20000 0.2 0.05
```

Call the wrapper build script:

```powershell
# .\build.cmd <salary> <pension-contribution-rate> <employer-pension-contribution-rate> <salary> <pension-contribution-rate> <employer-pension-contribution-rate>
.\build.cmd 20000 0.1 0.05 20000 0.2 0.05
```

or

```powershell
# .\build.ps1 <salary> <pension-contribution-rate> <employer-pension-contribution-rate> <salary> <pension-contribution-rate> <employer-pension-contribution-rate>
.\build.ps1 20000 0.1 0.05 20000 0.2 0.05
```
