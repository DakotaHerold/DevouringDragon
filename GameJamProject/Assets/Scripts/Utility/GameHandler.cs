using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : Singleton<GameHandler>
{
    protected GameHandler() { }

    public Transform lane1;
    public Transform lane2;
    public Transform lane3;

    // Store the prefabs here...
    public List<GameObject> edibles;
    public List<GameObject> towers;
    public GameObject arrowPrefab;
    public ShowPanels menuController;
    public StartOptions startOptions;
    public AudioSource fxPlayer;
    public List<AudioClip> clipList;
    public Text scoreBox;
    public bool gameOver;

    public enum AudioClipIndex
    {
        MOO1,
        MOO2,
        BAH1,
        BAH2,
        BAH3,
        YELL1,
        YELL2,
        YELL3,
        EAT1,
        EAT2,
        EAT3,
        EAT4,
        EAT5
    }

    public float playerSpeed; // How fast to scroll and simulate the player speed through the world
    public float spawnDelay; // Current time between spawns
    public float spawnInterval; // Use this to determine when we up the spawn speed
    public float forcedTowerDelay; // Time before it can drop a tower again...
    public float widthFudgeFactor; // Used to vary the x within the lanes...
    private float distanceTraveled = 0f;
    public float DistanceTravelled { get { return distanceTraveled; } set { distanceTraveled = value; } }

    public int score;

    private float lastSpawnTime1 = 0f;
    private float lastSpawnTime2 = 0f;
    private float lastSpawnTime3 = 0f;
    private float nextSpawnTime1 = 0f;
    private float nextSpawnTime2 = 0f;
    private float nextSpawnTime3 = 0f;
    //private float randOffset = 1f;
    private float lastSpawnInterval = 0f;
    private float nextSpawnInterval = 0f;
    private float lastTowerSpawn = 0;
    private float pausedPlayerSpeed = -1f;
    private float timesGrown = 0;
    private float basePlayerSpeed;

    private PlayerController player; 

    public void NewGame()
    {
        gameOver = false; 
        player = FindObjectOfType<PlayerController>();
        player.gameObject.transform.localScale = new Vector3(1, 1, 1);

        distanceTraveled = 0f;
        score = 0;
        playerSpeed = basePlayerSpeed;
        timesGrown = 0f;

        // Clean out old prefabs...
        GameObject[] edibles = GameObject.FindGameObjectsWithTag("Edible");
        foreach(GameObject go in edibles)
        {
            Destroy(go);
        }
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject go in towers)
        {
            Destroy(go);
        }
    }

    public void PauseGame()
    {
        pausedPlayerSpeed = playerSpeed;
        playerSpeed = 0f;
    }

    public void UnPauseGame()
    {
        if(pausedPlayerSpeed > 0f)
        {
            playerSpeed = pausedPlayerSpeed;
            pausedPlayerSpeed = -1f;
        }
    }

    public void GameOver()
    {
        gameOver = true; 
        PauseGame();
        startOptions.ReturnToMenu(); 
        menuController.ShowGameOverPanel(); 
    }

    private void Start()
    {
        basePlayerSpeed = playerSpeed;
        NewGame();
        PauseGame();
    }

    public void PlaySound(int index)
    {
        if(clipList.Count > index)
        {
            fxPlayer.PlayOneShot(clipList[index], menuController.GetFXVolume());
        }
    }

    public void TriggerEat()
    {
        player.TriggerEat(); 
    }

    private void FixedUpdate()
    {
        float curTime = Time.time;
        float fudgeX = (Random.value * (widthFudgeFactor * 2)) - (widthFudgeFactor);
        if (GameHandler.Instance.playerSpeed == 0)
        {
            nextSpawnTime1 += Time.deltaTime;
            nextSpawnTime2 += Time.deltaTime;
            nextSpawnTime3 += Time.deltaTime;
            nextSpawnInterval += Time.deltaTime;
            return;
        }
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
                GameObject go = Instantiate(towers[index], new Vector3(lane1.position.x + fudgeX, lane1.position.y), Quaternion.identity);
                Tower t = go.GetComponent<Tower>();
                t.Grow(timesGrown);
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
                GameObject go = Instantiate(towers[index], new Vector3(lane2.position.x + fudgeX, lane2.position.y), Quaternion.identity);
                Tower t = go.GetComponent<Tower>();
                t.Grow(timesGrown);
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
                GameObject go = Instantiate(towers[index], new Vector3(lane3.position.x + fudgeX, lane3.position.y), Quaternion.identity);
                Tower t = go.GetComponent<Tower>();
                t.Grow(timesGrown);
            }
        }
        if (curTime > nextSpawnInterval)
        {
            lastSpawnInterval = curTime;
            nextSpawnInterval = lastSpawnInterval + spawnInterval;
            playerSpeed += 1f;
            spawnDelay = spawnDelay * 0.75f;
            timesGrown += 1f;
        }
        if(scoreBox != null) scoreBox.text = "Score: " + score;
    }
}
