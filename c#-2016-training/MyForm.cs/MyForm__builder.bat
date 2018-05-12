@set dir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\

@echo %dir%
@echo.

@%dir%\csc /target:winexe MyForm.cs

@pause
