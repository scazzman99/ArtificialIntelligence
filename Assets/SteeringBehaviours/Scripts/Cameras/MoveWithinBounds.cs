using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithinBounds : MonoBehaviour {

    public CameraBounds camBounds;
    public float moveSpeed = 15f;
    public float zoomSpeed = 50f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LateUpdate()
    {
        //move cam left and right
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(inputH, 0, inputV);
        //Position += direction * speed * deltaTime
        //can also be described as Velocity * deltaTime
        transform.position += inputDir * moveSpeed * Time.deltaTime;

        //zoom cam in and out
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        //zoomInput returns 0, -1 or 1 so it will control if we zoomed back or forward
        transform.position += transform.forward * zoomInput * zoomSpeed * Time.deltaTime;

        //cap position to cambounds
        transform.position = camBounds.GetAdjustedPosition(transform.position);
    }
}
