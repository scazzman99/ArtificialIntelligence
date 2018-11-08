using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoHome
{
    public class UserInput : MonoBehaviour
    {
        public PlayerController player;
        
        // Update is called once per frame
        void Update()
        {
            //get input
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            //tell player to move with input
            player.Move(inputH, inputV);
        }
    } 
}
