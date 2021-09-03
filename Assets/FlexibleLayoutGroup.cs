using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleLayoutGroup : MonoBehaviour
{
    float defaultRatio16_9 = 1.777133f;
    GridLayoutGroup layout;
    // Start is called before the first frame update
    void Start()
    {
        layout = GetComponent<GridLayoutGroup>();
        layout.cellSize /= Camera.main.aspect / defaultRatio16_9;
        layout.padding.left = (int)(layout.padding.left / (Camera.main.aspect / defaultRatio16_9));
        layout.padding.right = (int)(layout.padding.left / (Camera.main.aspect / defaultRatio16_9));
        layout.padding.top = (int)(layout.padding.left / (Camera.main.aspect / defaultRatio16_9));
        layout.padding.bottom = (int)(layout.padding.left / (Camera.main.aspect / defaultRatio16_9));
        layout.spacing /= Camera.main.aspect / defaultRatio16_9;
    }
}