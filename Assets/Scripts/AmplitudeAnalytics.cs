using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeAnalytics : MonoBehaviour
{
    public static AmplitudeAnalytics instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        Amplitude amplitude = Amplitude.Instance;
        amplitude.logging = true;
        amplitude.init("f867d882ed16a21add8269fd3418fb4c");
    }
}