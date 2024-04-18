using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Blocks : MonoBehaviour, IPointerClickHandler
{
    public float speed = 2.0f; // �̵� �ӵ�
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

        // ��ǥ ��ġ���� ���� ����
        Vector3 direction = (target - transform.position).normalized;

        // ��ǥ ��ġ���� �Ÿ�
        float distance = Vector3.Distance(transform.position, target);

        // �Ÿ��� ���� �̻� �� ���� �̵�
        if (distance > 0.1f)
        {
            // �ε巯�� �̵��� ���� Lerp ���
            transform.localPosition += direction * speed * Time.deltaTime;
        }
    }

    void BlockDown()
    {
        Vector3 target = new Vector3(transform.position.x, 0.4f - 1.25f, transform.position.z);

        // ��ǥ ��ġ���� ���� ����
        Vector3 direction = (target - transform.position).normalized;

        // ��ǥ ��ġ���� �Ÿ�
        float distance = Vector3.Distance(transform.position, target);

        // �Ÿ��� ���� �̻� �� ���� �̵�
        if (distance > 0.1f)
        {
            // �ε巯�� �̵��� ���� Lerp ���
            transform.localPosition += direction * speed * Time.deltaTime;
        }
    }
}
