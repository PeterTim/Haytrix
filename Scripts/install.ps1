dotnet publish MatrixScreenSaver.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish\
Copy-Item publish\MatrixScreenSaver.exe C:\Windows\System32\MatrixScreenSaver.scr -Force
Copy-Item publish\WebView2Loader.dll C:\Windows\System32\ -Force
