using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT : MonoBehaviour
{
    [Header("Ÿ��")]
    public GameObject[] Tower;

    public void SpawnTower()
    {
        Instantiate(Tower[0], Input.mousePosition, Quaternion.identity);
    }

}
