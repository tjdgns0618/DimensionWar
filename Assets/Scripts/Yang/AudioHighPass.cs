using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHighPass : MonoBehaviour
{
    public AudioHighPassFilter audioHighPassFilter;

    private void Start()
    {

    }

    private void OnEnable()
    {
        audioHighPassFilter.enabled = true;
        GameManager.Instance.soundManager.m_AudioMixer.SetFloat("SFX", -80f);
    }

    private void OnDisable()
    {
        audioHighPassFilter.enabled = false;
        GameManager.Instance.soundManager.m_AudioMixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("SFXValue")) * 20); ;
    }

}
