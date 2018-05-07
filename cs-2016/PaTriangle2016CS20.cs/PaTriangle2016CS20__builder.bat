@set dir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\
@set ProjName=PaTriangle2016CS20

@echo %dir%
@echo.

@%dir%\csc /target:winexe /resource:res\%ProjName%.resources /win32icon:res\%ProjName%.ico /optimize %ProjName%.cs
@rem %dir%\csc /target:winexe /win32icon:res\%ProjName%.ico /optimize %ProjName%.cs

@pause
