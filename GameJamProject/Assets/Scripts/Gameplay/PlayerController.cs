using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool movingHorizontal = true;
    private float slowedSpeedIncrement = 0.01f; 
    private float baseSpeedIncrement = 0.1f; 
    private float speedIncrement = 0.1f; 
    private float maxDriftSpeed = 7.5f;
    private float swerveSpeed = 9.5f; 
    private float currentDriftSpeed = 1f; 
    

    private bool boosted = false;
    private float baseSwerveTime = 0.75f;
    private float currentSwerveTime = 0.0f;

    private float coolDownTimer = 0.0f; 
    private float coolDownBase = 0.25f;
    private bool coolingDown = false;

    private float timerIncrement = 0.5f;

    private bool movingUp = false; 
    private float currentYSpeed = 0.0f;
    private float maxYPos; 
    private float minYPos;
    private float ySpeed = 1.0f;
    private float maxYSpeed = 10.0f;
    private float minYSpeed = 5.0f;

    private Vector2 minScale;
    private Vector2 baseScale;
    public float shrinkOffset = 0.01f;
    public float growthOffset = 0.1f; 

    private bool isRight = true;
    public bool IsRight
    {
        get
        {
            return isRight;
        }

        set
        {
            isRight = value;
        }
    }

    public bool hitWall = false; 

    // Use this for initialization
    void Start () {
        coolDownTimer = coolDownBase;
        minYPos = transform.position.y;
        maxYPos = minYPos + 6.0f;

        baseScale = new Vector2(transform.localScale.x, transform.localScale.y);
        minScale = new Vector2(0.1f, 0.1f); 
    }
	
	// Update is called once per frame
	void Update () {

        //if (hitWall)
        //    return; 

        Vector3 newPos = transform.position;

        if (coolingDown)
        {
            coolDownTimer -= timerIncrement * Time.deltaTime;
            //Debug.Log("Cooling...");
             
            if(coolDownTimer <= 0) //&& !movingUp && (newPos.y <= minYPos)
            {
                coolDownTimer = coolDownBase;
                coolingDown = false; 
                //Debug.Log("Cooldown complete");
            }
        }
        else if(InputHandler.Instance.SwervedPressed() && !coolingDown)
        {
            // Change direction and begin boosting
            IsRight = !IsRight;
            boosted = true;
            //movingUp = true; 
            coolingDown = true; 
            currentDriftSpeed = swerveSpeed;
            currentSwerveTime = 0;
        }


        if(movingHorizontal)
        {
            if (IsRight)
            {
                newPos.x += currentDriftSpeed * Time.deltaTime;
            }
            else
            {
                newPos.x -= currentDriftSpeed * Time.deltaTime;
            }
        }

        float vMovement = InputHandler.Instance.GetVerticalMovement(); 
        if (vMovement != 0)
        {
            float ySpeed = vMovement * (currentDriftSpeed/2.0f); 
            newPos.y += ySpeed * Time.deltaTime; 
        }

        //if(movingUp)
        //{
        //    ySpeed = minYSpeed;
        //    newPos.y += ySpeed * Time.deltaTime; 
        //}
        //else
        //{
        //    ySpeed = maxYSpeed;
        //    newPos.y -= ySpeed * Time.deltaTime;
        //}

        //if(newPos.y >= maxYPos)
        //{
        //    movingUp = false; 
        //}

        //if (newPos.y > maxYPos)
        //{
        //    movingHorizontal = false;
        //}
        //else if(newPos.y <= minYPos)
        //{
        //    movingHorizontal = true;
        //}

        newPos.y = Mathf.Clamp(newPos.y, minYPos, maxYPos);
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
            currentSwerveTime += timerIncrement * Time.deltaTime;
            if (currentSwerveTime >= baseSwerveTime)
            {
                boosted = false;
                currentDriftSpeed = maxDriftSpeed; 
            }
        }
        //Debug.Log(currentDriftSpeed);

        

    }

    public void Shrink()
    {
        Debug.Log("Shrink!");

        Vector3 dragonScale = transform.localScale;
        dragonScale.x -= shrinkOffset;
        dragonScale.y -= shrinkOffset;
        transform.localScale = dragonScale; 

        if(dragonScale.x <= minScale.x)
        {
            Debug.Log("Game over!");
            GameHandler.Instance.GameOver(); 
        }

        
    }

    public void Grow()
    {
        Debug.Log("Grow!");

        Vector3 dragonScale = transform.localScale;
        dragonScale.x += growthOffset;
        dragonScale.y += growthOffset;
        transform.localScale = dragonScale;
    }

    /// <returns>The X-Value of the player position</returns>
    float GetXPos()
    {
        return transform.position.x; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isRight = !isRight;
            hitWall = true;
            StartCoroutine(ResetWallHit());
        }
        
    }

    IEnumerator ResetWallHit()
    {
        yield return new WaitForSeconds(0.25f);
        hitWall = false; 
    }
}
