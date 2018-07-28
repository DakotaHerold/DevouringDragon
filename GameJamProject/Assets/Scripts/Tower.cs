using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Collider2D bounds; 
    public int points;
    public float targetingOffsetX = 2f; 

    public delegate void EventAction();
    //public static event EventAction OnTowerEntered;
    //public static event EventAction OnTowerDestroyed;

    // Use this for initialization
    void Start () {
        bounds = GetComponent<Collider2D>(); 
	}
	
	// Update is called once per frame
	void Update () {
        float dist = GameHandler.Instance.playerSpeed * Time.deltaTime;
        //this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - dist);
        if (this.gameObject.transform.position.y < -20) TowerDestroyed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>(); 
        if(playerController != null)
            TowerSpaceEntered(playerController);
    }

    public void TowerSpaceEntered(PlayerController player)
    {
        Debug.Log("Targeting!");

        Vector3 playerDirection; 
        if(player.IsRight)
        {
            playerDirection = player.transform.right * targetingOffsetX; 
        }
        else
        {
            playerDirection = -player.transform.right * targetingOffsetX; 
        }

        GameObject arrowGO = Instantiate(GameHandler.Instance.arrowPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Arrow arrow = arrowGO.GetComponent<Arrow>();
        //arrow.transform.position = transform.position;
        arrow.direction = player.transform.position + playerDirection;
        arrow.initialized = true; 


    }

    public void TowerDestroyed()
    {
        // Here we want to eat the edible
        GameHandler.Instance.score += points;
        Destroy(gameObject, 0.2f);
        //OnTowerDestroyed();
    }
}
