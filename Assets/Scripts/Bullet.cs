using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject shooter;
    public GameObject player;

    private float timeCreated;
    private float timeAllowed = 5f;
	// Use this for initialization
	void Start () {
    player = GameObject.FindGameObjectWithTag ("Player");
    this.transform.parent.rotation = shooter.transform.rotation;
    timeCreated = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(Time.timeSinceLevelLoad - timeCreated >= timeAllowed)
        {
             Destroy(this.transform.parent.gameObject);
        }
	      this.transform.parent.position += new Vector3(this.transform.parent.up.x, this.transform.parent.up.y,0) * 3 * Time.deltaTime;	
	}
    void OnTriggerEnter2D(Collider2D col)
	{
         Debug.Log(col.gameObject.name);
             Debug.Log("bloop");
        if (col.gameObject == player)
        {
            player.GetComponent<Move>().isDead = true;
            player.GetComponent<Move>().killed = "Turret";
        }
        else if(col.gameObject.tag == "Wall")
        {
           
            Destroy(this.transform.parent.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
	{
         Debug.Log(col.gameObject.name);
             Debug.Log("bloop");
        if (col.gameObject == player)
        {
            Destroy(this.transform.parent.gameObject);
            player.GetComponent<Move>().isDead = true;
            player.GetComponent<Move>().killed = "Turret";
        }
        else if(col.gameObject.tag == "Wall")
        {
           
            Destroy(this.transform.parent.gameObject);
        }
    }
}
