@echo off

::set strLinker=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\csc.exe
set strLinker=%SystemRoot%\Microsoft.NET\Framework\v3.5\csc.exe

echo %strLinker%
echo.

set strCmd=%strLinker% /target:exe cs-sort-tiobe-filename-v3-demo-2.cs
echo #%strCmd%
%strCmd%

pause