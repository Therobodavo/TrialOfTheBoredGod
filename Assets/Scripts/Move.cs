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
    bool godMode = false;
    Vector2 tempMove;
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        currentPos = gameObject.transform.position;
        isDead = false;
        tempMove = new Vector2();
    }
	
	// Update is called once per frame
	void Update () {
        bool isWalking = false;
        if (!isDead)
        {
            if (Input.GetKey(KeyCode.A))
            {
                isWalking = true;
                tempMove.x = -1f;

            }
            else if (Input.GetKey(KeyCode.D))
            {
                isWalking = true;
                tempMove.x = 1f;

            }
            else
            {
                isWalking = false;
                tempMove.x = 0f;
            }

            if (Input.GetKey(KeyCode.W))
            {
                isWalking = true;
                 tempMove.y = 1f;

            }
            else if (Input.GetKey(KeyCode.S))
            {
                isWalking = true;
                 tempMove.y = -1f;

            }
            else
            {
                isWalking = false;
                tempMove.y = 0f;
            }
            animator.SetBool("isWalk", isWalking);
            
        }
        else
        {
            tempMove.x = 0;
            tempMove.y = 0;
            isWalking = false;
            if (godMode == false)
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
            else
                isDead = false;
        }
        
        if(Input.GetKeyDown(KeyCode.P))
        {
            godMode = !godMode;
        }
    }

    private void FixedUpdate()
    {
        currentPos = gameObject.transform.position;
        rigidbody.MovePosition(new Vector2(currentPos.x,currentPos.y) + (new Vector2(tempMove.x,tempMove.y) * 3.8f * Time.deltaTime));
    }
}
