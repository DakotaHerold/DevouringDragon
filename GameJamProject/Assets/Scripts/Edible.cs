using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour {
    public Collider bounds;
    public int points;

    public delegate void EventAction();
    public static event EventAction OnEdibleDetroyed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float dist = GameHandler.Instance.playerSpeed * Time.deltaTime;
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - dist);
        if (this.gameObject.transform.position.y < -20) EdibleDestroyed();
    }

    private void OnCollisionEnter(Collision collision)
    {
        EdibleDestroyed();
    }

    public void EdibleDestroyed()
    {
        // Here we want to eat the edible
        GameHandler.Instance.score += points;
        Destroy(gameObject, 0.2f);
        OnEdibleDetroyed();
    }
}
