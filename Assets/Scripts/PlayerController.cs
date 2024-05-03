using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 200f; // 플레이어의 체력

    public void TakeDamage(float damage)
    {
        health -= damage; // 플레이어의 체력에서 데미지만큼 감소

        // 체력이 0 이하로 떨어졌을 때 처리
        if (health <= 0)
        {
            gameObject.SetActive(false); // 플레이어 비활성화
            Debug.Log("Player died");
            // 게임 오버 또는 다른 처리를 수행할 수 있음
        }
    }

    private void OnDisable()
    {
        
    }
}
