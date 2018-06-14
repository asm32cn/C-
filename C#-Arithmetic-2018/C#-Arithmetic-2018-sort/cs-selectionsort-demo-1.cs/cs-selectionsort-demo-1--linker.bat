@echo off

set strLinker=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\csc.exe

echo %strLinker%
echo.

set strCmd=%strLinker% /target:exe cs-selectionsort-demo-1.cs
echo #%strCmd%
%strCmd%

pause