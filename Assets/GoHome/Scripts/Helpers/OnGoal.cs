using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GoHome
{
    public class OnGoal : MonoBehaviour
    {
        public UnityEvent onGoal;
        // Use this for initialization
        private void OnTriggerEnter(Collider other)
        {
            onGoal.Invoke();
        }
    } 
}
