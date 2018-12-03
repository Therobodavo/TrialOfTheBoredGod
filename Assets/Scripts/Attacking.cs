using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    Vector3 mousePos;
    float mouseAng;
    float halfWidth;
    float halfHeight;
    public GameObject rotPoint;
    public GameObject spear;
    Spear spearScript;

    List<GameObject> enemies = new List<GameObject>();
    bool attacking = false;

    bool shortAtk = false;
    bool LongAtk = false;
    float delayTimer = 0;
    public Animator animator;

    // Use this for initialization
    void Start ()
    {
		float camWidth = Camera.main.pixelWidth;
        float camHeight = Camera.main.pixelHeight;
        Debug.Log(camWidth + " " + camHeight);
        halfWidth = camWidth / 2.0f;
        halfHeight = camHeight / 2.0f;
        GameObject[] tempEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        spearScript = spear.GetComponent<Spear>();
        foreach (GameObject go in tempEnemies)
        {
            enemies.Add(go);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

        ///Determine width and height, find center, get mouse pos, subtract center form mouse pos, determine angle from that
        /*
        //Vector3 screenPos = Camera.main
        //mousePos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        //mousePos = Camera.main;
        //mousePos = Vector3.Normalize(mousePos);
        //Vector3 curPos = transform.position;
        //Vector3 dist = mousePos - curPos;
        //dist = Vector3.Normalize(dist);
        mouseAng = Mathf.Atan2(mousePos.y, mousePos.x);
        mouseAng *= Mathf.Rad2Deg;
        //Debug.Log(mousePos);
        Debug.Log(mouseAng);
        
        //Getting the relative position of the mouse
        Vector3 mouseWorldPos = Input.mousePosition;

        //Getting the values for atan2
        float ang = (Mathf.Atan2(mouseWorldPos.y, mouseWorldPos.x) * Mathf.Rad2Deg) + 180;

        Debug.Log(ang);
        */

        Vector3 mouseWorldPos = Input.mousePosition;

        mouseWorldPos = new Vector3(mouseWorldPos.x - halfWidth, mouseWorldPos.y - halfHeight, 0.0f);

        float ang = (Mathf.Atan2(mouseWorldPos.y, mouseWorldPos.x) * Mathf.Rad2Deg);

        //Debug.Log(ang);

        rotPoint.transform.rotation = Quaternion.Euler(0.0f, 0.0f, ang -90);

        //Vector3 testVec = new Vector3(transform.up.x * Mathf.Cos(ang * Mathf.Deg2Rad), transform.up.y * Mathf.Sin(ang * Mathf.Deg2Rad), 0.0f);
        //Debug.Log(testVec);
        //Debug.Log(mouseWorldPos);

        //  
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName("EndAction"))
        {
            shortAtk = false;
            LongAtk = false;
            Debug.Log("Merp");
        }
 


        if (delayTimer > 0)
        {
            delayTimer -= 1 * Time.deltaTime;
        }
        

        if(Input.GetMouseButtonDown(0) && shortAtk == false && LongAtk == false)
        {
            shortAtk = true;
         //   
        }
        if (Input.GetMouseButtonDown(1) && shortAtk == false && LongAtk == false)
        {
            LongAtk = true;

        }
        animator.SetBool("isShort", shortAtk);
        animator.SetBool("isLong", LongAtk);
        if (currentState.IsName("ShortAtk"))
        {
            spearScript.attacking = true;
        }
        else if (currentState.IsName("LongAtk2"))
            spearScript.farAttack = true;
        else
        {
            spearScript.attacking = false;
            spearScript.farAttack = false;
        }
    }
}
