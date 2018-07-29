using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public Transform scroll1;
    public Transform scroll2;
    public Transform scroll3;
    //private float spriteHeight = 34.38f;

	void Update ()
    {
        if (GameHandler.Instance.playerSpeed > 0)
        {
            float dist = GameHandler.Instance.playerSpeed * Time.deltaTime;
            GameHandler.Instance.DistanceTravelled += dist;
            //scroll1.position = new Vector3(scroll1.position.x, scroll1.position.y - dist + ((scroll1.position.y - dist < -30f) ? (3f * spriteHeight) : 0f));
            //scroll2.position = new Vector3(scroll2.position.x, scroll2.position.y - dist + ((scroll2.position.y - dist < -30f) ? (3f * spriteHeight) : 0f));
            //scroll3.position = new Vector3(scroll3.position.x, scroll3.position.y - dist + ((scroll3.position.y - dist < -30f) ? (3f * spriteHeight) : 0f));
            scroll1.position = new Vector3(scroll1.position.x, scroll1.position.y - dist + ((scroll1.position.y - dist < -30f) ? 51f : 0f));
            scroll2.position = new Vector3(scroll2.position.x, scroll2.position.y - dist + ((scroll2.position.y - dist < -30f) ? 51f : 0f));
            scroll3.position = new Vector3(scroll3.position.x, scroll3.position.y - dist + ((scroll3.position.y - dist < -30f) ? 51f : 0f));
        }
    }
}
