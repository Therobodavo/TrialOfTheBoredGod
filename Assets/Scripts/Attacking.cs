using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    Vector3 mousePos;
    float mouseAng;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePos = Vector3.Normalize(mousePos);
        //Vector3 curPos = transform.position;
        //Vector3 dist = mousePos - curPos;
        //dist = Vector3.Normalize(dist);
        mouseAng = Mathf.Atan2(mousePos.y, mousePos.x);
        mouseAng *= Mathf.Rad2Deg;
        //Debug.Log(mousePos);
        Debug.Log(mouseAng);
        */
        //Getting the relative position of the mouse
        Vector3 mouseWorldPos = Input.mousePosition;

        //Getting the values for atan2
        float ang = (Mathf.Atan2(mouseWorldPos.y, mouseWorldPos.x) * Mathf.Rad2Deg) + 180;

        Debug.Log(ang);
    }
}
