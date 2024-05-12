using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;

public class BTManager : MonoBehaviour
{
    [Header("Ÿ��")]
    public GameObject[] Tower;      // Ÿ����
    [Header("���Ӽӵ�ǥ��UI")]
    public GameObject[] SpeedUI;    // ���Ӽӵ� ǥ�� UI �̹���

    int gameSpeed = 1;              // ���� ���� �ӵ�

    public void BuyTower()      // Ÿ�� ���� �Լ�
    {
        if(GameManager.Instance.diamond >= 1) // 20��尡 �־�߸� ���� ���� ����
            StartCoroutine(_BuyTower());
    }

    public void SellTower()
    {
        StartCoroutine(_SellTower());
    }

    public void UpgradeTower()
    {
        if (GameManager.Instance.tower.CompareTag("Level1") && GameManager.Instance.gold >= 50)
            StartCoroutine(_UpgradeTower());
        else if (GameManager.Instance.tower.CompareTag("Level2") && GameManager.Instance.gold >= 100)
            StartCoroutine(_UpgradeTower());
        else if (GameManager.Instance.tower.CompareTag("Tower") && GameManager.Instance.gold >= 200)
            StartCoroutine(_UpgradeTower());

        else
        {
            StartTime();                        // timescale�� ������� �����ִ� �Լ�
        }
    }

