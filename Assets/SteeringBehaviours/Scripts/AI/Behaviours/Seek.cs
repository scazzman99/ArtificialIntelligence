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
        void Start()
        {

        }

        public override Vector3 GetForce()
        {
            return base.GetForce();
        }
        
       

    }
}
