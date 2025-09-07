@echo off
set "SSDFolder=E:\c++\IMS - Remasted\Institute - Remasted\x64\Release\Backup"
set "HDDFolder=D:\test hdd"

robocopy "%SSDFolder%" "%HDDFolder%" /MIR /FFT /Z /XA:H /W:5 /R:3
