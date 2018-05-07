@set dir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\
@set ProjName=PaTransparentForm2016CS20

@echo %dir%
@echo.

@%dir%\csc /target:winexe /resource:res\%ProjName%.resources %ProjName%.cs

@pause
