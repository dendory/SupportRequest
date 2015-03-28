@echo off
%SYSTEMROOT%\Microsoft.NET\Framework\v3.5\csc.exe /target:winexe /out:..\bin\supportrequest.exe %~dp0supportrequest.cs