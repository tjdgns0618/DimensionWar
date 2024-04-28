using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Text text_Loading;       // �ε� �ۼ�Ʈ�� ����ϴ� �ؽ�Ʈ
    public Slider slider;           // �ε��� �����ִ� �����̴�
    private float time_loading = 1; // �ε� �ִ�ġ
    private float time_current;     // ���� �ε� ����
    private float time_start;       // �ε����� �ʱ�ȭ��
    private bool isEnded = true;    // �ε��� �������� Ȯ�ο�
    void Start()
    {
        Reset_Loading();    // �ε� ���¸� �ʱ�ȭ���ش�.
    }

    void Update()
    {
        if (isEnded)        // �ε��� ������ ����ó��
            return;
        Check_Loading();    // �ε����ִ� �Լ�
    }

    // �ε��ϴ� �Լ�
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

    // �ε��� �������� ����Ǵ� �Լ�
    private void End_Loading()
    {
        Set_FillAmount(1);
        isEnded = true;
        SceneManager.LoadScene(PlayerPrefs.GetString("StageName"));
    }

    // �ε����¸� �ʱ�ȭ���ִ� �Լ�
    private void Reset_Loading()
    {
        time_current = time_loading;
        time_start = Time.time;
        Set_FillAmount(0);
        isEnded = false;
    }

    // ���� �ε� ���¸� �ؽ�Ʈ�� ������ִ� �Լ�
    private void Set_FillAmount(float _value)
    {
        slider.value = _value;
        string txt = (_value.Equals(1) ? " �ҷ����� �Ϸ�!   " : "�ҷ����� ��..  ") + (Mathf.Floor(_value * 10f) / 10).ToString("P");
        text_Loading.text = txt;
    }
}
