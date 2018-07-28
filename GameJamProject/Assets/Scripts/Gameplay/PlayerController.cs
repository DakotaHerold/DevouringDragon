using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float speedIncrement = 0.1f; 
    private float maxDriftSpeed = 7.5f;
    private float swerveSpeed = 9.5f; 
    private float currentDriftSpeed = 1f; 
    private bool isRight = true;
    private bool boosted = false;
    private float baseSwerveTime = 2f;
    private float currentSwerveTime = 0.0f; 


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if(InputHandler.Instance.SwervedPressed())
        {
            // Change direction and begin boosting
            isRight = !isRight;
            boosted = true;
            currentDriftSpeed = swerveSpeed;
            currentSwerveTime = 0;
        }

        Vector3 newPos = transform.position;

        if (isRight)
        {
            newPos.x += currentDriftSpeed * Time.deltaTime;
        }
        else
        {
            newPos.x -= currentDriftSpeed * Time.deltaTime;
        }

        transform.position = newPos;
        // Increment drift speed. Checks if player is swerving and 
        if(!boosted)
        {
            currentDriftSpeed += speedIncrement;
            currentDriftSpeed = Mathf.Clamp(currentDriftSpeed, -maxDriftSpeed, maxDriftSpeed);
        }
        else
        {
            currentDriftSpeed = swerveSpeed; 
            currentSwerveTime += 0.5f * Time.deltaTime;
            if (currentSwerveTime >= baseSwerveTime)
            {
                boosted = false;
                currentDriftSpeed = maxDriftSpeed; 
            }
        }
        Debug.Log(currentDriftSpeed);

    }

    /// <returns>The X-Value of the player position</returns>
    float GetXPos()
    {
        return transform.position.x; 
    }
}
