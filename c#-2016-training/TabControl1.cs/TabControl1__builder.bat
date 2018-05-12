@echo off
set dir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\
set ProjName=TabControl1

echo %dir%
echo.

rem %dir%\csc /target:winexe /resource:res\%ProjName%.resources /win32icon:res\%ProjName%.ico /optimize %ProjName%.cs
%dir%\csc /target:winexe /optimize %ProjName%.cs

pause
