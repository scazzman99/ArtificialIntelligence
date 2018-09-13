using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    #region Variables
    //making an area in which the camera can move in z and x axis. Y unrestricted
    public Vector3 size = new Vector3(50f, 0, 20f);

    #endregion
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }

    //GetAdjustedPosition returns restricted position of camera after function runs
    public Vector3 GetAdjustedPosition(Vector3 incomingPos)
    {
        Vector3 pos = transform.position;
        Vector3 halfSize = size * 0.5f;

        //Ifi incoming pos is out of bounds of Z
        if(incomingPos.z > pos.z + halfSize.z)
        {
            //cap it
            incomingPos.z = pos.z + halfSize.z;
        }
        if(incomingPos.z < pos.z - halfSize.z)
        {
            incomingPos.z = pos.z - halfSize.z;
        }
        //Same for the bounds of x
        if(incomingPos.x > pos.x + halfSize.x)
        {
            incomingPos.x = pos.x + halfSize.x;
        }
        if(incomingPos.x < pos.x - halfSize.x)
        {
            incomingPos.x = pos.x - halfSize.x;
        }

        return incomingPos;

    }
}
