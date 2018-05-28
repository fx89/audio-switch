@echo off

echo The device format is [device_id]:[device_name].
echo Only the device_name has to be used in config.xml.
echo The device_id is printed for debugging purposes.

echo ============================================================================

dotnet AudioDeviceSwitchApp.dll list

echo ============================================================================

pause