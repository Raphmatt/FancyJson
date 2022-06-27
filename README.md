# FancyJson
Just some random Project that I made on the weekend for a school presentation about data in c#.

# Building
What is needed:
* windows, linux or macOS (I only testet windows & macOS)
* .NET Core 5

Cloning the repository
```
git clone https://github.com/Raphmatt/FancyJson.git
cd FancyJson
```

Building the project to a single file
```
dotnet publish -o out -r win-x64 --self-contained false -p:PublishSingleFile=true
```
Running the program
```
.\out\FancyJson.exe
```
