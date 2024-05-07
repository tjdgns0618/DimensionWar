using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHighPass : MonoBehaviour
{
    public AudioHighPassFilter audioHighPassFilter;

    private void OnEnable()
    {
        audioHighPassFilter.enabled = true;
    }

    private void OnDisable()
    {
        audioHighPassFilter.enabled = false;
    }

}
