using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT : MonoBehaviour
{
    [Header("타워")]
    public GameObject[] Tower;

    public void SpawnTower()
    {
        Instantiate(Tower[0], Input.mousePosition, Quaternion.Euler(15,0,0));
        Debug.Log(Tower[0].name + "생성");
    }


}
