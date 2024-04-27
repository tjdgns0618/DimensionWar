using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BtnSkillCoolTime : MonoBehaviour
{
    public Button btn;
    public float coolTime = 10f;
    public TMP_Text textCoolTime;

    private Coroutine coolTimeRountine;

    public Image imgFill;

    public void Init()
    {
        this.textCoolTime.gameObject.SetActive(false);
        this.imgFill.fillAmount = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.btn = this.GetComponent<Button>();
        this.btn.onClick.AddListener(() =>
        {
            if(this.coolTimeRountine != null)
            {
                Debug.Log("쿨타임 중입니다...");
            }
            else
            {
                this.coolTimeRountine = this.StartCoroutine(this.CoolTimeRoutine());
            }
        });

        Init();
    }

    private IEnumerator CoolTimeRoutine()
    {
        this.textCoolTime.gameObject.SetActive(true);
        var time = this.coolTime;

        while(true)
        {
            time -= Time.deltaTime;
            this.textCoolTime.text = time.ToString("F1");   // 소수점 자리 첫번째까지 출력

            var per = time / this.coolTime;
            this.imgFill.fillAmount = per;

            if(time <= 0)
            {
                this.textCoolTime.gameObject.SetActive(false);
            }
            yield return null;
        }
        this.coolTimeRountine = null;
    }
}

