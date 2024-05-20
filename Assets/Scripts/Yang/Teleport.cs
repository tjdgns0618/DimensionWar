using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleport : MonoBehaviour
{
    public Transform end;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<NavMeshAgent>().Warp(end.position);
        }
    }

}
