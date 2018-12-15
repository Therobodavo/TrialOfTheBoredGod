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

    public float radius = 4.0f;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        
        //Getting the player object
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //If the trap has started rearing back, or should be checking for attacks, increment time
        if(hasStarted || attackLingerBool)
        {
            startTime += Time.deltaTime;
        }

        //If the time is greater than the attack delay, and the object has started rearing back, begin attacking
        if( startTime >= attackDelay && hasStarted)
        {
            hasStarted = false;
            attack = true;
            startTime = 0.0f;
            Debug.Log("Starting attack");
            attackLingerBool = true;
        }

        //If the trap has been attacking for longer than the linger duration, stop
        if(startTime >= attackLinger && attackLingerBool)
        {
            attackLingerBool = false;
            attack = false;
            startTime = 0.0f;
            Debug.Log("Ending attack");
        }

        //If attacking, check the distance between the player and the trap, killing the player if they're too close
        if(attack)
        {
            if(Vector3.Distance(player.transform.position, transform.position) < radius)
            {
                player.GetComponent<Move>().isDead = true;
            }
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
        //If the trap has not started rearing back or not currently attacking, begin rearing back
        if (hasStarted == false && attack == false)
        {
            hasStarted = true;
            Debug.Log("Initial contact");
        }
    }
}
