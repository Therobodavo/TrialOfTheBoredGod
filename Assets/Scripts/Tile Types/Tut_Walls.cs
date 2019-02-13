using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_Walls : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
	{
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.transform.parent.gameObject);
        }
    }
}
