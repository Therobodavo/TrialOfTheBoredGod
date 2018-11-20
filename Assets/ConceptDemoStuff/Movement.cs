using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    Rigidbody2D rigidbody;
    Animator animator;
    Vector3 currentPos;
    bool isSlime;
    public Sprite human;
    public Sprite slime;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentPos = gameObject.transform.position;
        isSlime = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (isSlime)
            GetComponent<SpriteRenderer>().sprite = slime;
        else
            GetComponent<SpriteRenderer>().sprite = human;

        updateCollider();

        currentPos = gameObject.transform.position;
        float x = 0; float y = 0;
        bool isWalking = false;
        bool transform = false;

        //GetComponent<SpriteRenderer>().sortingOrder = -1 * (int)(10* gameObject.transform.position.y);

        if (Input.GetKeyDown(KeyCode.E))
            transform = true;

        if (!transform)
        {
       
            if (Input.GetKey(KeyCode.A))
            {           
                x = -0.1f;
                if(isSlime)
                    x = -0.17f;
                isWalking = true;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                x = 0.1f;
                if (isSlime)
                    x = 0.17f;
                isWalking = true;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (Input.GetKey(KeyCode.W))
            {
                y = 0.1f;
                if (isSlime)
                    y = 0.17f;
                isWalking = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                y = -0.1f;
                if (isSlime)
                    y = -0.17f;
                isWalking = true;
            }
        }
        else
        {
            isWalking = false;
            isSlime = !isSlime;
            animator.SetBool("isSlime", isSlime);
        }
       


        animator.SetBool("isWalking", isWalking);
        rigidbody.MovePosition(new Vector3(currentPos.x + x, currentPos.y + y, 0));
    }


    public void updateCollider()
    {

        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        for (int i = 0; i < polygonCollider.pathCount; i++) polygonCollider.SetPath(i, null);
        polygonCollider.pathCount = sprite.GetPhysicsShapeCount();

        List<Vector2> path = new List<Vector2>();
        for (int i = 0; i < polygonCollider.pathCount; i++)
        {
            path.Clear();
            sprite.GetPhysicsShape(i, path);
            polygonCollider.SetPath(i, path.ToArray());
        }

    }
}
