#!/bin/bash
# Regenerate all FIX type assemblies
# Usage: ./regenerate-types.sh

set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

echo "Building PureFix.App..."
dotnet build PureFix.App -c Release -v q

APP="dotnet run --project PureFix.App -c Release --no-build --"

# Function to fix csproj paths after generation
fix_csproj() {
    local project_name=$1
    local csproj="PureFix.Types.${project_name}/PureFix.Types.${project_name}.csproj"
    if [[ -f "$csproj" ]]; then
        echo "  Fixing $csproj paths..."
        sed -i 's|"\.\./\.\./PureFix|"../PureFix|g' "$csproj"
    fi
}

# Core dictionaries to generate
declare -A DICTS=(
    ["FIX43"]="Data/FIX43.xml"
    ["FIX44"]="Data/FIX44.xml"
    ["FIX50SP2"]="Data/FIX50SP2.xml"
)

for name in "${!DICTS[@]}"; do
    dict="${DICTS[$name]}"
    echo ""
    echo "=== Generating types for $dict ==="
    $APP --generate --dict "$dict" --path .
    fix_csproj "$name"
done

# Add FIX43 to solution if not already there
if ! grep -q "PureFix.Types.FIX43" cspurefix.sln 2>/dev/null; then
    echo ""
    echo "=== Adding FIX43 to solution ==="
    dotnet sln add PureFix.Types.FIX43/PureFix.Types.FIX43.csproj 2>/dev/null || true
fi

echo ""
echo "=== Rebuilding solution ==="
dotnet build -v q

echo ""
echo "=== Running tests ==="
dotnet test --no-build -v q

echo ""
echo "Done! All types regenerated successfully."
