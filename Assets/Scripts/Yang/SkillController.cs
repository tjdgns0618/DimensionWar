using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public void OnObjectTriggerEnter(Collider hit)
    {
        Debug.Log(hit.gameObject.name);
    }
}
