@set dir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\
@set ProjectName=PaDisplayHZ2016CS20

@echo %dir%
@echo.

@rem %dir%\csc /target:winexe %ProjectName%.cs
@%dir%\csc /target:winexe /win32icon:res\%ProjectName%.ico /resource:res\%ProjectName%.resources %ProjectName%.cs

@pause
