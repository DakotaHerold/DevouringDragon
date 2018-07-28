using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //public Collider bounds;
    private Collider2D bounds; 
    public int points;

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
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - dist);
        if (this.gameObject.transform.position.y < -20) TowerDestroyed();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TowerSpaceEntered();
    }

    public void TowerSpaceEntered()
    {
        Debug.Log("Entered!"); 
    }

    public void TowerDestroyed()
    {
        // Here we want to eat the edible
        GameHandler.Instance.score += points;
        Destroy(gameObject, 0.2f);
        //OnTowerDestroyed();
    }
}
