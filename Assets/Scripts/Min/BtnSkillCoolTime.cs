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
                Debug.Log("��Ÿ�� ���Դϴ�...");
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
            this.textCoolTime.text = time.ToString("F1");   // �Ҽ��� �ڸ� ù��°���� ���

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

