using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticPlayer : MonoBehaviour
{
    [SerializeField] private List<HapticSource> haptics;

    private HapticSource currentHapticSource => haptics[playNum % haptics.Count];

    private int playNum = 0;

    private void Reset()
    {
        haptics.AddRange(GameObject.FindObjectsOfType<HapticSource>());
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Play");
            currentHapticSource.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Stop");
            currentHapticSource.Stop();
            playNum++;
        }
    }
}