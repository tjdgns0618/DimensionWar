using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DotDeal : MonoBehaviour
{
    EnemyController enemy;
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponentInParent<EnemyController>();
        StartCoroutine(OnDotDeal());
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OnDotDeal()
    {
        while (true)
        {
            enemy.OnDamage(1f);
            yield return new WaitForSeconds(1);
        }
    }
}
