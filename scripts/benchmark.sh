#!/bin/bash
# Run selected benchmark families
# Usage: ./scripts/benchmark.sh [filter]
#
# Examples:
#   ./scripts/benchmark.sh              # Interactive menu
#   ./scripts/benchmark.sh view         # Run ViewParsingBenchmarks
#   ./scripts/benchmark.sh span         # Run SpanApiAccessBenchmarks
#   ./scripts/benchmark.sh population   # Run MessagePopulationBenchmarks
#   ./scripts/benchmark.sh allocation   # Run AllocationBreakdownBenchmarks
#   ./scripts/benchmark.sh header       # Run HeaderStringAllocationBenchmarks
#   ./scripts/benchmark.sh all          # Run all benchmarks
#   ./scripts/benchmark.sh "*Heartbeat*" # Custom filter pattern

set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_DIR="$(dirname "$SCRIPT_DIR")"
BENCHMARK_PROJECT="$PROJECT_DIR/PureFix.Benchmarks/PureFix.Benchmarks.csproj"

# Map friendly names to filter patterns
declare -A FILTERS=(
    ["view"]="*ViewParsing*"
    ["span"]="*SpanApiAccess*"
    ["population"]="*MessagePopulation*"
    ["allocation"]="*AllocationBreakdown*"
    ["header"]="*HeaderStringAllocation*"
    ["all"]="*"
)

show_menu() {
    echo "=== PureFix Benchmark Runner ==="
    echo ""
    echo "Available benchmark families:"
    echo "  1) view        - ViewParsingBenchmarks (tokenization, ~5 benchmarks)"
    echo "  2) span        - SpanApiAccessBenchmarks (zero-alloc API, ~15 benchmarks)"
    echo "  3) population  - MessagePopulationBenchmarks (message extraction, ~10 benchmarks)"
    echo "  4) allocation  - AllocationBreakdownBenchmarks (detailed breakdown, ~15 benchmarks)"
    echo "  5) header      - HeaderStringAllocationBenchmarks (string interning, ~8 benchmarks)"
    echo "  6) all         - Run all benchmarks (slow)"
    echo ""
    echo "  q) Quit"
    echo ""
    read -p "Select benchmark family (1-6, or q to quit): " choice

    case $choice in
        1) FILTER="${FILTERS[view]}" ;;
        2) FILTER="${FILTERS[span]}" ;;
        3) FILTER="${FILTERS[population]}" ;;
        4) FILTER="${FILTERS[allocation]}" ;;
        5) FILTER="${FILTERS[header]}" ;;
        6) FILTER="${FILTERS[all]}" ;;
        q|Q) echo "Exiting."; exit 0 ;;
        *) echo "Invalid choice"; exit 1 ;;
    esac
}

# Handle command line argument or show menu
if [ -n "$1" ]; then
    if [ "${FILTERS[$1]+isset}" ]; then
        FILTER="${FILTERS[$1]}"
    else
        # Treat as custom filter pattern
        FILTER="$1"
    fi
else
    show_menu
fi

echo ""
echo "=== Running benchmarks with filter: $FILTER ==="
echo ""

dotnet run --project "$BENCHMARK_PROJECT" --configuration Release -- --filter "$FILTER"
