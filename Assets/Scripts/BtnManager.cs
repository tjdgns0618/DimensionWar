using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;

public class BTManager : MonoBehaviour
{
    [Header("鸥况")]
    public GameObject[] Tower;
    [Header("霸烙加档钎矫UI")]
    public GameObject[] SpeedUI;

    int gameSpeed = 1;

    public void BuyTower()
    {
        if(GameManager.Instance.gold >= 20)
        StartCoroutine(_BuyTower());
    }

    public void SellTower()
    {
        StartCoroutine(_SellTower());
    }

    public void UpgradeTower()
    {
        if (GameManager.Instance.gold >= 50)
            StartCoroutine(_UpgradeTower());
        else
        {
            StartTime();
        }
    }

    public void MeleeTowerSpawn()
    {
        if (GameManager.Instance.SelectBlock.transform.childCount >= 2)
            return;

        int i = Random.Range(0, 3);
        GameObject instance = Instantiate(Tower[i]);
        instance.transform.SetParent(GameManager.Instance.SelectBlock.transform);
        if (i == 0) {            
            instance.transform.localPosition = new Vector3(0, 1.519f, 0);
            instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (i == 1)
        {
            instance.transform.localPosition = new Vector3(0, 0.5f, 0);
            instance.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (i == 2)
        {
            instance.transform.localPosition = new Vector3(0, 0.5f, 0);
            instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        Debug.Log(Tower[i].name + "辟立鸥况积己");
    }

    public void RangerTowerSpawn()
    {
        if (GameManager.Instance.SelectBlock.transform.childCount >= 2)
            return;

        int i = Random.Range(3, 9);
        GameObject instance = Instantiate(Tower[i]);
        instance.transform.SetParent(GameManager.Instance.SelectBlock.transform);
        if (i == 3 || i == 4)
        {
            instance.transform.localPosition = new Vector3(0, 1.418f, 0);
            instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (i == 5 || i == 6)
        {
            instance.transform.localPosition = new Vector3(0, 0.5f, 0);
            instance.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (i == 7 || i == 8)
        {
            instance.transform.localPosition = new Vector3(0, 0.5f, 0);
            instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        }        
              
        Debug.Log(Tower[i].name + "盔芭府鸥况积己");
    }

    IEnumerator _BuyTower()
    {
        if (GameManager.Instance.SelectBlock.CompareTag("MeleeBuildable"))
            MeleeTowerSpawn();
        else
            RangerTowerSpawn();

        GameManager.Instance.gold -= 20;
        GameManager.Instance.blockClicked = false;
        GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORewind();
        GameManager.Instance.SelectBlock.layer = 2;
        Destroy(GameManager.Instance.SelectBlock.GetComponent<Blocks>().instance.gameObject);

        yield return null;
    }

    IEnumerator _SellTower()
    {
        GameManager.Instance.gold += 20;
        Destroy(GameManager.Instance.tower.gameObject);
        GameManager.Instance.tower.transform.parent.GetComponent<Blocks>().isBuild = false;
        GameManager.Instance.tower.transform.parent.gameObject.layer = 0;
        Time.timeScale = gameSpeed;

        yield return null;
    }

    IEnumerator _UpgradeTower()
    {
        if (GameManager.Instance.tower.GetComponent<TestScript>().nextTower == null)
        {
            Time.timeScale = gameSpeed;
            yield return null;
        }

        GameManager.Instance.gold -= 50;
        GameObject instance = Instantiate(GameManager.Instance.tower.GetComponent<TestScript>().nextTower);
        instance.transform.SetParent(GameManager.Instance.tower.transform.parent.transform);
        instance.transform.position = GameManager.Instance.tower.transform.position;
        if (GameManager.Instance.tower.tower_class == global::Tower.Tower_Class.Pixel && GameManager.Instance.tower.tag == "Level2")
            instance.transform.position += new Vector3(0, 0.4f, 0);
        Destroy(GameManager.Instance.tower.gameObject);
        Time.timeScale = gameSpeed;

        yield return null;
    }

    public void EndBuyTower()
    {
        GameManager.Instance.blockClicked = false;
        GameManager.Instance.SelectBlock.GetComponent<Blocks>().isBuild = false;
        GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORewind();
        Destroy(GameManager.Instance.SelectBlock.GetComponent<Blocks>().instance.gameObject);
    }

    public void EndSkillUp()
    {
        Time.timeScale = gameSpeed;
        GameManager.Instance.towerClicked = false;
    }

    public void StartTime()
    {
        Time.timeScale = gameSpeed;
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void SpeedUp()
    {
        if (Time.timeScale == 1)
        {
            gameSpeed = 2;
            SpeedUI[0].SetActive(false);
            SpeedUI[1].SetActive(true);
            Time.timeScale = gameSpeed;
        }
        else if (Time.timeScale == 2)
        {
            gameSpeed = 3;
            SpeedUI[1].SetActive(false);
            SpeedUI[2].SetActive(true);
            Time.timeScale = gameSpeed;
        }
        else if (Time.timeScale == 3)
        {
            gameSpeed = 1;
            SpeedUI[2].SetActive(false);
            SpeedUI[0].SetActive(true);
            Time.timeScale = gameSpeed;
        }
    }
}
