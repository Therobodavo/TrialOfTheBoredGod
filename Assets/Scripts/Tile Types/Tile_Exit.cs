using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tile_Exit : Tile
{
    public bool tutorial;
    public string nextLevel;
    private GameObject player;
    // Use this for initialization
    void Start () {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
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
        if (!tutorial)
            nextLevel = "Menu";
        if (col.gameObject == player)
        {
            Manger.Instance.currentScene = nextLevel;
            SceneManager.LoadScene(nextLevel);
        }
       
    }
}
