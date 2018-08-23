using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    namespace SteeringBehaviours {
    public class AIAgent : MonoBehaviour {

        #region Vars
        public NavMeshAgent agent;
        private Vector3 point = Vector3.zero;
        #endregion

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if (point.magnitude > 0) // if the point magnitude is literally anything then run
            {
                agent.SetDestination(point);
            }
        }

        public void SetTarget(Vector3 point) //this is our own function that will be called from Director
        {
            this.point = point;
        }
    }
}