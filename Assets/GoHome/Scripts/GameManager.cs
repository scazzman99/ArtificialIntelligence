using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization; //serielize converts from one format to another. Marking it down
using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace GoHome
{
    //you can make 2 classes like this, only one can inherit from mono. Cant save monobehaviour inheriting classes anyway
    public class GameData
    {
        public int score;
        public int level;
    }


    public class GameManager : MonoBehaviour
    {
        //make accessible anywhere
        #region Singleton
        public static GameManager Instance = null;

        private void Awake()
        {
            Instance = this;
            //path to the game data folder. It will be wherever assets is as a base.
            fullPath = Application.dataPath + "/GoHome/Data/" + fileName + ".xml";
            if (File.Exists(fullPath))
            {
                //load file and contents
                Load(); 
            }
        }

        private void OnDestroy()
        {
            Instance = null;
            Save();
        }
        #endregion

        public int currentLevel = 0;
        public int currentScore = 0;
        public bool isGameRunning = true;
        public Transform levelContainer;
        [Header("UI")]
        public Text scoreText;
        [Header("Game Saves")]
        public string fileName = "Game Data";


        private Level[] levels;
        private string fullPath;
        private GameData data = new GameData();
        

        
        private void Start()
        {
            //populate level array with level object
            
            levels = levelContainer.GetComponentsInChildren<Level>(true);
            SetLevel(currentLevel);
            scoreText.text = "Score: " + currentScore;
            
        }

        public void GameOver()
        {
            isGameRunning = false;
            Debug.Log("Game Over");
        }

        public void AddScore(int scoreAdd)
        {
            currentScore += scoreAdd;
            scoreText.text = "Score: " + currentScore;
        }

        //disable all levels except given index
        private void SetLevel(int levelIndex)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                //get level gameobject
                GameObject level = levels[i].gameObject;
                //disable level
                level.SetActive(false);
                if(i == levelIndex)
                {
                    level.SetActive(true);
                }
            }
        }

        public void NextLevel()
        {
            //incriment the level
            currentLevel++;
            //if the level is out of the array
            if(currentLevel >= levels.Length)
            {
                //end the game
                GameOver();
            } else
            {
                //update current level
                SetLevel(currentLevel);
            }
        }


        #region Save/Load Functions
        void Save()
        {
            data.score = currentScore;
            data.level = currentLevel;
            //var could be XmlSerializer ar is just a blank that becomes whatever it needs
            var serializer = new XmlSerializer(typeof(GameData));
            //while using a resource do this
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                //convert the class for data into xml and use stream
                serializer.Serialize(stream, data);
            }
        }

        void Load()
        {
            
            var serializer = new XmlSerializer(typeof(GameData));
            
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {

                data = serializer.Deserialize(stream) as GameData;
            }

            currentLevel = data.level;
            currentScore = data.score;
        }
        #endregion
    }
}
