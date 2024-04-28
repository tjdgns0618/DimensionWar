using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Blocks : MonoBehaviour, IPointerClickHandler
{
    float speed = 2f;               // �̵� �ӵ�
    public bool isBuild = false;    // Ÿ���� ��ġ�� �� ������
    public GameObject BuyEffect;    // Ŭ���� �� ������ ����Ʈ
    public int dimension;           // ���� ���������� ���� ���������� ���� ���ϴ� ����
    [HideInInspector]
    public GameObject instance;     // BuyEffect�� �������� GameObject ����

    private void Awake()
    {
        dimension = PlayerPrefs.GetInt("dimension");    // ���� ���������� Dimension�� �޾ƿ´�.
    }

    void FixedUpdate()
    {
        if (gameObject.CompareTag("MeleeBuildable") || dimension == 2)    // ����Ÿ���̰ų� ���������� 2D����������� ���� �������� ����
            return;

        if (isBuild)
            BlockUp();      // ���� ���õǾ����� ���� ���� �ö���� �Լ�
        else
            BlockDown();    // �� ������ �����Ǿ����� ���� �������� �Լ�
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.blockClicked)  // �̹� ���õ� ���� �����ϴ� ��� ��ȯ
            return;

        GameManager.Instance.blockClicked = true;               // ���� ���õ�
        GameManager.Instance.SelectBlock = this.gameObject;     // ���ӸŴ����� ���� ���õ� ���� �� ���� ����
        isBuild = true;
        GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORestart();   // ���� �г��� �ö�����ϴ� �Լ�
        instance = Instantiate(BuyEffect);      // ���� ���õǾ����� �˷��ִ� ����Ʈ ����
        instance.transform.parent = transform;  // ����Ʈ�� �� ���� �ڽ����� ����
        instance.transform.localPosition = new Vector3(0, 0.8f, 0); // ����Ʈ�� ��ġ ����
    }

    void BlockUp()
    {
        Vector3 target = new Vector3(transform.position.x, 0.2f, transform.position.z); // ��ǥ ��ġ
        Vector3 direction = (target - transform.position).normalized;   // ��ǥ ��ġ���� ���� ����
        float distance = Vector3.Distance(transform.position, target);  // ��ǥ ��ġ���� �Ÿ�

        // �Ÿ��� ���� �̻� �� ���� �̵�
        if (distance > 0.1f)
        {
            // �ε巯�� �̵��� ���� Lerp ���
            transform.localPosition += direction * speed * Time.fixedDeltaTime;
        }
    }

    void BlockDown()
    {
        Vector3 target = new Vector3(transform.position.x, 0.4f - 1.25f, transform.position.z); // ��ǥ ��ġ
        Vector3 direction = (target - transform.position).normalized;   // ��ǥ ��ġ���� ���� ����
        float distance = Vector3.Distance(transform.position, target);  // ��ǥ ��ġ���� �Ÿ�

        // �Ÿ��� ���� �̻� �� ���� �̵�
        if (distance > 0.1f)
        {
            // �ε巯�� �̵��� ���� Lerp ���
            transform.localPosition += direction * speed * Time.fixedDeltaTime;
        }
    }
}
