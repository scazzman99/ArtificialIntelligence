using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class Group : MonoBehaviour
    {
        #region Variables
        public float fallInterval = 1f;
        public float holdDuration = 1f;
        public float fastInterval = .25f;

        float holdTimer = 0f;
        float fallTimer = 0f;
        bool isFallingFaster = false;
        bool isSpacePressed = false;
        
        Spawner spawner;
        #endregion

        #region GroupFunctions

        bool isBlockOwner(int x, int y)
        {
            //get grid instance to var
            Grid grid = Grid.Instance;
            return grid.data[x, y] != null &&
                    grid.data[x, y].parent == transform;

        }

        bool isValidGridPos()
        {
            //get grid instance to var
            Grid grid = Grid.Instance;

            //loop through all children in group
            foreach (Transform child in transform)
            {
                //round child position
                Vector2 v = grid.RoundVec2(child.position);
                //not inside border
                if (!grid.InsideBorder(v))
                {
                    return false;
                }

                //truncate pos
                int x = (int)v.x;
                int y = (int)v.y;

                //if cell if NOT empty AND not part of same group
                isBlockOwner(x,y);
                    
            }
            return true;
        }

        void UpdateGrid()
        {
            Grid grid = Grid.Instance;

            //remove old children
            for (int x = 0; x < grid.width; x++)
            {
                for (int y = 0; y < grid.height; y++)
                {
                    //if grid element exists at index AND
                    //It belongs to this current group
                    if(isBlockOwner(x,y))
                    {
                        //remove block from grid
                        grid.data[x, y] = null;
                    }
                }
            }

            //add children to new positions to grid
            foreach(Transform child in transform)
            {
                //round childrens position
                Vector2 v = grid.RoundVec2(child.position);

                //truncate (cut mantissa from float) the position
                int x = (int)v.x;
                int y = (int)v.y;

                //set grid coordinates to child
                grid.data[x, y] = child;
                child.position = v;
            }
        }

        void MoveLeftOrRight()
        {
            Vector3 moveDir = Vector3.zero;

            //going left
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveDir = Vector3.left;
            }

            //going right
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveDir = Vector3.right;
            }

            //if there is any movement
            if(moveDir.magnitude > 0)
            {
                transform.position += moveDir;

                //if the new position is valid
                if (isValidGridPos())
                {
                    //its valid update grid
                    UpdateGrid();
                }
                else
                {
                    //Revert change
                    transform.position -= moveDir;
                }
            }
        }

        void MoveUpOrDown()
        {
            //is up arrow pressed
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //rotate 90 degrees
                transform.Rotate(0, 0, 90);
                //see if new pos is valid
                if (isValidGridPos())
                {
                    //its valid update grid
                    UpdateGrid();
                }
                else
                {
                    //not valid pos, revert
                    transform.Rotate(0, 0, -90);
                }
            }

            //is up arrow pressed
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //modify pos
                transform.position += Vector3.down;
                //check if valid
                if (isValidGridPos())
                {
                    //its valid update grid
                    UpdateGrid();

                    holdTimer += Time.deltaTime;

                    if(holdTimer >= holdDuration)
                    {
                        isFallingFaster = true;
                    }
                }
                else
                {
                    //not valid pos, revert
                    transform.position -= Vector3.down;
                }
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                isFallingFaster = false;
                holdTimer = 0;
            }
        }

        void DetectFullRow()
        {
            Grid.Instance.DeleteFullRows();
            //spawn next group
            spawner.SpawnNext();
            //disable script
            enabled = false;
        }

        void Fall()
        {
            //modify position
            transform.position += Vector3.down;
            //see if valid
            if (isValidGridPos())
            {
                //its valid update grid
                UpdateGrid();
            }
            else
            {
                //its NOT valid reset
                transform.position += Vector3.up;
                //detect full row
                DetectFullRow();
            }
        }
        #endregion

        #region Start&Update
        // Use this for initialization
        void Start()
        {
            spawner = FindObjectOfType<Spawner>();
            if(spawner == null)
            {
                Debug.Log("Spawner Doesn't exist in current scene. What the fuck dude?");
            }


        }

        // Update is called once per frame
        void Update()
        {
            //check if space is pressed
            //isSpacePressed = Input.GetKeyDown(KeyCode.Space);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isSpacePressed = true;
            }

            if (isSpacePressed)
            {
                //get group to fall immediately
                Fall();
            }
            else
            {
                //move left right up or down
                MoveLeftOrRight();
                MoveUpOrDown();

                //modify speed based on bool

                /* THIS CAN BECOME ONE LINE WITH TERNARY OPERATOR
                float currentInterval = fallInterval;
                if (isFallingFaster)
                {
                    currentInterval = fastInterval;
                }
                */

                //Ternary operator. basically a one line if statement
                //if bool is true, set currentInterval to fast ELSE fall;
                float currentInterval = isFallingFaster ? fastInterval : fallInterval;
                

                //increase falltimer
                fallTimer += Time.deltaTime;

                //if the falltimer reaches the currentInterval
                if(fallTimer >= currentInterval)
                {
                    //Get group to fall
                    Fall();
                    //reset timer
                    fallTimer = 0f;
                }
            }
        } 
        #endregion
    } 
}
