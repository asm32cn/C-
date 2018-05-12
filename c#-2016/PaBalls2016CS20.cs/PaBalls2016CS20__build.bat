@set dir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\
@set ProjectName=PaBalls2016CS20

@echo %dir%
@echo.

@%dir%\csc /target:winexe /resource:res\%ProjectName%.resources /win32icon:res\%ProjectName%.ico /optimize %ProjectName%.cs
@rem %dir%\csc /target:winexe %ProjectName%.cs

@pause
