using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour {

    // Use this for initialization
    GameObject dialogue;
    public Canvas canvas;
    void Start () {
        canvas.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        GetComponentInParent<SpriteRenderer>().sortingOrder = -1 * (int)(10 * gameObject.transform.position.y);
        
    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            Debug.Log("hit");
            canvas.enabled = true;
        }
     
    }

}
