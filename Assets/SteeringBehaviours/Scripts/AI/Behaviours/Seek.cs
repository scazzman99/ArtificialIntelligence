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
            
            if (target)
            {
                
                //get direction to target
                Vector3 vel = target.position - agent.transform.position;
                if(Vector3.Distance(target.position, agent.transform.position) < stopDist)
                {
                    return Vector3.zero;
                } 
                //normalise the vector
                vel.Normalize();
                
                return vel * weight * agent.maxSpeed;
            } else
            {
                
                return Vector3.zero;
            }
            
        }
        
       

    }
}
