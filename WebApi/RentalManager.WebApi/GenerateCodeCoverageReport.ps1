dotnet test --results-directory ./TestResults --collect:"XPlat Code Coverage";
reportgenerator -reports:.\TestResults\*\coverage.cobertura.xml -targetdir:CoverageReport;
rm -r ./TestResults;
./CoverageReport/index.html