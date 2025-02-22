#!/bin/bash
target=$1
script_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
project_dir="$script_dir"
dir="$script_dir/publish/$target"

if [ -z $1 ]; then
    echo "Usage: compile <target: win-x64, linux-x64, osx-x64, osx-arm64, win-arm64>"
    exit 1
fi

echo "Starting publish for target $target in project $project_dir in output $dir"

if [ -d "$dir" ]; then
    rm -rf "$dir"
fi

mkdir "$dir"
cd "$project_dir"
dotnet publish -c Release -r $target --self-contained true -p:PublishSingleFile=true -o "$dir" -p:DebugType=None -p:DebugSymbols=false