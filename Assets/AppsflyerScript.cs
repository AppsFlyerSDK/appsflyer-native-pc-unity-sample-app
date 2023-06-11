using UnityEngine;
using System.Collections;
using System.Text;
using System;
using System.Collections.Generic;

public class AppsflyerScript : MonoBehaviour
{
    void Start()
    {
        AppsflyerModule afm = new AppsflyerModule("DEV_KEY", "APP_ID", this);
        afm.Start();

        // set event name
        string event_name = "af_purchase";
        // set event values
        Dictionary<string, object> event_parameters = new Dictionary<string, object>();
        event_parameters.Add("af_currency", "USD");
        event_parameters.Add("af_price", 6.66);
        event_parameters.Add("af_revenue", 12.12);
        // send logEvent request
        afm.LogEvent(event_name, event_parameters);

        //Get the path of the Game data folder
        string m_Path = Application.dataPath;

        //Output the Game data path to the console
        Debug.Log("dataPath : " + m_Path);
    }

    private void Update() { }
}
