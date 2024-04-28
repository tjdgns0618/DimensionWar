using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCutScene : MonoBehaviour
{
    public Button button;

    public float fadeInDuration = 1.0f; // 페이드 인 시간
    public float scrollDuration = 2.0f; // 스크롤 총 시간
    public float initialYScale = 0.2f; // 초기 Y 스케일
    public float finalYScale = 0.5f; // 최종 Y 스케일
    public float displayDuration = 2.0f; // 유지시간

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        button.onClick.AddListener(OnClickButton);
    }

    void OnClickButton()
    {
       
        StartCoroutine(BiggerAndFadeInOut()); // 스케일링 실행
    }

    IEnumerator BiggerAndFadeInOut()
    {
        //// 이미지 활성화
        spriteRenderer.enabled = true;

        float startTime = Time.time;

        // 페이드 인과 확장 동시에 진행
        while (Time.time - startTime < Mathf.Max(fadeInDuration, scrollDuration))
        {
            // 페이드 인
            if (Time.time - startTime < fadeInDuration)
            {
                float alpha = Mathf.Lerp(0f, 1f, (Time.time - startTime) / fadeInDuration);
                SetSpriteAlpha(alpha);
            }

            // 확장
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
