using UnityEngine;
using System.Collections;
using System.Text;
using System;
using System.Collections.Generic;

public class AppsflyerScript : MonoBehaviour
{
    public string DEV_KEY;
    public string APP_ID;
    public bool IS_SANDBOX;

    void Start()
    {
        AppsflyerModule afm = new AppsflyerModule(DEV_KEY, APP_ID, this, IS_SANDBOX);
        afm.SetCustomerUserId("testTEST12345");
        afm.Start();
        afm.SetCustomerUserId("testTESTNOPE");
        afm.Stop();

        // set event name
        string event_name = "af_purchase";
        // set event values
        Dictionary<string, object> event_parameters = new Dictionary<string, object>();
        event_parameters.Add("af_currency", "USD");
        event_parameters.Add("af_price", 6.66);
        event_parameters.Add("af_revenue", 12.12);
        // send logEvent request
        afm.LogEvent(event_name, event_parameters);

        // the creation date in this example is "2023-03-23T08:30:00+00:00"
        bool newerDate = afm.IsInstallOlderThanDate("2023-06-13T10:00:00+00:00");
        bool olderDate = afm.IsInstallOlderThanDate("2023-02-11T10:00:00+00:00");

        // will return true
        Debug.Log("newerDate:" + (newerDate ? "true" : "false"));
        // will return false
        Debug.Log("olderDate:" + (olderDate ? "true" : "false"));
    }

    private void Update() { }
}
