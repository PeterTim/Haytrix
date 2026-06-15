# Matrix Screen Saver

A Matrix-style screensaver built with .NET 9 WinForms + WebView2. Features falling characters from the Armenian alphabet instead of the classic Japanese katakana.

## Requirements

- Windows 10/11
- [.NET SDK 9+](https://dotnet.microsoft.com/download) (for building)
- Microsoft Edge or [WebView2 Runtime](https://developer.microsoft.com/microsoft-edge/webview2/) (usually already installed)

## Build (Publish)

```powershell
dotnet publish MatrixScreenSaver.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish\
```

The `publish\` folder will contain two required files:

| File | Purpose |
|---|---|
| `MatrixScreenSaver.exe` | Main application |
| `WebView2Loader.dll` | Native WebView2 library |

The remaining files (`.pdb`, `.xml`, `runtimes\` folder) can be deleted.

## Installation

### Step 1 — Copy files to System32

```powershell
Copy-Item publish\MatrixScreenSaver.exe C:\Windows\System32\MatrixScreenSaver.scr -Force
Copy-Item publish\WebView2Loader.dll    C:\Windows\System32\ -Force
```

> The `.scr` extension is the Windows screensaver format. Both files must be in the same folder.

### Step 2 — Install via Explorer

Right-click `C:\Windows\System32\MatrixScreenSaver.scr` → **Install**

The Screen Saver Settings dialog will open with the screensaver already selected.

### Alternative: install via registry (no GUI)

```powershell
reg add "HKCU\Control Panel\Desktop" /v SCRNSAVE.EXE     /t REG_SZ /d "C:\Windows\System32\MatrixScreenSaver.scr" /f
reg add "HKCU\Control Panel\Desktop" /v ScreenSaveActive  /t REG_SZ /d "1" /f
reg add "HKCU\Control Panel\Desktop" /v ScreenSaveTimeOut /t REG_SZ /d "300" /f
```

`ScreenSaveTimeOut` — idle time in seconds before the screensaver starts (300 = 5 minutes).

## Uninstall

```powershell
Remove-Item C:\Windows\System32\MatrixScreenSaver.scr -Force
Remove-Item C:\Windows\System32\WebView2Loader.dll -Force
Remove-Item $env:TEMP\MatrixScreenSaverWebView2 -Recurse -Force
```
