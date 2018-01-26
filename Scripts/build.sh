#! /bin/sh

# Example build script for Unity3D project. See the entire example: https://github.com/JonathanPorta/ci-build

# Change this the name of your project. This will be the name of the final executables as well.
project="VRHackspace"

unity_bin="/Applications/Unity/Unity.app/Contents/MacOS/Unity"
#unity_bin="/c/Program Files/Unity/Hub/Editor/2017.3.0f3/Editor/Unity.exe"

echo $unity_bin

rm "$(pwd)/unity.log" -f

echo "Attempting to build $project for Windows"
"$unity_bin" \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile "$(pwd)/unity.log" \
  -projectPath "$(pwd)" \
  -buildWindowsPlayer "$(pwd)/Build/Windows/$project.exe" \
  -quit

echo "Attempting to build $project for OS X"
"$unity_bin" \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile "$(pwd)/unity.log" \
  -projectPath "$(pwd)" \
  -buildOSXUniversalPlayer "$(pwd)/Build/OSX/$project.app" \
  -quit

echo "Attempting to build $project for Linux"
"$unity_bin" \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile "$(pwd)/unity.log" \
  -projectPath "$(pwd)" \
  -buildLinuxUniversalPlayer "$(pwd)/Build/Linux/$project.exe" \
  -quit
  
echo 'Logs from build'
cat "$(pwd)/unity.log"