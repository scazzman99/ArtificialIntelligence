using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SteeringBehaviours {
    [RequireComponent(typeof(AIAgent))]
    public abstract class SteeringBehaviour : MonoBehaviour {

        public float weight; //how much wiehgt does the behaviour have over other behaviours
        public AIAgent agent; //AIAgent owner of the behaviour
        
        void Awake() {
            agent = GetComponent<AIAgent>();
        }

        public virtual Vector3 GetForce()
        {
            return Vector3.zero;
        }
        
    }
}
