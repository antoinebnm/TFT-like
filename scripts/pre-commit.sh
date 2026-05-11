#!/bin/sh

echo "Running formatter..."

dotnet format Assembly-CSharp.csproj

if [ $? -ne 0 ]; then
    echo "Formatting failed."
    exit 1
fi

git add .

echo "Pre-commit complete."