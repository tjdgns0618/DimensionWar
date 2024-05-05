using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 200f; // �÷��̾��� ü��
    public GameObject rocket;
    public GameObject effect;

    public void TakeDamage(float damage)
    {
        health -= damage; // �÷��̾��� ü�¿��� ��������ŭ ����

        // ü���� 0 ���Ϸ� �������� �� ó��
        if (health <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        Time.timeScale = 1;
        effect.SetActive(true);
        rocket.SetActive(false); // �÷��̾� ��Ȱ��ȭ
        yield return new WaitForSeconds(2f);
        GameManager.Instance.uiManager.FailedPanel.SetActive(true);
    }


    private void OnDisable()
    {
        
    }
}
