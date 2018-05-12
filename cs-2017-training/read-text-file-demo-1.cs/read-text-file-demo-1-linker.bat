@echo off

set strLinker=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\csc.exe
set strAppName=read-text-file-demo-1

echo %strLinker%
echo.

set strCmd=%strLinker% /target:winexe /resource:res\%strAppName%.resources /win32icon:res\%strAppName%.ico /optimize %strAppName%.cs
set strCmd=%strLinker% /target:winexe /optimize %strAppName%.cs

echo #%strCmd%
%strCmd%

pause
