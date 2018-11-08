using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoHome
{
    public class Collectable : MonoBehaviour
    {
        
        public int value = 1;
        
        //will be called by player
       public void Collect()
        {
            GameManager.Instance.AddScore(value);
            //destroy self
            Destroy(gameObject);
        }

        
    } 
}
