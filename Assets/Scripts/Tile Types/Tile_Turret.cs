using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Turret : Tile
{
    //References
    private GameObject player;
    public GameObject bullet;

    //Variables
    float lastShot = 0;
    float delay = 0.8f;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //Get Vector between turret and player
        Vector3 dir = player.transform.position - this.transform.position;
        
        //if distance between turret and player is less than 8 units
        if(dir.magnitude < 8f)
        {
            //If the player is in direct sight with the turret
            Ray2D ray = new Ray2D((this.transform.position + dir.normalized),dir);
            int layerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
            layerMask = ~layerMask;
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, dir, (int) dir.magnitude,layerMask);
            if(hit.collider != null)
            {   
                if(hit.collider.tag == "Player")
                {
                    //Get the angle from the turret to the player
                    float dirAngle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
                    this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, dirAngle - 90);

                    //Shot delay
                    if(Time.timeSinceLevelLoad - lastShot >= delay)
                    {
                        lastShot = Time.timeSinceLevelLoad;
                    
                        //Creates bullet
                        bullet.transform.position = new Vector3(this.transform.position.x + this.transform.up.x, this.transform.position.y + this.transform.up.y, 0);
                        bullet.transform.GetChild(0).GetComponent<Bullet>().shooter = this.gameObject;
                        GameObject bul = Instantiate(bullet);
                    }
                }
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
}
