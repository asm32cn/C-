@echo off
rem socket-webserver-demo-1-cs--builder.bat

set strLinker=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\csc.exe
set strAppName=socket-webserver-demo-1

echo %strLinker%
echo.

%strLinker% /target:exe %strAppName%.cs

pause