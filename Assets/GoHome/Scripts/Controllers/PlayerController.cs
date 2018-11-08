using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoHome
{
    public class PlayerController : MonoBehaviour
    {

        public float speed = 10f;
        public float maxVelocity = 20f;
        public Rigidbody rigid;

        //constrict velocity per update
        //collect item on trigger enter
        //input method for movement

        public void Move(float inputH, float inputV)
        {
            //generate direction from input
            Vector3 direction = new Vector3(inputH, 0, inputV);
            //set velocity to directon with speed
            rigid.velocity = direction * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            //try to grab collectable from collider
            Collectable collectable = other.GetComponent<Collectable>();

            //if collectable is not null
            if (collectable)
            {
                //collect the item
                collectable.Collect();
            }
        }
    }

}