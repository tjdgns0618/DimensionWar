using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiggertwes : MonoBehaviour
{
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    //List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>(); // 당장은 필요없음     

    List<Vector3> lastParticlePos = new List<Vector3>();
    
    private void OnParticleTrigger()
    {
       // if (skillController == null)
            return;

        //int numEnter = triggerParticle.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);          
      //  for (int i = 0; i < numEnter; i++)
        {
          // ParticleSystem.Particle p = enter[i];

          //  Collider[] hits = Physics.OverlapSphere(transform.TransformPoint(p.position) + Vector3.up, skillController.MySkillData.colliderSize);

            //if (hits.Length > 0)
            {
               // foreach (var hit in hits)
                    //skillController.OnObjectTriggerEnter(hit);
            }

            //lastParticlePos.Add(p.position + Vector3.up);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
