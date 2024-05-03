using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 200f; // �÷��̾��� ü��

    public void TakeDamage(float damage)
    {
        health -= damage; // �÷��̾��� ü�¿��� ��������ŭ ����

        // ü���� 0 ���Ϸ� �������� �� ó��
        if (health <= 0)
        {
            gameObject.SetActive(false); // �÷��̾� ��Ȱ��ȭ
            Debug.Log("Player died");
            // ���� ���� �Ǵ� �ٸ� ó���� ������ �� ����
        }
    }

    private void OnDisable()
    {
        
    }
}
