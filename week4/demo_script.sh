#!/bin/bash
set -o pipefail

# Session 1: Add Book
echo "=== SESSION 1: Adding a Book ==="
printf "1\nThe Great Gatsby\nF. Scott Fitzgerald\nScribner\n1925\n9\n" | dotnet run 2>&1 | tail -30

echo ""
echo "=== SESSION 2: Reopening App (Data Persists) ==="
printf "4\n9\n" | dotnet run 2>&1 | tail -20

echo ""
echo "=== SESSION 3: Adding Magazine & Newspaper ==="
printf "2\nThe New Yorker\n45\nCondé Nast\n2024\n3\nThe Washington Post\nSally Buzbee\nThe Washington Post Co.\n2024\n9\n" | dotnet run 2>&1 | tail -30

echo ""
echo "=== SESSION 4: Search, Sort & Stats ==="
printf "4\n5\n1\nGatsby\n5\n2\nFitzgerald\n6\n1\n8\n9\n" | dotnet run 2>&1 | tail -60
