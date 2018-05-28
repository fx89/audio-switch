/*
 * SOURCE: http://www.daveamenta.com/2011-05/programmatically-or-command-line-change-the-default-sound-playback-device-in-windows-7/
 */

#include "stdafx.h"
#include "AudioDeviceSwitch.h"
#include "IPolicyConfig.h"


AudioDeviceSwitch::AudioDeviceSwitch() { }

AudioDeviceSwitch::~AudioDeviceSwitch() {
	this->Reset();
}


void AudioDeviceSwitch::Reset() {
	this->audioDevicesCount = 0;
	delete this->audioDeviceIDs;
	delete this->audioDeviceNames;
}

HRESULT AudioDeviceSwitch::SetDefaultAudioPlaybackDevice(LPCWSTR devID) {
	IPolicyConfig *pPolicyConfig;
	ERole reserved = eConsole;

	HRESULT hr = CoCreateInstance(__uuidof(CPolicyConfigClient), NULL, CLSCTX_ALL, __uuidof(IPolicyConfig), (LPVOID *)&pPolicyConfig);

	if (SUCCEEDED(hr)) {
		hr = pPolicyConfig->SetDefaultEndpoint(devID, reserved);
		pPolicyConfig->Release();
	}

	return hr;
}


HRESULT AudioDeviceSwitch::GetDefaultAudioPlaybackDeviceId(LPWSTR &outId) {
	IMMDeviceEnumerator *pEnum = NULL;

	HRESULT hr = this->GetDeviceEnumerator(&pEnum);
	if (SUCCEEDED(hr) == FALSE) return -1;

	IMMDevice * pDevice;
	hr = pEnum->GetDefaultAudioEndpoint(eRender, eConsole, &pDevice);
	if (SUCCEEDED(hr) == FALSE) return -1;

	hr = pDevice->GetId(&outId);

	pEnum->Release();

	return hr;
}


HRESULT AudioDeviceSwitch::GetAudioPlaybackDevicesInfo(LPWSTR * &outDeviceIDs, LPWSTR * &outDeviceNames, int * outDevicesCount) {
	HRESULT hr = 1;

	if (this->audioDevicesCount == 0) {
		hr = this->CollectDeviceInfo();
		if (SUCCEEDED(hr) == FALSE) return -1;
	}

	outDeviceIDs = this->audioDeviceIDs;
	outDeviceNames = this->audioDeviceNames;
	*outDevicesCount = this->audioDevicesCount;

	return hr;
}



// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 


HRESULT AudioDeviceSwitch::GetDeviceEnumerator(IMMDeviceEnumerator **pEnum) {
	HRESULT hr = CoInitialize(NULL);
	if (SUCCEEDED(hr) == FALSE) return -1;

	hr = CoCreateInstance(__uuidof(MMDeviceEnumerator), NULL, CLSCTX_ALL, __uuidof(IMMDeviceEnumerator), (void**)pEnum);
	return hr;
}


HRESULT AudioDeviceSwitch::GetDeviceCollection(IMMDeviceCollection **pDevices) {
	IMMDeviceEnumerator *pEnum = NULL;

	HRESULT hr = this->GetDeviceEnumerator(&pEnum);
	if (SUCCEEDED(hr) == FALSE) return -1;

	hr = pEnum->EnumAudioEndpoints(eRender, DEVICE_STATE_ACTIVE, pDevices);

	return hr;
}


HRESULT AudioDeviceSwitch::CollectDeviceInfo() {
	IMMDeviceCollection * pDevices;
	HRESULT hr = this->GetDeviceCollection(&pDevices);

	UINT count;
	hr = pDevices->GetCount(&count);
	if (SUCCEEDED(hr) == FALSE) return -1;

	this->audioDeviceIDs = new LPWSTR[count];
	this->audioDeviceNames = new LPWSTR[count];

	for (int i = 0; i < count; i++) {
		IMMDevice *pDevice;
		hr = pDevices->Item(i, &pDevice);
		if (SUCCEEDED(hr) == FALSE) return -1;

		LPWSTR wstrID = NULL;
		hr = pDevice->GetId(&wstrID);
		if (SUCCEEDED(hr) == FALSE) return -1;

		this->audioDeviceIDs[i] = wstrID;

		IPropertyStore *pStore;
		hr = pDevice->OpenPropertyStore(STGM_READ, &pStore);
		if (SUCCEEDED(hr) == FALSE) return -1;

		PROPVARIANT friendlyName;
		PropVariantInit(&friendlyName);
		hr = pStore->GetValue(PKEY_Device_FriendlyName, &friendlyName);
		if (SUCCEEDED(hr) == FALSE) return -1;

		this->audioDeviceNames[i] = friendlyName.pwszVal;
	}

	this->audioDevicesCount = count;

	pDevices->Release();

	return 1;
}