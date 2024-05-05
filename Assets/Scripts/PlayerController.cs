using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 200f; // 플레이어의 체력
    public GameObject rocket;
    public GameObject effect;

    public void TakeDamage(float damage)
    {
        health -= damage; // 플레이어의 체력에서 데미지만큼 감소

        // 체력이 0 이하로 떨어졌을 때 처리
        if (health <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        Time.timeScale = 1;
        effect.SetActive(true);
        rocket.SetActive(false); // 플레이어 비활성화
        yield return new WaitForSeconds(2f);
        GameManager.Instance.uiManager.FailedPanel.SetActive(true);
    }


    private void OnDisable()
    {
        
    }
}
