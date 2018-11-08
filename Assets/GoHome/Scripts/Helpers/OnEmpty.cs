using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GoHome
{
    public class OnEmpty : MonoBehaviour
    {
        public UnityEvent onEmpty;
        

        // Update is called once per frame
        void Update()
        {
            if (transform.childCount == 0)
            {
                //invoke the unity event
                onEmpty.Invoke();
                gameObject.SetActive(false);
            }
        }
    } 
}
