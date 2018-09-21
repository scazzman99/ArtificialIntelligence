using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringBehaviours
{
    public class AIAgentDirector : MonoBehaviour
    {

        #region Vars
        public AIAgent[] agents;
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
                Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit mouseHit;
                if (Physics.Raycast(camRay, out mouseHit, 1000f))
                {
                    Flee flee = GetComponent<Flee>();
                    Seek seek = GetComponent<Seek>();
                    placeholdPoint.position = mouseHit.point;

                    foreach (var agent in agents)
                    {
                        
                        if (seek)
                        {
                            seek.target = placeholdPoint;
                        }
                        else if (flee)
                        {
                            flee.target = placeholdPoint;
                        }

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