using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject shooter;
    public GameObject player;

	// Use this for initialization
	void Start () {
    player = GameObject.FindGameObjectWithTag ("Player");
        if(shooter != null)
		    this.transform.parent.rotation = shooter.transform.rotation;
	}
	
	// Update is called once per frame
	void Update ()
    {
	      this.transform.parent.position += new Vector3(this.transform.parent.up.x, this.transform.parent.up.y,0) * 3 * Time.deltaTime;	
	}
    void OnTriggerEnter2D(Collider2D col)
	{
        if (col.gameObject == player)
        {
            player.GetComponent<Move>().isDead = true;
            player.GetComponent<Move>().killed = "Turret";
        }
        else if(col.gameObject.tag == "Wall")
        {
            Destroy(this);
        }
    }
}
