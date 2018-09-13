using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringBehaviours
{
    public class Seek : SteeringBehaviour
    {
        public Transform target;
        public float stopDist;
        // Use this for initialization
 

        public override Vector3 GetForce()
        {
            //get direction to target
            Vector3 vel = target.position - agent.transform.position;
            //normalise the vector
            vel.Normalize();
            return vel * agent.maxSpeed;
        }
        
       

    }
}
