using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_SpawnEnemy : Tile {

	public GameObject enemy;

	// Use this for initialization
	protected override void Start()
	{
		base.Start();

		//Spawn Enemy
		if(enemy != null)
		{
			enemy.transform.position = transform.position;
			Instantiate(enemy);
		}
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
}
