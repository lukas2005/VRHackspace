#! /bin/sh

# Example build script for Unity3D project. See the entire example: https://github.com/JonathanPorta/ci-build

# Change this the name of your project. This will be the name of the final executables as well.
project="VRHackspace"

unity_bin="/Applications/Unity/Unity.app/Contents/MacOS/Unity"
#unity_bin="/c/Program Files/Unity/Hub/Editor/2017.3.0f3/Editor/Unity.exe"

rm -f "$(pwd)/unity.log"

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
  -buildOSXUniversalPlayer "$(pwd)/Build/Mac/$project.app" \
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
  
echo 'Attempting to zip builds'
zip -r "$(pwd)/Build/Linux.zip" "$(pwd)/Build/Linux/"
zip -r "$(pwd)/Build/Mac.zip" "$(pwd)/Build/Mac/"
zip -r "$(pwd)/Build/Windows.zip" "$(pwd)/Build/Windows/"
  
echo 'Logs from build'
cat "$(pwd)/unity.log"