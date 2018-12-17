using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public bool attacking = false;
    public bool farAttack = false;
    public GameObject longAttackObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //If a long attack is initiated, activate the 
	    if(farAttack)
        {
            longAttackObject.GetComponent<LongSpear>().longAttacking = true;
            attacking = true;
            farAttack = false;
        }
        //If not long attacking, send it to the object
        else
        {
            longAttackObject.GetComponent<LongSpear>().longAttacking = false;
        }
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if (attacking)
        {
            //for (int i = 0; i < enemies.Count; i++)
            if (other.tag == "Enemy")
            {
                other.GetComponent<Enemy>().damge(100,false);
        
            }
            attacking = false;
        }
    }
}
