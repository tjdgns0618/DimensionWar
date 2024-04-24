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
    int gameSpeed = 1;

    public void BuyTower()
    {
        StartCoroutine(_BuyTower());
    }

    public void SellTower()
    {
        StartCoroutine(_SellTower());
    }

    public void UpgradeTower()
    {
        StartCoroutine(_UpgradeTower());
    }

    public void MeleeTowerSpawn()
    {
        if (GameManager.Instance.SelectBlock.transform.childCount >= 2)
            return;

        int i = Random.Range(0, 3);
        GameObject instance = Instantiate(Tower[i]);
        instance.transform.SetParent(GameManager.Instance.SelectBlock.transform);
        if (i == 0) {            
            instance.transform.localPosition = new Vector3(0, 2.1f, 0);
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
            instance.transform.localPosition = new Vector3(0, 2.1f, 0);
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

        GameManager.Instance.blockClicked = false;
        GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORewind();
        GameManager.Instance.SelectBlock.layer = 2;
        Destroy(GameManager.Instance.SelectBlock.GetComponent<Blocks>().instance.gameObject);

        yield return null;
    }

    IEnumerator _SellTower()
    {
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

        GameObject instance = Instantiate(GameManager.Instance.tower.GetComponent<TestScript>().nextTower);
        instance.transform.SetParent(GameManager.Instance.tower.transform.parent.transform);
        instance.transform.position = GameManager.Instance.tower.transform.position;
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
            Time.timeScale = gameSpeed;
        }
        else if (Time.timeScale == 2)
        {
            gameSpeed = 3;
            Time.timeScale = gameSpeed;
        }
        else if (Time.timeScale == 3)
        {
            gameSpeed = 1;
            Time.timeScale = gameSpeed;
        }
    }
}
