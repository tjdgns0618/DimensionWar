using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float health = 20f; // �÷��̾��� ü��
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
        health -= damage; // �÷��̾��� ü�¿��� ��������ŭ ����
        hpSlider.value -= damage;
        // ü���� 0 ���Ϸ� �������� �� ó��
        if (health <= 0)
        {
            StartCoroutine(GameOver());
            hpSlider.value = 0;
        }
    }

    IEnumerator GameOver()
    {
        Time.timeScale = 0.5f;
        hpSlider.gameObject.SetActive(false);
        effect.SetActive(true);
        rocket.SetActive(false); // �÷��̾� ��Ȱ��ȭ
        yield return new WaitForSeconds(0.4f);
        GameManager.Instance.uiManager.FailedPanel.SetActive(true);
    }



}
