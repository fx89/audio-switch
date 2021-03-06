// AudioSwitch.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "AudioDeviceSwitch.h"


AudioDeviceSwitch **devices = new AudioDeviceSwitch*[200];
long nDevices = 0;


long GetDeviceHandle(AudioDeviceSwitch *device) {
	for (int i = 0; i < nDevices; i++) {
		if (devices[i] == device)
			return i;
	}

	int ret = nDevices;
	nDevices++;

	devices[ret] = device;
	
	return ret;
}



extern "C" __declspec(dllexport)
long CreateAudioSwitchInstance() {
	return GetDeviceHandle(new AudioDeviceSwitch());
}


extern "C" __declspec(dllexport)
void DisposeAudioSwitchInstance(long audioDeviceSwitch) {
	delete devices[audioDeviceSwitch];
}


extern "C" __declspec(dllexport)
long SetDefaultAudioPlaybackDevice(long hInstance, char *devID) {
	int length = strlen(devID) + 1;
	LPWSTR wDevID = new wchar_t[length];
	mbstowcs(wDevID, devID, length);

	HRESULT hr = devices[hInstance]->SetDefaultAudioPlaybackDevice(wDevID);

	return hr;
}


extern "C" __declspec(dllexport)
long GetDefaultAudioPlaybackDeviceId(long hInstance, char *outId, int maxLen, int * outLen) {
	LPWSTR wOutId;
	HRESULT hr = devices[hInstance]->GetDefaultAudioPlaybackDeviceId(wOutId);
	if (SUCCEEDED(hr) == false) return hr;

	int wLen = lstrlenW(wOutId) + 1;
	wcstombs(outId, wOutId, wLen);

	*outLen = wLen - 1;

	return hr;
}


extern "C" __declspec(dllexport)
long GetAudioPlaybackDevicesCount(long hInstance, int *outDevicesCount) {
	LPWSTR * wDeviceIDs;
	LPWSTR * wDeviceNames;

	HRESULT hr = devices[hInstance]->GetAudioPlaybackDevicesInfo(wDeviceIDs, wDeviceNames, outDevicesCount);

	return hr;
}

extern "C" __declspec(dllexport)
long GetAudioPlaybackDeviceProperty(long hInstance, int deviceIndex, int propertyIndex, char * buffer, int maxLength, int * outLength) {
	LPWSTR * wDeviceIDs;
	LPWSTR * wDeviceNames;
	int * devCount = new int;

	HRESULT hr = devices[hInstance]->GetAudioPlaybackDevicesInfo(wDeviceIDs, wDeviceNames, devCount);
	if (SUCCEEDED(hr) == false) return hr;


	LPWSTR * propertyArray;
	if (propertyIndex == 1) propertyArray = wDeviceIDs;
	else propertyArray = wDeviceNames;

	int wLen = lstrlenW(propertyArray[deviceIndex]) + 1;
	if (wLen > maxLength) wLen = maxLength;
	wcstombs(buffer, propertyArray[deviceIndex], wLen);

	*outLength = wLen - 1;

	return hr;
}

extern "C" __declspec(dllexport)
void ResetAudioSwitchInstance(long hInstance) {
	devices[hInstance]->Reset();
}



/*
extern "C" __declspec(dllexport)
long GetAudioPlaybackDevicesInfo(long hInstance, char ***outDeviceIDs, char ***outDeviceNames, int *outDevicesCount) {
	LPWSTR * wDeviceIDs;
	LPWSTR * wDeviceNames;

	HRESULT hr = devices[hInstance]->GetAudioPlaybackDevicesInfo(wDeviceIDs, wDeviceNames, *outDevicesCount);
	if (SUCCEEDED(hr) == false) return hr;

	char** deviceIDs = new char*[*outDevicesCount];
	char** deviceNames = new char*[*outDevicesCount];

	for (int i = 0; i < *outDevicesCount; i++) {
		int wLen = lstrlenW(wDeviceIDs[i]) + 1;
		deviceIDs[i] = new char[wLen];
		wcstombs(deviceIDs[i], wDeviceIDs[i], wLen);

		wLen = lstrlenW(wDeviceNames[i]) + 1;
		deviceNames[i] = new char[wLen];
		wcstombs(deviceNames[i], wDeviceNames[i], wLen);
	}

	*outDeviceIDs = deviceIDs;
	*outDeviceNames = deviceNames;

	return 1;

}
*/