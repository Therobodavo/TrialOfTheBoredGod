using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSpear : MonoBehaviour
{
    public bool longAttacking = false;

    private void OnTriggerStay(Collider other)
    {
        if (longAttacking)
        {
            //for (int i = 0; i < enemies.Count; i++)
            if (other.tag == "Enemy")
            {
                //Debug.Log("Hit enemy");
                Destroy(other.gameObject);
            }
            longAttacking = false;
        }
    }

}
