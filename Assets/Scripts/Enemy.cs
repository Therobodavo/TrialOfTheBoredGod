using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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


       Vector3 dist = player.transform.position -transform.position;
        float distance = Mathf.Abs(dist.magnitude);
        Debug.Log(distance);
        if (!alerted && distance < 5)
            alerted = true;
        if (alerted && distance > 10)
            alerted = false;

        if (!stuned && alerted)
           transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        invicbility -= 1 * Time.deltaTime;
        if (invicbility <= 0)
        {
            invicbility = 0;
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = 1;
            GetComponent<SpriteRenderer>().color = color;
            stuned = false;

        }          
        else
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = 0.5f;
            GetComponent<SpriteRenderer>().color = color;
        }

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
        /*if(collision.gameObject.CompareTag("Wall"))
        {
            xDir = -xDir;
        }
        */

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
//enemyRB.MovePosition(new Vector3(transform.position.x + xDir, transform.position.y, 0));

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