    public void MeleeTowerSpawn()               // ���� Ÿ���� �������ִ� �Լ�
    {
        if (GameManager.Instance.SelectBlock.transform.childCount >= 2) // Ÿ���� �������� �ʴ� �����̶��
            return;

        int i = Random.Range(0, 3);
        GameObject instance = Instantiate(Tower[i]);    // ����Ÿ���߿��� �������� ����
        GameManager.Instance.towers.Add(instance);      // ������ Ÿ���� ���ӸŴ����� �������ش�.
        instance.transform.SetParent(GameManager.Instance.SelectBlock.transform);   // Ŭ���� ���� �ڽ����� Ÿ���� �����Ѵ�.
        if (i == 0) {
            instance.transform.localPosition = new Vector3(0, 1.519f, 0);   // �ȼ� ���� Ÿ�� ��ġ �ʱ�ȭ
            instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (i == 1)
        {
            instance.transform.localPosition = new Vector3(0, 0.5f, 0);     // �ο����� ���� Ÿ�� ��ġ �ʱ�ȭ
            instance.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (i == 2)
        {
            instance.transform.localPosition = new Vector3(0, 0.5f, 0);     // 3D ���� Ÿ�� ��ġ �ʱ�ȭ
            instance.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void RangerTowerSpawn()
    {
        if (GameManager.Instance.SelectBlock.transform.childCount >= 2) // Ÿ���� �������� �ʴ� �����̶��
            return;

        int i = Random.Range(3, 9);
        GameObject instance = Instantiate(Tower[i]);    // ���Ÿ�Ÿ���߿��� �������� ����
        GameManager.Instance.towers.Add(instance);      // ������ Ÿ���� ���ӸŴ����� �������ش�.
        instance.transform.SetParent(GameManager.Instance.SelectBlock.transform);   // Ŭ���� ���� �ڽ����� Ÿ���� �����Ѵ�.
        if (i == 3 || i == 4)
        {
            instance.transform.localPosition = new Vector3(0, 1.418f, 0);   // �ȼ� ���Ÿ� Ÿ�� ��ġ �ʱ�ȭ
            instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (i == 5 || i == 6)
        {
            instance.transform.localPosition = new Vector3(0, 0.5f, 0);     // �ο����� ���Ÿ� Ÿ�� ��ġ �ʱ�ȭ
            instance.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (i == 7 || i == 8)
        {
            instance.transform.localPosition = new Vector3(0, 0.5f, 0);     // 3D ���Ÿ� Ÿ�� ��ġ �ʱ�ȭ
            instance.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void UpgradeBtnClick()
    {
        GameManager.Instance.uiManager.UpgradePanel.GetComponent<DOTweenAnimation>().DOPlay();

        if(GameManager.Instance.tower.GetComponent<Tower>().tower_type == 
            global::Tower.Tower_Type.Meele)
        {
            GameManager.Instance.uiManager.rangeStat1Button.gameObject.SetActive(false);
            GameManager.Instance.uiManager.rangeStat2Button.gameObject.SetActive(false);
            GameManager.Instance.uiManager.meleeStat1Button.gameObject.SetActive(true);
            GameManager.Instance.uiManager.meleeStat2Button.gameObject.SetActive(true);

            if (!GameManager.Instance.tower.GetComponent<Tower>().upgrade[0])
                GameManager.Instance.uiManager.meleeStat1Button.image.color = Color.white;
            else
                GameManager.Instance.uiManager.meleeStat1Button.image.color = Color.green;

            if (!GameManager.Instance.tower.GetComponent<Tower>().upgrade[1])
                GameManager.Instance.uiManager.meleeStat2Button.image.color = Color.white;
            else
                GameManager.Instance.uiManager.meleeStat2Button.image.color = Color.green;

            GameManager.Instance.uiManager.meleeStat1Button.enabled =
                !GameManager.Instance.tower.GetComponent<Tower>().upgrade[0];
            GameManager.Instance.uiManager.meleeStat2Button.enabled =
                !GameManager.Instance.tower.GetComponent<Tower>().upgrade[1];
        }
        else
        {
            GameManager.Instance.uiManager.meleeStat1Button.gameObject.SetActive(false);
            GameManager.Instance.uiManager.meleeStat2Button.gameObject.SetActive(false);
            GameManager.Instance.uiManager.rangeStat1Button.gameObject.SetActive(true);
            GameManager.Instance.uiManager.rangeStat2Button.gameObject.SetActive(true);

            if (!GameManager.Instance.tower.GetComponent<Tower>().upgrade[0])
                GameManager.Instance.uiManager.rangeStat1Button.image.color = Color.white;
            else
                GameManager.Instance.uiManager.rangeStat1Button.image.color = Color.green;

            if (!GameManager.Instance.tower.GetComponent<Tower>().upgrade[1])
                GameManager.Instance.uiManager.rangeStat2Button.image.color = Color.white;
            else
                GameManager.Instance.uiManager.rangeStat2Button.image.color = Color.green;

            GameManager.Instance.uiManager.rangeStat1Button.enabled =
                !GameManager.Instance.tower.GetComponent<Tower>().upgrade[0];
            GameManager.Instance.uiManager.rangeStat2Button.enabled =
                !GameManager.Instance.tower.GetComponent<Tower>().upgrade[1];
        }
        GameManager.Instance.uiManager.UpgradeButton.enabled = 
            (GameManager.Instance.tower.GetComponent<Tower>().upgrade[0] && 
            GameManager.Instance.tower.GetComponent<Tower>().upgrade[1]);
    }

    public void Stat1Upgrade()
    {
        if (GameManager.Instance.tower.GetComponent<Tower>().tower_type ==
            global::Tower.Tower_Type.Meele)
        {
            GameManager.Instance.uiManager.meleeStat1Button.enabled = false;
            GameManager.Instance.uiManager.meleeStat1Button.image.color = Color.green;
        }
        else
        {
            GameManager.Instance.uiManager.rangeStat1Button.enabled = false;
            GameManager.Instance.uiManager.rangeStat1Button.image.color = Color.green;
        }

            
        GameManager.Instance.tower.GetComponent<Tower>().upgrade[0] = true;
        if(GameManager.Instance.tower.GetComponent<Tower>().upgrade[0] && 
            GameManager.Instance.tower.GetComponent<Tower>().upgrade[1])
        {
            GameManager.Instance.uiManager.UpgradeButton.enabled = true;
        }
    }

    public void Stat2Upgrade()
    {
        if (GameManager.Instance.tower.GetComponent<Tower>().tower_type ==
            global::Tower.Tower_Type.Meele)
        {
            GameManager.Instance.uiManager.meleeStat2Button.enabled = false;
            GameManager.Instance.uiManager.meleeStat2Button.image.color = Color.green;
        }
        else
        {
            GameManager.Instance.uiManager.rangeStat2Button.enabled = false;
            GameManager.Instance.uiManager.rangeStat2Button.image.color = Color.green;
        }

        GameManager.Instance.tower.GetComponent<Tower>().upgrade[1] = true;
        if (GameManager.Instance.tower.GetComponent<Tower>().upgrade[0] &&
            GameManager.Instance.tower.GetComponent<Tower>().upgrade[1])
        {
            GameManager.Instance.uiManager.UpgradeButton.enabled = true;
        }
    }

    public void DamageUpgrade()
    {
        GameManager.Instance.gold -= 50;
        GameManager.Instance.tower.GetComponent<Tower>().Damage *= 1.2f;
    }

    public void SpeedUpgrade()
    {
        GameManager.Instance.gold -= 50;
        GameManager.Instance.tower.GetComponent<Tower>().AttackDel *= 0.9f;
    }

    public void SkillCoolUpgrade()
    {
        GameManager.Instance.gold -= 50;
        GameManager.Instance.tower.GetComponent<Tower>().SkillCost--;
    }

    public void HealthUpgrade()
    {
        GameManager.Instance.gold -= 50;
        GameManager.Instance.tower.GetComponent<Tower>().health *= 2f;
        GameManager.Instance.tower.GetComponent<Tower>().tempHealth *= 2f;
    }

    IEnumerator _BuyTower()
    {
        if (GameManager.Instance.SelectBlock.CompareTag("MeleeBuildable"))  // ���� ��ġ ���� �����̶��
            MeleeTowerSpawn();
        else
            RangerTowerSpawn();
                
        GameManager.Instance.diamond--;            // 20��尡 �Ҹ�ȴ�.
        GameManager.Instance.blockClicked = false;  // ���� Ŭ�����¸� �ʱ�ȭ���ش�.
        GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORewind();    // ���� �г��� ����������.
        GameManager.Instance.SelectBlock.layer = 2; // ������ Ŭ�� �Ұ����ϰ� �����.
        Destroy(GameManager.Instance.SelectBlock.GetComponent<Blocks>().tempBuyEffect.gameObject);   // ���� ���� ����Ʈ�� �����ش�.
        GameManager.Instance.SelectBlock = null;

        yield return null;
    }

    IEnumerator _SellTower()
    {
        if(GameManager.Instance.tower.CompareTag("Level1"))
            GameManager.Instance.gold += 20;
        else if(GameManager.Instance.tower.CompareTag("Level2"))
            GameManager.Instance.gold += 50;
        else if(GameManager.Instance.tower.CompareTag("Tower"))
            GameManager.Instance.gold += 100;

        GameManager.Instance.gold += 20;    // 20��带 �����޴´�.
        Destroy(GameManager.Instance.tower.gameObject); // Ÿ���� �����Ѵ�.
        GameManager.Instance.tower.transform.parent.GetComponent<Blocks>().isBuild = false;
        GameManager.Instance.blockClicked = false;  // ���� Ŭ�����¸� �ʱ�ȭ���ش�.
        GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORewind();    // ���� �г��� ����������.
        GameManager.Instance.tower.transform.parent.gameObject.layer = 0;   // ������ Ŭ�� �����ϰ� �����.

        yield return null;
    }

    IEnumerator _UpgradeTower()
    {
        // ���� Ÿ���� �������� �ʴٸ� ����ó��
        if (GameManager.Instance.tower.GetComponent<TestScript>().nextTower == null)
        {
            Time.timeScale = gameSpeed;
            //�ӽ÷� 3�� Ÿ�� ��ų���� �� �߰�
            if (GameManager.Instance.tower.GetComponent<Tower_Skill>().tower_Level + 1 <= GameManager.Instance.tower.GetComponent<Tower_Skill>().SkillPrefabs.Length)
                GameManager.Instance.tower.GetComponent<Tower_Skill>().tower_Level++;
            yield return null;
        }

        if(GameManager.Instance.tower.CompareTag("Level1"))
            GameManager.Instance.gold -= 50;
        else if(GameManager.Instance.tower.CompareTag("Level2"))
            GameManager.Instance.gold -= 100;
        else if(GameManager.Instance.tower.CompareTag("Tower"))
            GameManager.Instance.gold -= 200;
            
        // ���� ���õ� Ÿ���� ���� Ÿ���� �����Ѵ�.
        GameObject instance = Instantiate(GameManager.Instance.tower.GetComponent<TestScript>().nextTower);
        instance.transform.SetParent(GameManager.Instance.tower.transform.parent.transform);
        instance.transform.position = GameManager.Instance.tower.transform.position;
        if(instance.GetComponent<Tower>().tower_class != global::Tower.Tower_Class.Pixel)
            instance.transform.rotation = Quaternion.Euler(0, 180, 0);
        if (GameManager.Instance.tower.tower_class == global::Tower.Tower_Class.Pixel && GameManager.Instance.tower.tag == "Level2")
            instance.transform.position += new Vector3(0, 0.4f, 0); // �ȼ� 3�� ��ġ�� ����ó��
        Destroy(GameManager.Instance.tower.gameObject); // ���� Ÿ���� �����Ѵ�.
        Time.timeScale = gameSpeed;

        yield return null;
    }

    // ���� ������ ����ϰ� ���� �г��� ���������� �Լ�
    public void EndBuyTower()
    {
        if (GameManager.Instance.tower != null)
        {
            GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORewind();
            GameManager.Instance.tower.GetComponent<TestScript>().ClickEffect.SetActive(false);
            return;
        }
        GameManager.Instance.blockClicked = false;
        if(GameManager.Instance.SelectBlock)
            GameManager.Instance.SelectBlock.GetComponent<Blocks>().isBuild = false;
        //GameManager.Instance.SelectBlock.GetComponent<Blocks>().audiosource.clip =
        //    GameManager.Instance.SelectBlock.GetComponent<Blocks>().audio[1];
        //GameManager.Instance.SelectBlock.GetComponent<Blocks>().audiosource.Play();
        GameManager.Instance.SelectBlock.GetComponent<Blocks>().audiosource[1].Play();
        Destroy(GameManager.Instance.SelectBlock.GetComponent<Blocks>().tempBuyEffect.gameObject);

        GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORewind();
        GameManager.Instance.SelectBlock = null;
        GameManager.Instance.tower = null;
    }

    // ��ų ĵ������ �����ϰ� ���Ӽӵ��� ������� �ϴ� �Լ�
    public void EndSkillUp()
    {
        Time.timeScale = gameSpeed;
        GameManager.Instance.towerClicked = false;
    }

    // timescale�� ���� �ӵ��� ����� �Լ�
    public void StartTime()
    {
        Time.timeScale = gameSpeed;
    }

    public void MakeTime1()
    {
        Time.timeScale = 1;
    }

    // timescale�� 0���� ����� �Լ�
    public void StopTime()
    {
        Time.timeScale = 0;
    }

    // timescale�� 1,2,3���� ������ְ� gameSpeed �ʱ�ȭ���ִ� �Լ�
    public void SpeedUp()
    {
        if (Time.timeScale == 1)
        {
            gameSpeed = 2;
            PlayerPrefs.SetInt("Timescale", gameSpeed);
            SpeedUI[0].SetActive(false);
            SpeedUI[1].SetActive(true);
            Time.timeScale = gameSpeed;
        }
        else if (Time.timeScale == 2)
        {
            gameSpeed = 3;
            PlayerPrefs.SetInt("Timescale", gameSpeed);
            SpeedUI[1].SetActive(false);
            SpeedUI[2].SetActive(true);
            Time.timeScale = gameSpeed;
        }
        else if (Time.timeScale == 3)
        {
            gameSpeed = 1;
            PlayerPrefs.SetInt("Timescale", gameSpeed);
            SpeedUI[2].SetActive(false);
            SpeedUI[0].SetActive(true);
            Time.timeScale = gameSpeed;
        }
    }
}
