using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Blocks : MonoBehaviour, IPointerClickHandler
{
    float speed = 2.0f; // �̵� �ӵ�
    public bool isBuild = false;
    public GameObject BuyEffect;
    [HideInInspector]
    public GameObject instance;

    void Update()
    {
        if (gameObject.CompareTag("MeleeBuildable"))
            return;

        if (isBuild)
            BlockUp();
        else
            BlockDown();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.blockClicked)
            return;

        GameManager.Instance.blockClicked = true;
        GameManager.Instance.SelectBlock = this.gameObject;
        isBuild = true;
        GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORestart();
        instance = Instantiate(BuyEffect);
        instance.transform.parent = transform;
        instance.transform.localPosition = new Vector3(0, 0.8f, 0);
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
