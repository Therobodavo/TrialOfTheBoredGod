using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Spikes : Tile {

	private GameObject player;

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
	}

	protected override void Init()
	{
		base.Init();
	}

	public void RunInit() //this is dumb and only so init can be public too
	{
		Init();
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		//trigger trap
		player.transform.position = new Vector3(0,0,0);
	}

}
