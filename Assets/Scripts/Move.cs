using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    Rigidbody2D rigidbody;
    Vector3 currentPos;
  public  Animator animator;
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        currentPos = gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        bool isWalking = false;
        currentPos = gameObject.transform.position;
        float x = 0; float y = 0;
        if (Input.GetKey(KeyCode.A))
        {
            isWalking = true;
            x = -0.08f;
           
        }
        if (Input.GetKey(KeyCode.D))
        {
            isWalking = true;
            x = 0.08f;

        }
        if (Input.GetKey(KeyCode.W))
        {
            isWalking = true;
            y = 0.08f;

        }
        if (Input.GetKey(KeyCode.S))
        {
            isWalking = true;
            y = -0.08f;

        }
        animator.SetBool("isWalk", isWalking);
        rigidbody.MovePosition(new Vector3(currentPos.x + x, currentPos.y + y, 0));
    }
}
