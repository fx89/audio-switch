/***************************************************************************************
 *                                                                                     *
 *   AudioDeviceSwitch - allows listing audio devices and changing the active device   *
 *                                                                                     *
 ***************************************************************************************/

#pragma once

class AudioDeviceSwitch {
public:
	AudioDeviceSwitch();
	~AudioDeviceSwitch();

	HRESULT SetDefaultAudioPlaybackDevice(LPCWSTR devID);

	HRESULT GetDefaultAudioPlaybackDeviceId(LPWSTR &outId);

	HRESULT GetAudioPlaybackDevicesInfo(LPWSTR * &outDeviceIDs, LPWSTR * &outDeviceNames, int * outDevicesCount);

	void Reset();


private:
	LPWSTR * audioDeviceIDs;
	LPWSTR * audioDeviceNames;
	int audioDevicesCount = 0;

	HRESULT GetDeviceEnumerator(IMMDeviceEnumerator **pEnum);

	HRESULT GetDeviceCollection(IMMDeviceCollection **pDevices);

	HRESULT CollectDeviceInfo();
};

