using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    namespace SteeringBehaviours {
    public class AIAgent : MonoBehaviour {

        #region Vars
        public Transform target;
        public NavMeshAgent agent;
        #endregion

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            agent.SetDestination(target.position);
        }
    }
}