using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public enum State
    {
        Patrol = 0,
        Seek = 1
    }
    public Transform target; // This will be for when the enemy seeks the player

    public Transform waypointParent; // create a collection of transforms, currently the array is null, as we have set nothing.
    private Transform[] waypoints;
    int currentIndex = 1; //parent included so start at 1
    public NavMeshAgent agent;
    public float stoppingDist = .5f;
    public State currentState = State.Patrol; //object type State enum set to Patrol
    public float seekRadius = 5f;

    
    //CTRL + M + O (Fold Code)
    //CTRL + M + P (Unfold Code)

	// Use this for initialization
	void Start () {
        waypoints = waypointParent.GetComponentsInChildren<Transform>(); //Get's the details of waypoints under waypointParent (given thru inspector) and makes them type Transform for array

	}
	
	// Update is called once per frame
	void Update ()
    {
        //Switch current state. Ifi n patrol call patrol, if in seek call seek
        switch (currentState) //equivelant to a bunch of else if statements
        {
            case State.Patrol:
                //patrol statements
                Patrol();
                break;
            case State.Seek:
                //seek statements
                Seek();
                break;
            default:
                break;
        }
        

    }

    //Patrol state method. Needs everything that we wrote in update previously
    void Patrol()
    {
        Transform point = waypoints[currentIndex];

        float distance = Vector3.Distance(transform.position, point.position);  //.Distance measures distance between 2 things. in our case transorm.position & current position, point.position

        if (distance < stoppingDist) //now if our ai is close enough to the current waypoint he will go to the next one
        {
          
            
            currentIndex++;   //currentIndex = currentIndex + 1;
            if (currentIndex >= waypoints.Length) //Checks if the currentIndex is still in the array. Reset to 1 if not.
            {
                currentIndex = 1;
            }
        }
        agent.SetDestination(point.position); // targets the waypoint at current array index.
        
        //An attack on the player character requires the target for SetDestination to be PLAYER_VAR.transform.position
        //transform.position = Vector3.MoveTowards(transform.position, point.position, 0.05f); //figures out its next target position from just waypoints ALONE.

        float distToTarget = Vector3.Distance(transform.position, target.position);
        if(distToTarget <= seekRadius) // If enemy is inside player range target them switch to seek
        {
            currentState = State.Seek;
        }
    }

    //Seek state method. Seek out the player
    void Seek()
    {
        agent.SetDestination(target.position); //seek out the player with target.position
        float distToTarget = Vector3.Distance(transform.position, target.position);
        if(distToTarget > seekRadius) //If enemy has left range of player
        {
            currentState = State.Patrol;
        }
    }
}
