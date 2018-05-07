@set dir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\
@set ProjName=PaGernateVB6Project2016CS20

@echo %dir%
@echo.

@rem %dir%\csc /target:winexe /win32icon:%ProjName%.ico /optimize %ProjName%.cs
@%dir%\csc /target:winexe /resource:res\%ProjName%.resources /optimize %ProjName%.cs

@pause
