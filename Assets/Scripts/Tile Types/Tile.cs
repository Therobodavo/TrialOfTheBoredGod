using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ALL TILES MUST HAVE SPRITE AND ROTATION SET BY THE LEVEL GENERATOR
 * ANY TILES THAT NEED EXTRA FUNCTIONALITY SHOULD EXTEND THIS SCRIPT
 * ANY TILES THE LEVEL GENERATOR NEEDS TO KNOW ABOUT SHOULD BE ADDED TO ITS TILE TYPES PROPERTY IN THE INSPECTOR
 * LOOK AT Tile_Wall for how to do this
 */
public class Tile : MonoBehaviour {

    public Sprite myTexture;
    public float myRotation = 0.0f;
    public string layerName;
    public int xIndex;
    public int yIndex;
	// Use this for initialization
	protected virtual void Start () {

	}
	
	// Update is called once per frame
	protected virtual void Update () {
		// this probably shouldnt have anything in it bc it will affect every tile
	}

    protected virtual void Init()
    {
        SpriteSetup();
        SetLayer(layerName);
    }

    public void SpriteSetup()
    {
        SpriteRenderer spriteRender = gameObject.GetComponent<SpriteRenderer>();
        spriteRender.sprite = myTexture;
        gameObject.transform.Rotate(new Vector3(0.0f,0.0f,myRotation));//only rotate on one axis bc 2d
    }

    public void SetLayer(string newLayer)
    {
        int l = LayerMask.NameToLayer(newLayer);
        if(l == -1) //should check for an error, this is to keep away making dumb typos lol
        {
            Debug.LogError(gameObject + " layer not found: " + newLayer);
        }
        gameObject.layer = l;
    }

}
