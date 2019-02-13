using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variables
    bool canMove = false;
    GameObject player;
    public float speed;
    Rigidbody2D enemyRB;
    public float health;
    float invicbility;
    bool stuned;
    bool alerted;
    

	// Use this for initialization
	void Start ()
    {
        invicbility = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        enemyRB = GetComponent<Rigidbody2D>();
        alerted = false;
	}
	
	// Update is called once per frame
	void Update ()
    {

        //Getting the distance between the player and the object
       Vector3 dist = player.transform.position -transform.position;

        //Getting the magnitude of the distance vector
        float distance = Mathf.Abs(dist.magnitude);
        //Debug.Log(distance);

        //If the enemy is not currently alerted, and the player is close enough, alert the enemy
        if (!alerted && distance < 5)
            alerted = true;

        //If the enemy is deleted,and the player leaves the detection range, stop alerting them
        if (alerted && distance > 10)
            alerted = false;

        //If the enemy isn't stunned, and is moving, move it towards the player
        if (!stuned && alerted)
           transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        //Decrementing the time left in invinicibility
        invicbility -= 1 * Time.deltaTime;

        //If the invincibility timer has run out, change its color back to normal, and unstun it
        if (invicbility <= 0)
        {
            invicbility = 0;
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = 1;
            GetComponent<SpriteRenderer>().color = color;
            stuned = false;

        }        
        //If it is still invincible, make it slightly transparent. 
        else
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = 0.5f;
            GetComponent<SpriteRenderer>().color = color;
        }

        //If the enemy's health is below 0, destory it
        if (health <= 0)
            Destroy(gameObject);
    }

    public void damge(int dam, bool stun)
    {
        if(invicbility <= 0)
        {
            health-= dam;
            invicbility = 30 * Time.deltaTime;
        }
        stuned = stun;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject == GameObject.FindGameObjectWithTag("Player") && invicbility <= 0)
        {
            player.GetComponent<Move>().isDead = true;
            player.GetComponent<Move>().killed = "Enemy";
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
       
      
    }
}
