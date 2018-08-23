using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringBehaviours
{
    public class AIAgentDirector : MonoBehaviour
    {

        #region Vars
        public AIAgent agent;
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
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if(Physics.Raycast(camRay, out mouseHit, 1000f))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    agent.SetTarget(mouseHit.point); //use SetTarget here for the AI agent. It will call this from AIAgent.
                }
            }
        }
    }
}