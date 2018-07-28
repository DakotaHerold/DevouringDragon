﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : Singleton<GameHandler>
{
    protected GameHandler() { }

    public Transform lane1;
    public Transform lane2;
    public Transform lane3;

    // Store the prefabs here...
    public List<GameObject> edibles;
    public List<GameObject> towers;

    public float playerSpeed; // How fast to scroll and simulate the player speed through the world
    public float spawnDelay; // Current time between spawns
    public float spawnInterval; // Use this to determine when we up the spawn speed
    public float forcedTowerDelay; // Time before it can drop a tower again...
    public float widthFudgeFactor; // Used to vary the x within the lanes...

    public int score;

    private float lastSpawnTime1 = 0f;
    private float lastSpawnTime2 = 0f;
    private float lastSpawnTime3 = 0f;
    private float nextSpawnTime1 = 0f;
    private float nextSpawnTime2 = 0f;
    private float nextSpawnTime3 = 0f;
    //private float randOffset = 1f;
    private float lastSpawnInterval = 0f;
    private float lastTowerSpawn = 0;

    private void FixedUpdate()
    {
        float curTime = Time.time;
        float fudgeX = (Random.value * (widthFudgeFactor * 2)) - (widthFudgeFactor);
        if (curTime > nextSpawnTime1)
        {
            lastSpawnTime1 = curTime;
            nextSpawnTime1 = lastSpawnTime1 + (spawnDelay * (1f + Random.value));
            int index = Mathf.RoundToInt(Random.value);
            if ((index > 0) || (curTime < lastTowerSpawn + forcedTowerDelay))
            {
                index = Mathf.RoundToInt(Random.value * (edibles.Count-1));
                Instantiate(edibles[index], new Vector3(lane1.position.x + fudgeX,lane1.position.y), Quaternion.identity);
            }
            else
            {
                lastTowerSpawn = curTime;
                index = Mathf.RoundToInt(Random.value * (towers.Count-1));
                Instantiate(towers[index], new Vector3(lane1.position.x + fudgeX, lane1.position.y), Quaternion.identity);
            }
        }
        if (curTime > nextSpawnTime2)
        {
            lastSpawnTime2 = curTime;
            nextSpawnTime2 = lastSpawnTime2 + (spawnDelay * (1f + Random.value));
            int index = Mathf.RoundToInt(Random.value);
            if ((index > 0) || (curTime < lastTowerSpawn + forcedTowerDelay))
            {
                index = Mathf.RoundToInt(Random.value * (edibles.Count-1));
                Instantiate(edibles[index], new Vector3(lane2.position.x + fudgeX, lane2.position.y), Quaternion.identity);
            }
            else
            {
                lastTowerSpawn = curTime;
                index = Mathf.RoundToInt(Random.value * (towers.Count-1));
                Instantiate(towers[index], new Vector3(lane2.position.x + fudgeX, lane2.position.y), Quaternion.identity);
            }
        }
        if (curTime  > nextSpawnTime3)
        {
            lastSpawnTime3 = curTime;
            nextSpawnTime3 = lastSpawnTime3 + (spawnDelay * (1f + Random.value));
            int index = Mathf.RoundToInt(Random.value);
            if ((index > 0) || (curTime < lastTowerSpawn + forcedTowerDelay))
            {
                index = Mathf.RoundToInt(Random.value * (edibles.Count-1));
                Instantiate(edibles[index], new Vector3(lane3.position.x + fudgeX, lane3.position.y), Quaternion.identity);
            }
            else
            {
                lastTowerSpawn = curTime;
                index = Mathf.RoundToInt(Random.value * (towers.Count-1));
                Instantiate(towers[index], new Vector3(lane3.position.x + fudgeX, lane3.position.y), Quaternion.identity);
            }
        }
        if ((curTime - lastSpawnInterval) > spawnInterval)
        {
            lastSpawnInterval = curTime;
            playerSpeed += 1f;
            spawnDelay = spawnDelay * 0.75f;
        }
    }
}
