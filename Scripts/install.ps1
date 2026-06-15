dotnet publish Haytrix.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish\
Copy-Item publish\Haytrix.exe C:\Windows\System32\Haytrix.scr -Force
Copy-Item publish\WebView2Loader.dll C:\Windows\System32\ -Force
