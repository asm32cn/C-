@echo off
set cscdir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\
set proj=Form1

echo %cscdir%

echo.

%cscdir%\csc /target:winexe %proj%.cs

pause
