@echo off
set gf="C:\Program Files (x86)\Windows Kits\10\Debuggers\x64\gflags.exe"

if "%1" == "" goto printstatus

if %1 == gon goto turnonglobal
if %1 == goff goto turnoffglobal

set exe=%1

if %2 == on goto turnon
if %2 == off goto turnoff
echo ##### Doing nothing
goto done

:printstatus
call :printgflags
goto done

:turnon
echo ##### Turning on gflags for %exe%
%gf% /p /full /enable %exe%
goto done

:turnoff
echo ##### Turning off gflags for %exe%
%gf% /p /full /disable %exe%
goto done

:turnonglobal
echo ##### Enabling standard page heap verification for all processes
%gf% /r +hpa
echo ##### You should probably reboot
echo.
call :printgflags
goto done


:turnoffglobal
echo ##### Disabling standard page heap verification for all processes
%gf% /r -hpa
echo ##### You should probably reboot
echo.
call :printgflags
goto done

:printgflags
echo ##### gflags /r #####
%gf% /r
echo.
echo ##### gflags /k #####
%gf% /k
echo.
echo ##### gflags /ko #####
%gf% /ko
echo.
echo ##### gflags /p #####
%gf% /p
echo.
echo ##### gflags /ro #####
%gf% /ro
echo.
exit /b 0

:done