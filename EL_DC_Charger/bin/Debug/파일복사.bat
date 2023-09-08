@ECHO OFF
taskkill /im EL_DC_Charger.exe
md C:\EL_DC_Charger
md C:\EL_DC_Charger\application
del C:\EL_DC_Charger\application /s /f /q
xcopy *.* C:\EL_DC_Charger\application\ /e /h /k /y
exit