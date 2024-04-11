using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFlame : MonoBehaviour
{    
    ParticleSystem triggerParticle;
    ParticleSystem.Particle d;
    SkillController skillController;
   
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    //List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>(); // 당장은 필요없음     

    List<Vector3> lastParticlePos = new List<Vector3>();

    private void Awake()
    {
        triggerParticle = GetComponent<ParticleSystem>();
        skillController = GetComponent<SkillController>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.TransformPoint(d.position) + Vector3.up, 3);
    }
    private void OnParticleTrigger()
    {
        if (skillController == null)
            return;

        int numEnter = triggerParticle.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);          
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
             d = p;
            Collider[] hits = Physics.OverlapSphere(transform.TransformPoint(p.position) + Vector3.up, 3);
            

            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                    if (hit.CompareTag("Enemy"))
                    {
                        skillController.OnObjectTriggerEnter(hit);
                    }
            }
            lastParticlePos.Add(p.position + Vector3.up);
        }
    }

}
