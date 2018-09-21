using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

namespace SteeringBehaviours
{
    public class Wander : SteeringBehaviour
    {
        public float offset = 1f;
        public float rad = 1f;
        public float jitter = .2f; //rate of random point generation within radius

        private Vector3 targetDir;
        private Vector3 randomDir;
        // Use this for initialization
        public override Vector3 GetForce()
        {
            Vector3 force = Vector3.zero;
            // we need the max negative and positive float to get most random
            //HEX 0x7fff = 32767
            //this needs to have half of itself taken from itself
            float randX = Random.RandomRange(0, 0x7fff) - (0x7fff / 2);
            float randZ = Random.RandomRange(0, 0x7fff) - (0x7fff / 2);
            /*
             -32767                                         32767
             |-----------------------0-----------------------|
                        |_________________________|
                               random range
            */
            #region Calculate RandomDir
            //Create a random Direction
            randomDir = new Vector3(randX, 0f, randZ);
            //Normalise for the direction
            randomDir.Normalize();
            //aApply jitter to apply magnitude to randomDir
            randomDir *= jitter;
            #endregion

            #region Calculate targetDir
            //offset the target dir with the random dir
            targetDir += randomDir;
            //Normalise the target dir for direction
            targetDir.Normalize();
            //apply the radius as magnitude
            targetDir *= rad;
            #endregion

            //Seek logic
            Vector3 seekPos = transform.position + targetDir;
            seekPos += transform.forward * offset;

            #region GizmosGL
            GizmosGL.color = Color.red;
            GizmosGL.AddCircle(seekPos + Vector3.up * .1f, .5f, Quaternion.LookRotation(Vector3.down) );
            GizmosGL.color = Color.blue;
            Vector3 offsetPos = transform.position + transform.forward * offset;
            GizmosGL.AddCircle(offsetPos + Vector3.up * .1f, rad, Quaternion.LookRotation(Vector3.down));
            GizmosGL.color = Color.cyan;
            GizmosGL.AddLine(transform.position, offsetPos, .1f, .1f);
            #endregion

            //calculate final force
            Vector3 direction = seekPos - transform.position;

            Vector3 desiredForce = Vector3.zero;
            if(direction != Vector3.zero)
            {
                //apply weighting to direction
                desiredForce = direction.normalized * weight;
                force = desiredForce - agent.velocity;
            }

            

            return force;

        }
    }
}
