using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCutScene : MonoBehaviour
{
    public Button button;

    public float fadeInDuration = 1.0f; // ���̵� �� �ð�
    public float scrollDuration = 2.0f; // ��ũ�� �� �ð�
    public float initialYScale = 0.2f; // �ʱ� Y ������
    public float finalYScale = 0.5f; // ���� Y ������
    public float displayDuration = 2.0f; // �����ð�

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        button.onClick.AddListener(OnClickButton);
    }

    void OnClickButton()
    {
       
        StartCoroutine(BiggerAndFadeInOut()); // �����ϸ� ����
    }

    IEnumerator BiggerAndFadeInOut()
    {
        //// �̹��� Ȱ��ȭ
        spriteRenderer.enabled = true;

        float startTime = Time.time;

        // ���̵� �ΰ� Ȯ�� ���ÿ� ����
        while (Time.time - startTime < Mathf.Max(fadeInDuration, scrollDuration))
        {
            // ���̵� ��
            if (Time.time - startTime < fadeInDuration)
            {
                float alpha = Mathf.Lerp(0f, 1f, (Time.time - startTime) / fadeInDuration);
                SetSpriteAlpha(alpha);
            }

            // Ȯ��
            if (Time.time - startTime < scrollDuration)
            {
                float progress = (Time.time - startTime) / scrollDuration;
                float scaleFactor = Mathf.Lerp(initialYScale, finalYScale, progress);
                transform.localScale = new Vector3(0.5f, scaleFactor, 1f);
            }

            yield return null;
        }

        yield return new WaitForSeconds(displayDuration);
        spriteRenderer.enabled = false;
    }

     private void SetSpriteAlpha(float alpha)
     {
         Color color = spriteRenderer.color;
         color.a = alpha;
         spriteRenderer.color = color;
     }
}
