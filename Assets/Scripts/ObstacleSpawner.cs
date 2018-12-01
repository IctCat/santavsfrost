using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObstacleSpawner : MonoBehaviour 
{
    public GameObject[] obstaclePrefabs;
    public string spawnOrder;                                       //Numbers from from range 1-3 represent obstacle types.
    public float exitBorder = -15;                                  //X coordinate indicating end of camera view.
    public float entryBorder = 15;                                  //X coordinate indicating start of camera view.
    public float spawnHeight = -2;                                  //Y coordinate of spawned obstacles.
    public float obstacleSpacing = 10;

    private int currentObstacle = 0;                                //Index of the first active rendered obstacle.

    private Vector2 spawnPos;                                   	//First spawn point

    private List<int> spawnQueue = new List<int>();
    private GameObject[] activeObstacles;                               //Actual instances of obstacles.
    private int activeObstaclesSize;                                    

    void Start()
	{

        spawnQueue = new List<int>(Array.ConvertAll(spawnOrder.Split(' '), int.Parse));
        activeObstaclesSize = spawnQueue.Count;
        //Debug.Log("Koko on: " + activeObstaclesSize.ToString());
        activeObstacles = new GameObject[activeObstaclesSize];
        spawnPos = new Vector2(entryBorder, spawnHeight);
        /**
         * Debugging string conversion
         * 
        foreach (int value in spawnQueue)
        {
            Debug.Log(value);
        }
        timeSinceLastSpawned = 0f;
        */

        //Initialize the columns collection.
        //obstacleObjects = new GameObject[columnPoolSize];
        //Loop through the collection... 
        for (int i = 0; i < activeObstaclesSize; i++)
        {
            spawnPos.x += obstacleSpacing;
            Debug.Log("New spawn pos: " + spawnPos.x);
            //Debug.Log("i: " + i);
            //Debug.Log(spawnQueue[i]);
            //Debug.Log("Length: " +activeObstacles.Length + "Size: " + activeObstaclesSize);
            int type = spawnQueue[i];
            //Debug.Log("Tyyppi: " + type);
            GameObject newObstacle = (GameObject)Instantiate(obstaclePrefabs[type], spawnPos, Quaternion.identity);
            activeObstacles[i] = newObstacle;

		}
	}
    
    //Moves obstacles when game is not over.
	void Update()
	{
 
		if (GameControl.instance.gameOver == false) 
		{	
			if( activeObstacles[currentObstacle].transform.position.x <= exitBorder )
            {
                spawnPos.x += obstacleSpacing;
                Debug.Log("New position: " + spawnPos.x);
                activeObstacles[currentObstacle].transform.position = spawnPos;
                currentObstacle++;
            }
		}
	}
}