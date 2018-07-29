using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Collider2D bounds; 
    public int points;
    public float targetingOffsetX = 2f;

    private bool coolingDown = false;
    private float coolTimer = 0.0f;
    private float coolTime = 0.25f;
    private float growth = 0;

    public delegate void EventAction();
    //public static event EventAction OnTowerEntered;
    //public static event EventAction OnTowerDestroyed;

    // Use this for initialization
    void Start () {
        coolTimer = coolTime; 
	}

    public void Grow(float times)
    {
        growth = times;
        bounds.bounds.Expand(times);
    }
	
	// Update is called once per frame
	void Update ()
    {
        float dist = GameHandler.Instance.playerSpeed * Time.deltaTime;
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - dist);
        if (this.gameObject.transform.position.y < -20) TowerDestroyed();

        if(coolingDown)
        {
            coolTimer -= 1f * Time.deltaTime; 
            if(coolTimer <= 0)
            {
                coolTimer = coolTime;
                coolingDown = false; 
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>(); 
        if(playerController != null)
            TowerSpaceEntered(playerController);
    }

    public void TowerSpaceEntered(PlayerController player)
    {
        if (GameHandler.Instance.gameOver)
            return; 

        if(!coolingDown)
        {
            coolingDown = true;
            //Debug.Log("Targeting!");

            Vector3 playerDirection;
            if (player.IsRight)
            {
                playerDirection = player.transform.right;
            }
            else
            {
                playerDirection = -player.transform.right;
            }

            playerDirection = player.transform.right + player.transform.up;

            if (player.transform.position.y > transform.position.y)
            {
                playerDirection = player.transform.position + playerDirection;
            }
            else
            {
                playerDirection = player.transform.position - playerDirection;
            }
            
            
            FireArrow(playerDirection);
        }
        


    }

    private void FireArrow(Vector3 direction)
    {
        GameObject arrowGO = Instantiate(GameHandler.Instance.arrowPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Arrow arrow = arrowGO.GetComponent<Arrow>();
        arrow.direction = direction;
        arrow.speed += growth * 0.25f;
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
