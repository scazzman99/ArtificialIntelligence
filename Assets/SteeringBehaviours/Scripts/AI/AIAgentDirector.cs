using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringBehaviours
{
    public class AIAgentDirector : MonoBehaviour
    {

        #region Vars
        public AIAgent agent;
        public Transform placeholdPoint;
        #endregion
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

     
        private void FixedUpdate()
        {

            if (Input.GetMouseButtonDown(0))
            {
                Seek seek = agent.GetComponent<Seek>();
                if (seek)
                {
                    Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit mouseHit;
                    if(Physics.Raycast(camRay, out mouseHit, 1000f))
                    {
                        placeholdPoint.position = mouseHit.point;
                        seek.target = placeholdPoint;
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(camRay.origin, camRay.origin + camRay.direction * 1000f);
        }
    }
}