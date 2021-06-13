using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    float getMultiplier() {
        Debug.Log(part.name);
        if(part.name == "ChargedBeamParticle"){

            return 10;
        }
        if(part.name == "MissleParticle"){

            return 20;
        }
        return 1;
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        if (numCollisionEvents > 0 && other.CompareTag("Interactable"))
        {
            if(other.GetComponent<Rigidbody>() != null)
            {
                Vector3 pos = collisionEvents[0].intersection;
                Vector3 force = collisionEvents[0].velocity * 10 * getMultiplier();

                other.GetComponent<Rigidbody>().AddForceAtPosition(force, pos);
            }
        }
    }
}
