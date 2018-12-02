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
    public float obstacleSpacing = 3;                               // Minimum space between objects
    public float startSpacing = 20;
    public float widthFix = 4;

    public int rngMax = 6;                                          // Rng generator upper bound

    private int currentObstacle = 0;                                //Index of the first active rendered obstacle.

    private Vector2 spawnPos;                                   	//First spawn point
    Vector2 modifiedSpawnPos;

    private List<int> spawnQueue = new List<int>();
    private GameObject[] activeObstacles;                               //Actual instances of obstacles.
    private int activeObstaclesSize;
    private float distance;                                           // Distance from first obstacle to the last obstacle

    void Start()
	{
        spawnQueue = new List<int>(Array.ConvertAll(spawnOrder.Split(' '), int.Parse));
        activeObstaclesSize = spawnQueue.Count;
        //Debug.Log("Koko on: " + activeObstaclesSize.ToString());
        activeObstacles = new GameObject[activeObstaclesSize];
        spawnPos = new Vector2(entryBorder, spawnHeight);
        modifiedSpawnPos = spawnPos;
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
            int randomOffSet = UnityEngine.Random.Range(0, rngMax);
            modifiedSpawnPos.x = spawnPos.x + randomOffSet + startSpacing;
            Debug.Log("New spawn pos: " + spawnPos.x);
            Debug.Log("Modified new spawn pos: " + modifiedSpawnPos.x);
            //Debug.Log("i: " + i);
            //Debug.Log(spawnQueue[i]);
            //Debug.Log("Length: " +activeObstacles.Length + "Size: " + activeObstaclesSize);
            int type = spawnQueue[i];
            //Debug.Log("Tyyppi: " + type);
            GameObject newObstacle = (GameObject)Instantiate(obstaclePrefabs[type], modifiedSpawnPos, Quaternion.identity);

            spawnPos.x += obstacleSpacing;
            activeObstacles[i] = newObstacle;
		}
        distance = spawnPos.x - entryBorder - obstacleSpacing;
        spawnPos = new Vector2(entryBorder, spawnHeight);
        //Debug.Log("distance: " + distance);
    }
    
    //Moves obstacles when game is not over.
	void Update()
	{
		if (GameControl.instance.gameOver == false) 
		{	
			if( activeObstacles[currentObstacle].transform.position.x <= exitBorder )
            {
                spawnPos.x = activeObstacles[currentObstacle].transform.position.x + distance + obstacleSpacing;
                Debug.Log("Obstacle: " + currentObstacle + " New position: " + spawnPos.x);

                int randomOffSet = UnityEngine.Random.Range(0, rngMax);
                modifiedSpawnPos.x = spawnPos.x + randomOffSet;
                Debug.Log("Obstacle: " + currentObstacle + " Modified position: " + modifiedSpawnPos.x);

                int previousIndex = currentObstacle - 1;
                if(previousIndex < 0)
                {
                    previousIndex = activeObstaclesSize - 1;
                }

                if(modifiedSpawnPos.x <= activeObstacles[previousIndex].transform.position.x + widthFix)
                {
                    modifiedSpawnPos.x += widthFix;
                }
                activeObstacles[currentObstacle].transform.position = modifiedSpawnPos;

                activeObstacles[currentObstacle].GetComponent<ObstacleComponent>().Reset();
                //spawnPos.x += obstacleSpacing;
                currentObstacle++;               

                if(currentObstacle == activeObstaclesSize)
                {
                    currentObstacle = 0;
                   // spawnPos = new Vector2(entryBorder, spawnHeight); Unnecessary reset
                }
            }
		}
	}
}