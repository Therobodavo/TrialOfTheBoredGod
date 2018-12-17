using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    Rigidbody2D rigidbody;
    Vector3 currentPos;
    public  Animator animator;
    public GameObject canvas;
    public  bool isDead;
    public string killed; //contains what killed player;
    bool finalDeath = false;
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        currentPos = gameObject.transform.position;
        isDead = false;
    }
	
	// Update is called once per frame
	void Update () {
        bool isWalking = false;
        if (!isDead)
        {
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
        else
        {

            if (!canvas.activeSelf)
                canvas.SetActive(true);
 
            animator.SetBool("isWalk", false);
            animator.SetBool("isDead", true);
            if (finalDeath == false)
            {
                Manger.Instance.addData(killed);
                killed = null;
            }

            if (Input.GetKeyUp(KeyCode.R))
                Manger.Instance.reloadScene();
            finalDeath = true;
        }
        
    }
}
