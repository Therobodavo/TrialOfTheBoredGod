using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool canMove = false;
    GameObject player;

    public float speed;

    Rigidbody enemyRB;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyRB = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        //enemyRB.MovePosition(new Vector3(transform.position.x + xDir, transform.position.y, 0));
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); 
        /*Transform target = player.transform;
        Vector3 targetHeading = target.position - transform.position;
        Vector3 targetDirection = targetHeading.normalized;

        //rotate to look at the player

        transform.rotation = Quaternion.LookRotation(targetDirection); // Converts target direction vector to Quaternion
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        //move towards the player
        transform.position += transform.forward * speed * Time.deltaTime;
        //enemyRB.MovePosition(transform.forward * speed * Time.deltaTime);
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if(collision.gameObject.CompareTag("Wall"))
        {
            xDir = -xDir;
        }
        */
    }
}
