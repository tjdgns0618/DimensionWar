using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Text text_Loading;
    public Slider slider;
    private float time_loading = 1;
    private float time_current;
    private float time_start;
    private bool isEnded = true;
    void Start()
    {
        Reset_Loading();
    }

    void Update()
    {
        if (isEnded)
            return;
        Check_Loading();
    }

    private void Check_Loading()
    {
        time_current = Time.time - time_start;
        if (time_current < time_loading)
        {
            Set_FillAmount(time_current / time_loading);
        }
        else if (!isEnded)
        {
            End_Loading();
        }
    }

    private void End_Loading()
    {
        Set_FillAmount(1);
        isEnded = true;
        SceneManager.LoadScene(PlayerPrefs.GetString("StageName"));
    }

    private void Reset_Loading()
    {
        time_current = time_loading;
        time_start = Time.time;
        Set_FillAmount(0);
        isEnded = false;
    }

    private void Set_FillAmount(float _value)
    {
        slider.value = _value;
        string txt = (_value.Equals(1) ? " 불러오기 완료!   " : "불러오는 중..  ") + (Mathf.Floor(_value * 10f) / 10).ToString("P");
        text_Loading.text = txt;
    }
}
