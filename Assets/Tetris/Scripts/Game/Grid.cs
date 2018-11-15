using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class Grid : MonoBehaviour
    {
        #region Singleton
        //it will be null on decleration
        public static Grid Instance = null;
        private void Awake()
        {
            Instance = this;
        }
        private void OnDestroy()
        {
            Instance = null;
        }
        #endregion
        public int width = 10, height = 20;
        public Transform[,] data;

        private void OnDrawGizmos()
        {

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Gizmos.DrawWireCube(new Vector3(x, y), Vector3.one);
                }
            }
        }

        // Use this for initialization
        void Start()
        {
            data = new Transform[width, height];
        }

        //check if position is in border
        public bool InsideBorder(Vector2 pos)
        {
            //Truncate the vector
            int x = (int)pos.x;
            int y = (int)pos.y;

            //is the pos within the bounds of grid
            if(x >= 0 && x < width && y >= 0)
            {
                //inside border
                return true;
            }
            //outside border
            return false;
        }
        //delete row with given y coordinate
        public void DeleteRow(int y)
        {
            //loop thru row using x - width
            for (int x = 0; x < width; x++)
            {
                //destroy each element
                Destroy(data[x, y].gameObject);
                //clear array index
                data[x, y] = null;

            }
        }

        //shifts row down one square in y coordinate
        public void DecreaseRow(int y)
        {
            //loop thru row
            for (int x = 0; x < width; x++)
            {
                //check if index != null
                if (data[x, y] != null)
                {
                    //move one towards bottom
                    data[x, y - 1] = data[x, y];
                    data[x, y] = null;
                    //update block pos
                    data[x, y - 1].position += Vector3.down;
                }
            }
        }
        //shifts row above from given y
        public bool IsRowFull(int y)
        {
            for (int x = 0; x < width; x++)
            {
                //if cell empty
                if(data[x,y] == null)
                {
                    return false;
                }
            }
            //we found a full row
            return true;
        }
        //delete full rows
        public int DeleteFullRows()
        {
            int clearedRows = 0;

            //loop thru rows
            for (int y = 0; y < height; y++)
            {
                if (IsRowFull(y))
                {
                    //add row to count
                    clearedRows++;
                    //delete row
                    DeleteRow(y);
                    //decrease row from above
                    DecreaseRowsAbove(y + 1);
                    //decrease current y coordinate
                    //so we dont skip next row
                    y--;
                }
            }

            //if there are rows we cleared
            if(clearedRows > 0)
            {
                //tell game manager how many were cleared
            }

            return clearedRows;
        }
        //Rounds vector2 to nearest whole number
        public Vector2 RoundVec2(Vector2 v)
        {
            float roundX = Mathf.Round(v.x);
            float roundY = Mathf.Round(v.y);
            return new Vector2(roundX, roundY);
        }

        public void DecreaseRowsAbove(int y)
        {
            //loop each row starting from y
            for (int i = y; i < height; i++)
            {
                //Decrease each row
                DecreaseRow(i);
            }
        }
       
    } 
}
