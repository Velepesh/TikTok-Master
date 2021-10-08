using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeAnalytics : MonoBehaviour
{
    public static AmplitudeAnalytics instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        Amplitude amplitude = Amplitude.Instance;
        amplitude.logging = true;
        amplitude.init("dfbf8c48de36208caa1ab19cf25c9097");
    }
}