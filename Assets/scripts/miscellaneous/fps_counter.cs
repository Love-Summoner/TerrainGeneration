using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fps_counter : MonoBehaviour
{
    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    private float time = 0;
    void Update()
    {
        time += Time.unscaledDeltaTime;
        if (time > .5f)
        {
            text.text = ((int)(1 / Time.unscaledDeltaTime)).ToString() + " fps";
            time = 0;
        }
    }
}
