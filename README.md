---
title: Native PC Unity
parentDoc: 64ad6456ceede10cf0b2a120
category: 6446526dddf659006c7ea807
order: 1
hidden: false
slug: unity-nativepc
---

> Link to repository  
> [GitHub](https://github.com/AppsFlyerSDK/appsflyer-native-pc-unity-sample-app)

## AppsFlyer Native PC Unity SDK integration

AppsFlyer empowers gaming marketers to make better decisions by providing powerful tools to perform cross-platform attribution.

Game attribution requires the game to integrate the AppsFlyer SDK that records first opens, consecutive sessions, and in-app events. For example, purchase events.
We recommend you use this sample app as a reference for integrating the AppsFlyer SDK into your Unity Native PC game.

**Note**: The sample code that follows is supported in a both Windows & Mac environment.

<hr/>

## AppsflyerModule - Interface

`AppsflyerModule.cs`, included in the scenes folder, contains the required code and logic to connect to AppsFlyer servers and report events.

### AppsflyerModule

This method receives your API key, App ID, the parent MonoBehaviour and a sandbox mode flag (optional, false by default) and initializes the AppsFlyer Module.

**Method signature**

```c#
AppsflyerModule(string devkey, string appid, MonoBehaviour mono, bool isSandbox = false)
```

**Arguments**:

- `DEV_KEY`: Get from the marketer or [AppsFlyer HQ](https://support.appsflyer.com/hc/en-us/articles/211719806-App-settings-#general-app-settings).
- `APP_ID`: The app id on Appsflyer HQ (excluding the `nativepc-` prefix).
- `MonoBehaviour mono`: the parent MonoBehaviour.
- `bool isSandbox`: Whether to activate sandbox mode. False by default. This option is for debugging. With the sandbox mode, AppsFlyer dashboard does not show the data. 

**Usage**:

```c#
// for regular init
AppsflyerModule afm = new AppsflyerModule(<< DEV_KEY >>, << APP_ID >>, this);

// for init in sandbox mode (reports the events to the sandbox endpoint)
AppsflyerModule afm = new AppsflyerModule(<< DEV_KEY >>, << APP_ID >>, this, true);
```

### Start

This method sends first open/session requests to AppsFlyer.

**Method signature**

```c#
void Start(bool skipFirst = false)
```

**Usage**:

```c#
// without the flag
afm.Start();

// with the flag
bool skipFirst = [SOME_CONDITION];
afm.Start(skipFirst);
```


### Stop

This method stops the SDK from functioning and communicating with AppsFlyer servers. It's used when implementing user opt-in/opt-out.

**Method signature**

```c#
void Stop()
```

**Usage**:

```c#
// Starting the SDK
afm.Start();
// ...
// Stopping the SDK, preventing further communication with AppsFlyer
afm.Stop();
```

### LogEvent

This method receives an event name and JSON object and sends an in-app event to AppsFlyer.

**Method signature**

```c#
void LogEvent(
      string event_name,
      Dictionary<string, object> event_parameters,
      Dictionary<string, object> event_custom_parameters = null
   )
```

**Arguments**:

- `string event_name`: the name of the event.
- `Dictionary<string, object> event_parameters`: dictionary object which contains the [predefined event parameters](https://dev.appsflyer.com/hc/docs/ctv-log-event-event-parameters).
- `Dictionary<string, object> event_custom_parameters`: (non-mandatory): dictionary object which contains the any custom event parameters.

**Usage**:

```c#
// set event name
string event_name = "af_purchase";
// set event values
Dictionary<string, object> event_parameters = new Dictionary<string, object>();
event_parameters.Add("af_currency", "USD");
event_parameters.Add("af_revenue", 12.12);
// send logEvent request
afm.LogEvent(event_name, event_parameters);

// send logEvent request with custom params
Dictionary<string, object> event_custom_parameters = new Dictionary<string, object>();
event_custom_parameters.Add("goodsName", "新人邀约购物日");
afm.LogEvent(event_name, event_parameters, event_custom_parameters);
```

### IsInstallOlderThanDate

This method receives a date string and returns true if the game folder creation date is older than the date string. The date string format is: "2023-03-01T23:12:34+00:00"

**Method signature**

```c#
bool IsInstallOlderThanDate(string datestring)
```

**Usage**:

```c#
// the creation date in this example is "2023-03-23T08:30:00+00:00"
bool newerDate = afm.IsInstallOlderThanDate("2023-06-13T10:00:00+00:00");
bool olderDate = afm.IsInstallOlderThanDate("2023-02-11T10:00:00+00:00");

// will return true
Debug.Log("newerDate:" + (newerDate ? "true" : "false"));
// will return false
Debug.Log("olderDate:" + (olderDate ? "true" : "false"));

// example usage with skipFirst -
// skipping if the install date is NOT older than the given date
bool IsInstallOlderThanDate = afm.IsInstallOlderThanDate("2023-02-11T10:00:00+00:00");
afm.Start(!IsInstallOlderThanDate);
```


### SetCustomerUserId

This method sets a customer ID that enables you to cross-reference your unique ID with the AppsFlyer unique ID and other device IDs. Note: You can only use this method before calling `Start()`.
The customer ID is available in raw data reports and in the postbacks sent via API.

**Method signature**

```c#
void SetCustomerUserId(string cuid)
```

**Usage**:

```c#
AppsflyerSteamModule afm = new AppsflyerSteamModule(DEV_KEY, STEAM_APP_ID, this);
afm.SetCustomerUserId("15667737-366d-4994-ac8b-653fe6b2be4a");
afm.Start();
```

### GetAppsFlyerUID

Get AppsFlyer's unique device ID. The SDK generates an AppsFlyer unique device ID upon app installation. When the SDK is started, this ID is recorded as the ID of the first app install.

**Method signature**

```c#
void GetAppsFlyerUID()
```

**Usage**:

```c#
AppsflyerModule afm = new AppsflyerModule(<< DEV_KEY >>, << APP_ID >>, this);
afm.Start();
string af_uid = afm.GetAppsFlyerUID();
```

## Running the sample app

1. Open Unity hub and open the project.
2. Use the sample code in AppsflyerScript.cs and update it with your DEV_KEY and APP_ID.
3. Add the AppsflyerScript to an empty game object (or use the one in the scenes folder):  
   ![Request-OK](https://files.readme.io/b271553-small-EpicGameObject.PNG)
4. Launch the sample app via the Unity editor and check that your debug log shows the following message:  
   ![Request-OK](https://files.readme.io/7105a10-small-202OK.PNG)
5. After 24 hours, the AppsFlyer dashboard updates and shows organic and non-organic installs and in-app events.

## Implementing AppsFlyer in your Native PC game

### Setup

1. Add the script from `Assets/AppsflyerModule.cs` to your app.
2. Use the sample code in `Assets/AppsflyerScript.cs` and update it with your `DEV_KEY` and `APP_ID`.
3. Initialize the SDK.

```c#
AppsflyerModule afm = new AppsflyerModule(<< DEV_KEY >>, << APP_ID >>, this);
```

6. [Start](#start) the AppsFlyer integration.
7. Report [in-app events](#logevent).

## Resetting the attribution

[Delete the PlayerPrefs data the registry/preferences folder](https://docs.unity3d.com/ScriptReference/PlayerPrefs.html), or use [PlayerPrefs.DeleteAll()](https://docs.unity3d.com/2020.1/Documentation/ScriptReference/PlayerPrefs.DeleteAll.html) when testing the attribution in the UnityEditor.
![AF guid & counter in the Windows Registry](https://files.readme.io/51b1681-image.png)
