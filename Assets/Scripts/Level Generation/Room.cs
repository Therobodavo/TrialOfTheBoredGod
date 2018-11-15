using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public List<Tile_Floor> myTiles;
    public int myID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MergeRoom(Room other)//merges another room with the current room
    {
        foreach(Tile_Floor t in other.myTiles)
        {
            myTiles.Add(t);
        }
    }
}
