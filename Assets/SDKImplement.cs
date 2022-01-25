using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using Facebook.Unity;

public class SDKImplement : MonoBehaviour
{
    private void Start()
    {
        GameAnalytics.Initialize();
    }
}
