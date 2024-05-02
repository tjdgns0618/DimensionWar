using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnim : MonoBehaviour
{
    public GameObject parent;

    public void parentAttack()
    {
        parent.GetComponent<Tower>().test();
    }
}