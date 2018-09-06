using System.Collections;
using System.Collections.Generic; //lets you use lists
using UnityEngine;
using UnityEngine.AI; //lets you use navmeshagent

    namespace SteeringBehaviours {
    public class AIAgent : MonoBehaviour {

        #region Vars
        public float maxSpeed;
        public float maxDist;
        public bool updatePos;
        public bool updateRot;
        public Vector3 velocity;
        private Vector3 force;
        private NavMeshAgent agent;
        private List<SteeringBehaviour> behaviours;
        #endregion

        private void Awake()
        {
            
        }

        private void Update()
        {
            
        }

        public void ComputeForces()
        {

        }

        public void ApplyVelocity()
        {

        }


    }
}