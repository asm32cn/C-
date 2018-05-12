@set dir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\
@set ProjName=NotifyIcon1CS

@echo %dir%
@echo.

@%dir%csc /target:winexe %ProjName%.cs

@pause
