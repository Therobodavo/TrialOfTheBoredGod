using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSpear : MonoBehaviour
{
    public bool longAttacking = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (longAttacking)
        {
            //for (int i = 0; i < enemies.Count; i++)
            if (other.tag == "Enemy")
            {
                other.GetComponent<Enemy>().damge(3, true);
            }
            
        }
        longAttacking = false;
    }
   
}
