#! /bin/sh

# Example install script for Unity3D project. See the entire example: https://github.com/JonathanPorta/ci-build

# This link changes from time to time. I haven't found a reliable hosted installer package for doing regular
# installs like this. You will probably need to grab a current link from: http://unity3d.com/get-unity/download/archive

version=$(cat ./ProjectSettings/ProjectVersion.txt | cut -d' ' -f 2)
echo "Using unity version $version"

url="https://netstorage.unity3d.com/unity/a9f86dcd79df/MacEditorInstaller/Unity-$version.pkg"

echo "Downloading from $url"
curl -o "Scripts/Unity.pkg" $url

echo "Installing Unity.pkg"
sudo installer -dumplog -package "Scripts/Unity.pkg" -target /