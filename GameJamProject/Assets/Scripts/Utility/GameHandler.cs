using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : Singleton<GameHandler>
{
    protected GameHandler() { }

    public Transform lane1;
    public Transform lane2;
    public Transform lane3;

    public float playerSpeed; // How fast to scroll and simulate the player speed through the world
    public float spawnSpeed; // Current time between spawns
    public float spawnInterval; // Use this to determine when we up the spawn speed
    public int score;
}
