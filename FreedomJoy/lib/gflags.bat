@set exe=%1
@set gf="C:\Program Files (x86)\Windows Kits\10\Debuggers\x64\gflags.exe"

@if %2 == on goto turnon
@if %2 == off goto turnoff
@echo ##### Doing nothing
@goto done

:turnon
@echo ##### Turning on gflags for %exe%
@%gf% /p /full /enable %exe%
@goto done

:turnoff
@echo ##### Turning off gflags for %exe%
@%gf% /p /full /disable %exe%
@goto done

:done