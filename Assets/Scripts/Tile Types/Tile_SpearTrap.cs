using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_SpearTrap : Tile
{
    CircleCollider2D outerCirc;

    float startTime = 0.0f;
    public float attackDelay = 2.0f;
    bool hasStarted = false;
    bool attack = false;

    float attackLinger = 1.0f;
    bool attackLingerBool = false;

    public GameObject player;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        outerCirc = GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(hasStarted || attackLingerBool)
        {
            startTime += Time.deltaTime;
        }

        if( startTime >= attackDelay && hasStarted)
        {
            hasStarted = false;
            attack = true;
            startTime = 0.0f;
            Debug.Log("Starting attack");
            attackLingerBool = true;
        }
        if(startTime >= attackLinger && attackLingerBool)
        {
            attackLingerBool = false;
            attack = false;
            startTime = 0.0f;
            Debug.Log("Ending attack");
        }
    }

    protected override void Init()
    {
        base.Init();
    }

    public void RunInit() //this is dumb and only so init can be public too
    {
        Init();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasStarted == false)
        {
            hasStarted = true;
            Debug.Log("Initial contact");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attack)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.transform.position = new Vector3(0, 0, 0);
                player.transform.position = new Vector3(0, 0, 0);

            }

        }
    }
    /* private void OnTriggerEnter2D(Collider other)
     {

     }

     private void OnTriggerStay2D(Collider other)
     {
         
     }
     */
}
