using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {

    public List<GameObject> TileTypes;
    public List<Sprite> TileSprites;
    public int xSize, ySize;
    public int tileSize = 64;
    private GameObject[,] tilemap;

	// Use this for initialization
	void Start () {
        GenerateTilemap(5,5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateTilemap(int x, int y)
    {
        tilemap = new GameObject[x, y];
        xSize = x;
        ySize = y;
        PlaceWalls();
    }

    void PlaceWalls()
    {
        for(int x= 0; x < xSize; x++)
        {
            for(int y = 0; y < ySize; y++)
            {
               
                tilemap[x, y] = Instantiate(TileTypes[0], gameObject.transform);
                Tile_Wall t = tilemap[x, y].GetComponent<Tile_Wall>();
                t.myTexture = TileSprites[0];
                t.xIndex = x;
                t.yIndex = y;
                tilemap[x, y].transform.position = new Vector3(x, y, 0.0f);
                t.RunInit();
            }
        }
    }
}
