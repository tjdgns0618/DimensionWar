using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTManager : MonoBehaviour
{
    [Header("鸥况")]
    public GameObject[] Tower;

    public void MeleeTowerSpawn()
    {
        int i = Random.Range(0, 2);
        Instantiate(Tower[i], Input.mousePosition, Quaternion.Euler(45,0,0));
        Debug.Log(Tower[i].name + "辟立鸥况积己");
    }

    public void RangerTowerSpawn()
    {
        int i = Random.Range(2, 6);
        Debug.Log(1);
        Debug.Log(i);
        Instantiate(Tower[i], Input.mousePosition, Quaternion.Euler(45, 0, 0));
        Debug.Log(Tower[i].name + "盔芭府鸥况积己");
    }

    public void SkillLevelUp()
    {
        GameManager.Instance.uiManager.skillCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.clicked = false;
        Camera.main.transform.rotation = Quaternion.Euler(20, 0, 0);
    }
}
