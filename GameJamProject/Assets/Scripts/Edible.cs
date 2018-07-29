using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour {
    public int points;
    public List<AudioSource> soundList;
	
	// Update is called once per frame
	void Update ()
    {
        if (GameHandler.Instance.playerSpeed > 0)
        {
            float dist = GameHandler.Instance.playerSpeed * Time.deltaTime;
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - dist);
            if (this.gameObject.transform.position.y < -20) EdibleDestroyed(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EdibleDestroyed(true);
        if(soundList.Count > 0)
        {
            int index = Mathf.RoundToInt(Random.value * (soundList.Count - 1));
            soundList[index].volume = GameHandler.Instance.menuController.GetFXVolume();
            soundList[index].Play();
        }
    }

    public void EdibleDestroyed(bool byPlayer = true)
    {
        // Here we want to eat the edible
        if(byPlayer) GameHandler.Instance.score += points;
        Destroy(gameObject, 0.2f);
    }
}
