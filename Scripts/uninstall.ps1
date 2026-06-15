Remove-Item C:\Windows\System32\MatrixScreenSaver.scr -Force
Remove-Item C:\Windows\System32\WebView2Loader.dll -Force
Remove-Item $env:TEMP\MatrixScreenSaverWebView2 -Recurse -Force
