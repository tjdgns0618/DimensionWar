using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class Blocks : MonoBehaviour, IPointerClickHandler
{
    float speed = 2f;               // �̵� �ӵ�

    public AudioSource[] audiosource;
    public bool isBuild = false;    // Ÿ���� ��ġ�� ���� ������
    public GameObject BuyEffect;    // Ŭ���� ���� ������ ����Ʈ
    public int dimension;           // ���� ���������� ���� ������������ ���� ���ϴ� ����
    [HideInInspector]
    public GameObject tempBuyEffect;     // BuyEffect�� �������� GameObject ����

    private void Awake()
    {
        dimension = PlayerPrefs.GetInt("dimension");    // ���� ���������� Dimension�� �޾ƿ´�.
    }

    void FixedUpdate()
    {
        if (gameObject.CompareTag("MeleeBuildable") || dimension == 2)    // ����Ÿ���̰ų� ���������� 2D����������� ������ �������� ����
            return;

        if (isBuild)
            BlockUp();      // ������ ���õǾ����� ������ ���� �ö���� �Լ�
        else
            BlockDown();    // ���� ������ �����Ǿ����� ������ �������� �Լ�
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // �̹� ���õ� ������ �����ϴ� ��� ��ȯ, Ÿ���� �������ϰ��
        if (GameManager.Instance.tower)
        {
            GameManager.Instance.uiManager.UpgradePanel.GetComponent<DOTweenAnimation>().DORewind();
            GameManager.Instance.tower.GetComponent<TestScript>().ClickEffect.SetActive(false);
            GameManager.Instance.tower = null;
            BlockClick();
        }
        else if (GameManager.Instance.SelectBlock)
        {
            Destroy(GameManager.Instance.SelectBlock.GetComponent<Blocks>().tempBuyEffect.gameObject);
            GameManager.Instance.SelectBlock.GetComponent<Blocks>().isBuild = false;
            BlockClick();
        }
        else
        {
            BlockClick();
        }
    }

    void BlockClick()
    {
        GameManager.Instance.uiManager.BuyButton.image.color = Color.white;
        GameManager.Instance.uiManager.SellButton.image.color = Color.gray;
        GameManager.Instance.uiManager.towerUpgradeButton.image.color = Color.gray;
        GameManager.Instance.uiManager.argumentButton.image.color = Color.gray;
        GameManager.Instance.uiManager.BuyButton.GetComponent<Button>().enabled = true;
        GameManager.Instance.uiManager.SellButton.GetComponent<Button>().enabled = false;
        GameManager.Instance.uiManager.towerUpgradeButton.GetComponent<Button>().enabled = false;
        GameManager.Instance.uiManager.argumentButton.GetComponent<Button>().enabled = false;
        audiosource[0].Play();

        GameManager.Instance.blockClicked = true;               // ������ ���õ�
        GameManager.Instance.SelectBlock = this.gameObject;     // ���ӸŴ����� ���� ���õ� ������ �� ������ ����
        isBuild = true;
        GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORestart();   // ���� �г��� �ö�����ϴ� �Լ�
        tempBuyEffect = Instantiate(BuyEffect);      // ������ ���õǾ����� �˷��ִ� ����Ʈ ����
        tempBuyEffect.transform.parent = transform;  // ����Ʈ�� �� ������ �ڽ����� ����
        tempBuyEffect.transform.localPosition = new Vector3(0, 0.8f, 0); // ����Ʈ�� ��ġ ����
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
