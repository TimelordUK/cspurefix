#!/bin/bash
# Run tests with code coverage and generate HTML report
# Usage: ./scripts/coverage.sh

set -e

echo "=== Running tests with coverage ==="
dotnet test PureFix.Test.ModularTypes/PureFix.Test.ModularTypes.csproj \
  --configuration Release \
  --verbosity normal \
  --collect:"XPlat Code Coverage" \
  --results-directory ./coverage \
  -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Exclude="[PureFix.Types.FIX*]*"

echo ""
echo "=== Installing ReportGenerator (if needed) ==="
dotnet tool install -g dotnet-reportgenerator-globaltool 2>/dev/null || true

echo ""
echo "=== Generating HTML report ==="
~/.dotnet/tools/reportgenerator \
  -reports:"./coverage/**/coverage.cobertura.xml" \
  -targetdir:"./coverage/report" \
  -reporttypes:"Html;TextSummary" \
  -assemblyfilters:"-PureFix.Types.FIX*;-PureFix.Types.FIX44UnitTest"

echo ""
echo "=== Coverage Summary ==="
cat ./coverage/report/Summary.txt

echo ""
echo "Full HTML report: ./coverage/report/index.html"
