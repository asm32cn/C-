@echo off

rem socket-webserver-demo-2-cs--builder.bat

set strLinker=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\csc.exe
set strAppName=socket-webserver-demo-2

echo %strLinker%
echo.

%strLinker% /target:exe /optimize %strAppName%.cs

pause