@set dir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\
@set ProjName=PaDoListBookChapter2016CS

@echo %dir%
@echo.

@%dir%\csc /target:winexe /win32icon:%ProjName%.ico /optimize %ProjName%.cs

@pause
