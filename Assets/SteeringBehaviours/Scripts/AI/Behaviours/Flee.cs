using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringBehaviours
{
    public class Flee : SteeringBehaviour
    {
        public Transform target;


        public override Vector3 GetForce()
        {
            if (target)
            {
                Vector3 vel = target.position - agent.transform.position;
                vel.Normalize();
                return -1 * vel * agent.maxSpeed;
            }
            return Vector3.zero;
        }
    }
}
