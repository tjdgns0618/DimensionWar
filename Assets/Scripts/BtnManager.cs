using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class BTManager : MonoBehaviour
{
    public GameObject RandomPreview;
    [Header("鸥况")]
    public GameObject[] Tower;

    public void MeleeTowerSpawn()
    {
        int i = Random.Range(0, 3);
        RandomPreview.GetComponent<FollowMouse>().Towerprefab = Tower[i];
        RandomPreview.GetComponent<Tower>().tower_type = global::Tower.Tower_Type.Meele;
        RandomPreview.GetComponent<Tower>().isMelea = true;
        RandomPreview.GetComponent<Tower>().isWall = true;
        if (i == 0)
            RandomPreview.GetComponent<Tower>().tower_class = global::Tower.Tower_Class.Pixel;
        else if(i == 1)
            RandomPreview.GetComponent<Tower>().tower_class = global::Tower.Tower_Class.RowPoly;
        else if (i == 2)
            RandomPreview.GetComponent<Tower>().tower_class = global::Tower.Tower_Class._3D;

        Instantiate(RandomPreview, Input.mousePosition, Quaternion.Euler(45,0,0));
        Debug.Log(Tower[i].name + "辟立鸥况积己");
    }

    public void RangerTowerSpawn()
    {
        int i = Random.Range(3, 9);
        RandomPreview.GetComponent<FollowMouse>().Towerprefab = Tower[i];
        RandomPreview.GetComponent<Tower>().tower_type = global::Tower.Tower_Type.Range;
        if (i == 3 || i == 4)
            RandomPreview.GetComponent<Tower>().tower_class = global::Tower.Tower_Class.Pixel;
        else if (i == 5 || i == 6)
            RandomPreview.GetComponent<Tower>().tower_class = global::Tower.Tower_Class.RowPoly;
        else if (i == 7 || i == 8)
            RandomPreview.GetComponent<Tower>().tower_class = global::Tower.Tower_Class._3D;
        Instantiate(RandomPreview, Input.mousePosition, Quaternion.Euler(45,0,0));
        Debug.Log(Tower[i].name + "盔芭府鸥况积己");
    }

    public void EndSkillUp()
    {
        Time.timeScale = 1f;
        GameManager.Instance.clicked = false;
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
