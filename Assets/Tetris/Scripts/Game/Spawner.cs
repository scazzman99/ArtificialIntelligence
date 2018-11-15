using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class Spawner : MonoBehaviour
    {
        #region Variables

        public GameObject[] groups;

        //reference to next element
        public int nextIndex = 0;

        #endregion
        public void SpawnNext()
        {
            //check if game is not over (game manager)
            Instantiate(groups[nextIndex], transform.position, Quaternion.identity);
            //get next index randomly
            nextIndex = Random.Range(0, groups.Length);
        }
        // Use this for initialization
        void Start()
        {
            //run initial spawn
            SpawnNext();
        }

        
    } 
}
