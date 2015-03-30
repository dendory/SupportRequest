@echo off

:: This file copies the binary to the public desktop and creates the necessary Registry entry.
:: To use this with your own NodePoint installation, replace the following values with your own settings.

set NODEPOINT_URL=https://nodepoint.ca/nodepoint
set NODEPOINT_KEY=xSSjUrmRu23KVJsGLUKQW7Bw98U4HffB
set NODEPOINT_PRODUCT=5
set NODEPOINT_RELEASE=1.0

mkdir "%PROGRAMFILES%\Support Request"
copy "%~dp0supportrequest.exe" "%SYSTEMDRIVE%\Users\Public\Desktop\Support Request.exe"
reg add HKLM\Software\SupportRequest /v url /t REG_SZ /d "%NODEPOINT_URL%" /f
reg add HKLM\Software\SupportRequest /v key /t REG_SZ /d "%NODEPOINT_KEY%" /f
reg add HKLM\Software\SupportRequest /v product_id /t REG_SZ /d "%NODEPOINT_PRODUCT%" /f
reg add HKLM\Software\SupportRequest /v release_id /t REG_SZ /d "%NODEPOINT_RELEASE%" /f
