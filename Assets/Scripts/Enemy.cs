using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool canMove = false;
    GameObject player;

    float xDir = .1f;

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
        /*Vector3 distance = player.transform.position - transform.position;

        if(distance.magnitude < 1)
        {
            float ang = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, ang);

            transform.position += transform.forward;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        */
        enemyRB.MovePosition(new Vector3(transform.position.x + xDir, transform.position.y, 0));

	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            xDir = -xDir;
        }
    }
}
