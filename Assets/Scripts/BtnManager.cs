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

    public void BuyTower()
    {
        StartCoroutine(_BuyTower());
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

    public void EndBuyTower()
    {
        GameManager.Instance.blockClicked = false;
        GameManager.Instance.SelectBlock.GetComponent<Blocks>().isBuild = false;
        GameManager.Instance.uiManager.BuyPaenl.GetComponent<DOTweenAnimation>().DORewind();
    }

    public void EndSkillUp()
    {
        Time.timeScale = 1f;
        GameManager.Instance.towerClicked = false;
    }

    public void SpeedUp()
    {
        if (Time.timeScale == 1f)
            Time.timeScale = 2f;
        else if (Time.timeScale == 2f)
            Time.timeScale = 3f;
        else if (Time.timeScale == 3f)
            Time.timeScale = 1f;
    }
}
