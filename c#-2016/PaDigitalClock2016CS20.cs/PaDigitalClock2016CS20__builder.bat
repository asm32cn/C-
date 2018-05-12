@set dir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\
@set ProjName=PaDigitalClock2016CS20

@echo %dir%
@echo.

@%dir%\csc /target:winexe /res:res\%ProjName%.resources /win32icon:res\%ProjName%.ico /optimize %ProjName%.cs

@pause
