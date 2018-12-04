using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Spikes : Tile {

	private GameObject player;

    public Sprite textureDown;
    public Sprite textureUp;
	// Use this for initialization
	protected override void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player");
        base.Start();
		

	}

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
	}

	protected override void Init()
	{
        myTexture = textureDown;
		base.Init();
	}

	public void RunInit() //this is dumb and only so init can be public too
	{
		Init();
	}
	public void OnTriggerEnter(Collider collision)
	{
		//trigger trap
		player.transform.position = new Vector3(0,0,0);
        myTexture = textureUp;
	}

}
