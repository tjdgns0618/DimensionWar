using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float health = 20f; // 플레이어의 체력
    public GameObject rocket;
    public GameObject effect;
    public Slider hpSlider;

    private void Awake()
    {
        hpSlider.maxValue = health;
        hpSlider.value = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage; // 플레이어의 체력에서 데미지만큼 감소
        hpSlider.value -= damage;
        // 체력이 0 이하로 떨어졌을 때 처리
        if (health <= 0)
        {
            StartCoroutine(GameOver());
            hpSlider.value = 0;
        }
    }

    IEnumerator GameOver()
    {
        Time.timeScale = 1;
        hpSlider.gameObject.SetActive(false);
        effect.SetActive(true);
        rocket.SetActive(false); // 플레이어 비활성화
        yield return new WaitForSeconds(2f);
        GameManager.Instance.uiManager.FailedPanel.SetActive(true);
    }



}
