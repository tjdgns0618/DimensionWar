using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera cam; 

    void Start()
    {
        // ���� ������ Ȱ��ȭ�� ī�޶� ã��
        cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("ī�޶� ����");
        }
    }

    void Update()
    {
        // ī�޶� ���� ��� ������Ʈ ����
        if (cam == null)
        {
            return;
        }

        // �׻� ī�޶� ���ϵ��� �����̼� ���� ����
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}
