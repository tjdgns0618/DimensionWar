using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Blocks : MonoBehaviour, IPointerClickHandler
{
    public float speed = 2.0f; // 이동 속도
    public bool isBuild = false;

    void Update()
    {
        if (isBuild)
            BlockUp();
        else
        {
            BlockDown();
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        
        isBuild = true;
    }

    void BlockUp()
    {
        Vector3 target = new Vector3(transform.position.x, 0.2f, transform.position.z);

        // 목표 위치로의 방향 벡터
        Vector3 direction = (target - transform.position).normalized;

        // 목표 위치로의 거리
        float distance = Vector3.Distance(transform.position, target);

        // 거리가 일정 이상 멀 때만 이동
        if (distance > 0.1f)
        {
            // 부드러운 이동을 위해 Lerp 사용
            transform.localPosition += direction * speed * Time.deltaTime;
        }
    }

    void BlockDown()
    {
        Vector3 target = new Vector3(transform.position.x, 0.4f - 1.25f, transform.position.z);

        // 목표 위치로의 방향 벡터
        Vector3 direction = (target - transform.position).normalized;

        // 목표 위치로의 거리
        float distance = Vector3.Distance(transform.position, target);

        // 거리가 일정 이상 멀 때만 이동
        if (distance > 0.1f)
        {
            // 부드러운 이동을 위해 Lerp 사용
            transform.localPosition += direction * speed * Time.deltaTime;
        }
    }
}
