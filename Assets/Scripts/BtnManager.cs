using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT : MonoBehaviour
{
    [Header("Ÿ��")]
    public GameObject[] Tower;

    public void SpawnTower(int i)
    {
        Instantiate(Tower[i], Input.mousePosition, Quaternion.Euler(45,0,0));
        Debug.Log(Tower[i].name + "����");
    }


}
