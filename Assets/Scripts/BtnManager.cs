using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTManager : MonoBehaviour
{
    [Header("Ÿ��")]
    public GameObject[] Tower;

    public void MeleeTowerSpawn()
    {
        int i = Random.Range(0, 1);
        Instantiate(Tower[i], Input.mousePosition, Quaternion.Euler(45,0,0));
        Debug.Log(Tower[i].name + "����Ÿ������");
    }

    public void RangerTowerSpawn()
    {
        int i = Random.Range(1, 3);
        Debug.Log(1);
        Debug.Log(i);
        Instantiate(Tower[i], Input.mousePosition, Quaternion.Euler(45, 0, 0));
        Debug.Log(Tower[i].name + "���Ÿ�Ÿ������");
    }


}
