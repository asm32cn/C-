@set dir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\
@set ProjName=PaRegexp2016Apr02CS20

@echo %dir%
@echo.

@rem %dir%\csc /target:winexe /resource:res\%ProjName%.resources %ProjName%.cs
@%dir%\csc /target:winexe %ProjName%.cs

@pause
