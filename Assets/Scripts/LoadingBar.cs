using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Text text_Loading;       // 로딩 퍼센트를 출력하는 텍스트
    public Slider slider;           // 로딩을 보여주는 슬라이더
    private float time_loading = 1; // 로딩 최대치
    private float time_current;     // 현재 로딩 상태
    private float time_start;       // 로딩상태 초기화용
    private bool isEnded = true;    // 로딩이 끝났는지 확인용
    void Start()
    {
        Reset_Loading();    // 로딩 상태를 초기화해준다.
    }

    void Update()
    {
        if (isEnded)        // 로딩이 끝나면 예외처리
            return;
        Check_Loading();    // 로딩해주는 함수
    }

    // 로딩하는 함수
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

    // 로딩이 끝났을때 실행되는 함수
    private void End_Loading()
    {
        Set_FillAmount(1);
        isEnded = true;
        SceneManager.LoadScene(PlayerPrefs.GetString("StageName"));
    }

    // 로딩상태를 초기화해주는 함수
    private void Reset_Loading()
    {
        time_current = time_loading;
        time_start = Time.time;
        Set_FillAmount(0);
        isEnded = false;
    }

    // 현재 로딩 상태를 텍스트로 출력해주는 함수
    private void Set_FillAmount(float _value)
    {
        slider.value = _value;
        string txt = (_value.Equals(1) ? " 불러오기 완료!   " : "불러오는 중..  ") + (Mathf.Floor(_value * 10f) / 10).ToString("P");
        text_Loading.text = txt;
    }
}
