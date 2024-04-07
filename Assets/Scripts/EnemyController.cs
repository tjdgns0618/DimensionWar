using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float attackDamage = 10f; // 적의 공격력
    public float attackCooldown = 1f; // 적의 공격 속도
    public float movementSpeed = 3f; // 적의 이동 속도
    public float health = 100f; // 적의 체력

    private Tower currentTower; // 현재 충돌한 타워
    private GameObject player; // 플레이어 오브젝트
    private bool isAttackingTower = false; // 타워를 공격 중인지 여부
    private float attackTimer = 0f; // 적의 공격 쿨다운 타이머

    void Start()
    {
        // 플레이어 게임 오브젝트를 태그로 찾음
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
          //  Debug.LogError("Player not found!"); // 플레이어가 없을 경우 에러 메시지 출력 후 함수 종료
            return;
        }

        // 플레이어 쪽을 향하도록 적의 방향 설정 (Y 값을 무시하여 수직으로 움직이지 않도록 함)
        Vector3 playerPos = player.transform.position;
        playerPos.y = transform.position.y;
        transform.LookAt(playerPos);
    }

    void Update()
    {
        if(!player)
        {
            return;
        }
        // 타워를 공격 중이지 않을 경우 플레이어 쪽으로 이동
        if (!isAttackingTower)
        {
            Vector3 targetPos = player.transform.position;
            targetPos.y = transform.position.y;
            transform.Translate((targetPos - transform.position).normalized * movementSpeed * Time.deltaTime, Space.World);
        }
        else // 타워를 공격 중인 경우
        {
            // 타워가 존재하고 타워 게임 오브젝트도 존재하는 경우에만 공격 쿨다운 적용
            if (currentTower != null && currentTower.gameObject != null)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackCooldown) // 공격 쿨다운이 지나면 타워를 공격
                {
                    AttackTower(currentTower);
                    attackTimer = 0f; // 공격 쿨다운 타이머 초기화
                }
            }
            else // 타워가 파괴되었거나 없는 경우 공격 상태 해제
            {
                isAttackingTower = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        // 충돌한 오브젝트가 타워이고 현재 타워를 공격 중이지 않은 경우
        if (other.GetComponent<Tower>().isWall && currentTower == null)
        {
            currentTower = other.GetComponent<Tower>(); // 충돌한 타워 설정
            // 현재 타워의 적 수가 최대 적 수보다 적을 때
            if (currentTower.currentEnemyCount < currentTower.maxEnemiesPerTower)
            {
                currentTower.AddEnemy(); // 타워의 적 수 증가
                isAttackingTower = true; // 타워를 공격 중인 상태로 설정
            }
        }
        // 충돌한 오브젝트가 플레이어인 경우
        else if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>(); // 충돌한 플레이어 컨트롤러
            if (player != null)
            {
                player.TakeDamage(attackDamage); // 플레이어에게 데미지 주기
            }
            Destroy(gameObject); // 적 오브젝트 파괴
        }
    }

    void AttackTower(Tower tower)
    {
        tower.TakeDamage(attackDamage); // 타워에 데미지 주기
    }
}