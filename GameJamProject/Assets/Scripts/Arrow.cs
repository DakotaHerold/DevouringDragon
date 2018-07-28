using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public Vector3 direction; 
    public float speed = 1f;

    public bool initialized = false; 

    private void Update()
    {
        if(initialized)
        {
            Vector3 velocity = direction * (speed * Time.deltaTime);

            Vector3 newPos = transform.position + velocity;
            transform.position = newPos;
            transform.up = direction; 

            //if (transform.position.x < 2000 || transform.position.x > 2000)
            //{
            //    Destroy(gameObject);
            //}
        }

        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(CompareTag("Player"))
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>(); 
            if(controller != null)
            {
                controller.Shrink(); 
                Destroy(gameObject);
            }
            
        }
        
    }
}
