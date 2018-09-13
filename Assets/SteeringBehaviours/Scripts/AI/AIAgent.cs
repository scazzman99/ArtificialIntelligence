using System.Collections;
using System.Collections.Generic; //lets you use lists
using UnityEngine;
using UnityEngine.AI; //lets you use navmeshagent

    namespace SteeringBehaviours {
    public class AIAgent : MonoBehaviour {

        #region Vars
        public float maxSpeed = 10f;
        public float maxDist = 5f;
        public bool updatePos = true;
        public bool updateRot = true;
        public Vector3 velocity;
        private Vector3 force;
        private NavMeshAgent agent;
        private SteeringBehaviour[] behaviours;
        #endregion

        private void Awake()
        {
            behaviours = GetComponents<SteeringBehaviour>();
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            ComputeForces();
            ApplyVelocity();

        }

        public void ComputeForces()
        {
            //reset velocity so it doesnt stack everyupdate
            velocity = Vector3.zero;
            //loop thru behaviours
            for(int i = 0; i < behaviours.Length; i++)
            {
                //Get the force from behaviour
                Vector3 force = behaviours[i].GetForce();
                //add it to the velocity
                velocity += force;
            }
        }

        public void ApplyVelocity()
        {
            //get an offset position as a target
            Vector3 point = transform.position + velocity * Time.deltaTime;
            //apply the velocity to the transform, VIA THE AGENT
            agent.SetDestination(point);
        }


    }
}