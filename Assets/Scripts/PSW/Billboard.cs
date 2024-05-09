using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera cam; 

    void Start()
    {
        // 현재 씬에서 활성화된 카메라를 찾음
        cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("카메라 없음");
        }
    }

    void Update()
    {
        // 카메라가 없을 경우 업데이트 중지
        if (cam == null)
        {
            return;
        }

        // 항상 카메라를 향하도록 로테이션 값을 조정
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}
