using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{

    public GameObject enemyToSpawn;
    public List<GameObject> TileTypes; // 0 =wall,1=floor
    public List<Sprite> TileSprites; //0=floor,1=wall empty,2=wall one side,3=wallcorner,4=wall opposites,5=wall 3 sides, 6 = wall no sides, 7 = error texture, 8 = corner in singlem 9 = corner in double opposite, 10 = corner in double adjacent, 11 corner in triple, 12= corner in quad
    public GameObject player;
    public int xSize, ySize;
    //public int tileSize = 64;
    private GameObject[,] tilemap;
    private int[,] intMap;

    public float percentRandomFill;
    public float percentMinPlayable;
    public float percentMaxPlayable;
    public int smoothIterations;

    public int numTraps;
    public float[] trapData = new float[4];//0=spear,1=spike,2=turret,3=enemy
    private List<Vector2> playableArea;
    private Vector2 seed;
    /* A NOTE ABOUT HOW NEIGHBORS ARE CALCULATED, FOR TILE ~T~ THE ARRAY OF NEIGHBORS IS
     * 0 | 1 | 2
     * 7 | T | 3
     * 6 | 5 | 4
     * 8 = total number of neighbors (can be innaccurate, by overcounting ner edges of the map
     */
    // Use this for initialization
    void Start()
    {
        //create a background
        FillBackground(xSize, ySize);
        GenerateTilemap(xSize, ySize);
        //place some traps or something
        SetTrapData(2, 2, 3, 4);
        SpawnTraps();
        SpawnExit();
    }

    void GenerateTilemap(int x, int y)
    {
        playableArea = new List<Vector2>();
        //create some vars
        tilemap = new GameObject[x, y];
        intMap = new int[x, y];
        xSize = x;
        ySize = y;
        //create a random seed for the level
        Randomize(intMap);

        //smooth the random map
        SmoothMap(smoothIterations);

        //check playable area
        if (!CheckPlayableArea())
        {
            RetryGeneration();
        }

        //move the player to the seed
        player.transform.position = new Vector3(seed.x, seed.y, -1.0f);
        //spawn textures
        SpawnTiles(); //creates the tiles and sets their textures
    }

    void RetryGeneration()
    {
        // Debug.ClearDeveloperConsole();
        Debug.Log("Retrying Generation...");
        Randomize(intMap);
        SmoothMap(smoothIterations);
        if (!CheckPlayableArea())
        {
            RetryGeneration();
        }
    }

    bool CheckPlayableArea()//returns true if generation resulted in too small of an area
    {
        int play = CreatePlayableList(playableArea);
        float max = xSize * ySize;
        float percentPlayable = play / max;
        Debug.Log("Max Playable tiles: " + ((xSize - 2) * (ySize - 2)));
        Debug.Log("Playable percent: " + percentPlayable);
        if (percentPlayable > percentMinPlayable && percentPlayable < percentMaxPlayable)
        {
            Debug.Log("The percentage is good");
            return true;
        }
        return false;
    }

    int CreatePlayableList(List<Vector2> playableList)
    {
        //get the seed
        for (int y = 0; y < ySize; y++)
        {
            if (intMap[xSize / 2, y] == 0 && intMap[xSize / 2, y + 1] == 0)
            {
                seed = new Vector2(xSize / 2, y);
                Debug.Log("Seed: " + seed);
                break;
            }
        }

        playableArea = new List<Vector2>();
        List<Vector2> toCheck = new List<Vector2>();
        playableArea.Add(seed);

        foreach (Vector2 element in GetNeighborsToTileCoords(GetNeighbors((int)seed.x, (int)seed.y), seed))
        {
            toCheck.Add(element);
        }

        while (toCheck.Count > 0)
        {
            Vector2 temp = toCheck[0];
            //get neighbors
            //add neighbors to tocheck if not on it or playablelist
            foreach (Vector2 element in GetNeighborsToTileCoords(GetNeighbors((int)temp.x, (int)temp.y), temp))
            {
                if (!ListContains(toCheck, element) && !ListContains(playableArea, element))
                {
                    toCheck.Add(element);
                }

            }
            //add temp to playable list
            playableArea.Add(temp);
            //remove temp from tocheck
            toCheck.RemoveAt(0);
        }
        //flood fill the shit
        Debug.Log("Playable tiles: " + playableArea.Count);
        return playableArea.Count;
    }

    bool ListContains(List<Vector2> parent, Vector2 child)
    {
        foreach (Vector2 element in parent)
        {
            if (element.x == child.x)
            {
                if (element.y == child.y)
                {
                    return true;
                }
            }
        }
        return false;
    }

    List<Vector2> GetNeighborsToTileCoords(int[] neighbors, Vector2 at) //test this
    {
        List<Vector2> toRet = new List<Vector2>();
        if (neighbors[1] == 0) { toRet.Add(new Vector2(at.x, at.y + 1)); }
        if (neighbors[3] == 0) { toRet.Add(new Vector2(at.x + 1, at.y)); }
        if (neighbors[5] == 0) { toRet.Add(new Vector2(at.x, at.y - 1)); }
        if (neighbors[7] == 0) { toRet.Add(new Vector2(at.x - 1, at.y)); }
        return toRet;
    }

    void Randomize(int[,] map) //fills a 2d array of ints with random values, serves as the seed of the generation
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                float r = Random.Range(0.0f, 1.0f);
                if (r > percentRandomFill)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    void SmoothMap(int itr) //smooths the map giving it its cavelike appearance
    {
        int[] n;
        for (int i = 0; i < itr; i++)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    n = GetNeighbors(x, y);
                    int total = n[8];
                    /*rules for smoothing
                     * total neighbors >= 5 become solid
                     * total neighbors <= 3 become empty
                     * cardinal neighbors all solid become solid
                     * cardinal neighbors all empty become empty
                     */
                    if (total >= 5)//5 or more solid, become solid
                    {
                        intMap[x, y] = 1;
                    }
                    if (total <= 3)//three or less solid, become empty
                    {
                        intMap[x, y] = 0;
                    }
                    if (n[1] == 1 && n[1] == n[3] && n[1] == n[5] && n[1] == n[7])//all cardinal neighbors are solid
                    {
                        intMap[x, y] = 1;
                    }
                    if (n[1] == 0 && n[1] == n[3] && n[1] == n[5] && n[1] == n[7])//all cardinal neighbors are empty
                    {
                        intMap[x, y] = 0;
                    }
                    if (x == 0 || y == 0 || x == xSize - 1 || y == ySize - 1)//make the borders solid
                    {
                        intMap[x, y] = 1;
                    }
                }
            }
        }
    }

    void SpawnTiles() //spawns the tiles based on the intMap
    {
        int[] neighbors;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                neighbors = GetNeighbors(x, y);
                int cardinalTotal = neighbors[1] + neighbors[3] + neighbors[5] + neighbors[7];

                switch (intMap[x, y])
                {
                    case 0:// floor
                        tilemap[x, y] = Instantiate(TileTypes[1], gameObject.transform);
                        Tile_Floor t = tilemap[x, y].GetComponent<Tile_Floor>();
                        t.myTexture = TileSprites[0];
                        t.xIndex = x;
                        t.yIndex = y;
                        tilemap[x, y].transform.position = new Vector3(x, y, 0.0f);
                        t.RunInit();
                        break;
                    case 1://wall, sorry guys this is a mess but i promise it works at least???
                        //spawn a wall tile in the right spot
                        tilemap[x, y] = Instantiate(TileTypes[0], gameObject.transform);
                        Tile_Wall t1 = tilemap[x, y].GetComponent<Tile_Wall>();
                        t1.xIndex = x;
                        t1.yIndex = y;
                        tilemap[x, y].transform.position = new Vector3(x, y, 0.0f);
                        //deal with the fukin texture and rotation
                        switch (cardinalTotal)
                        {
                            case 0://no neighbors are walls so this should have 4 sides
                                t1.myTexture = TileSprites[6]; // no rotation
                                break;

                            case 1: //one neighbors is a wall so it should have 3 sides
                                t1.myTexture = TileSprites[5];
                                //decide the rotation
                                if (neighbors[1] == 1) //north
                                {
                                    //none rotation
                                }
                                else if (neighbors[3] == 1) //east
                                {
                                    //270
                                    t1.myRotation = 270;
                                }
                                else if (neighbors[5] == 1) //south
                                {
                                    //180
                                    t1.myRotation = 180;
                                }
                                else if (neighbors[7] == 1) //west
                                {
                                    //90
                                    t1.myRotation = 90;
                                }
                                break;

                            case 2: //two neighbors are walls so it should have two sides
                                //check if corner or parallel
                                //check paralells
                                if (neighbors[1] == 1 && neighbors[1] == neighbors[5])
                                {
                                    //parallel, north/south
                                    t1.myTexture = TileSprites[4];
                                }
                                else if (neighbors[3] == 1 && neighbors[3] == neighbors[7])
                                {
                                    //parallel, east/west
                                    t1.myTexture = TileSprites[4];
                                    t1.myRotation = 90;
                                }
                                else if (neighbors[1] == 1 && neighbors[1] == neighbors[3])
                                {
                                    //corner, SW
                                    t1.myTexture = TileSprites[3];
                                    t1.myRotation = 270;
                                }
                                else if (neighbors[3] == 1 && neighbors[3] == neighbors[5])
                                {
                                    //corner, NW
                                    t1.myTexture = TileSprites[3];
                                    t1.myRotation = 180;
                                    //no rotation
                                }
                                else if (neighbors[5] == 1 && neighbors[5] == neighbors[7])
                                {
                                    //corner, 
                                    t1.myTexture = TileSprites[3];
                                    t1.myRotation = 90;
                                }
                                else if (neighbors[7] == 1 && neighbors[7] == neighbors[1])
                                {
                                    //corner, 
                                    t1.myTexture = TileSprites[3];
                                }
                                else
                                {
                                    t1.myTexture = TileSprites[7];
                                }
                                break;

                            case 3://three neighbors are walls so it should have 1 side
                                t1.myTexture = TileSprites[2];
                                if (neighbors[1] == 0) //north
                                {
                                    //90
                                    t1.myRotation = 90;
                                }
                                else if (neighbors[3] == 0) //east
                                {
                                    //none
                                    //GOOD
                                }
                                else if (neighbors[5] == 0) //south
                                {
                                    //270
                                    t1.myRotation = 270;
                                }
                                else if (neighbors[7] == 0) //west
                                {
                                    //180
                                    t1.myRotation = 180;
                                }
                                break;

                            case 4://all neighbors are walls so it should have no sides, oir be a corner in
                                //check if full or a corner in
                                int offNeighbors = neighbors[0] + neighbors[2] + neighbors[4] + neighbors[6];
                                switch (offNeighbors)
                                {
                                    case 0://quad corner in, no rotation
                                        t1.myTexture = TileSprites[12];
                                        break;
                                    case 1: //triple corner in
                                        t1.myTexture = TileSprites[11];
                                        if (neighbors[0] == 1)
                                        {
                                            t1.myRotation = 270;
                                        }
                                        else if (neighbors[2] == 1)
                                        {
                                            t1.myRotation = 180;
                                        }
                                        else if (neighbors[4] == 1)
                                        {
                                            //no rotation                                      
                                            t1.myRotation = 90;
                                        }
                                        else if (neighbors[6] == 1)
                                        {
                                        }
                                        break;
                                    case 2:
                                        //check for parallel vs adjacent
                                        if (neighbors[0] == 0 && neighbors[0] == neighbors[4])
                                        {
                                            //parallel, no rot
                                            t1.myTexture = TileSprites[9];
                                        }
                                        else if (neighbors[2] == 0 && neighbors[2] == neighbors[6])
                                        {
                                            //parallel, with rot
                                            t1.myTexture = TileSprites[9];
                                            t1.myRotation = 90;
                                        }
                                        else if (neighbors[0] == 0 && neighbors[0] == neighbors[2])
                                        {
                                            //corner, SW
                                            t1.myTexture = TileSprites[10];
                                            t1.myRotation = 90;
                                        }
                                        else if (neighbors[2] == 0 && neighbors[2] == neighbors[4])
                                        {
                                            //corner, NW
                                            t1.myTexture = TileSprites[10];
                                            //no rotation
                                        }
                                        else if (neighbors[4] == 0 && neighbors[4] == neighbors[6])
                                        {
                                            //corner, 
                                            t1.myTexture = TileSprites[10];
                                            t1.myRotation = 270;
                                        }
                                        else if (neighbors[6] == 0 && neighbors[6] == neighbors[0])
                                        {
                                            //corner, 
                                            t1.myTexture = TileSprites[10];
                                            t1.myRotation = 180;
                                        }
                                        else
                                        {
                                            t1.myTexture = TileSprites[7];
                                        }

                                        break;
                                    case 3://corner single, rotation
                                        t1.myTexture = TileSprites[8];
                                        if (neighbors[0] == 0)
                                        {
                                            t1.myRotation = 180;
                                        }
                                        else if (neighbors[2] == 0)
                                        {
                                            t1.myRotation = 90;
                                        }
                                        else if (neighbors[4] == 0)
                                        {
                                            //no rotation
                                        }
                                        else if (neighbors[6] == 0)
                                        {
                                            t1.myRotation = 270;
                                        }
                                        break;
                                    case 4:// no border wall
                                        t1.myTexture = TileSprites[1];
                                        break;
                                    default:
                                        Debug.LogError("You have too many or few non cardinal neighbors(" + offNeighbors + ") at: (" + x + ", " + y + ")");
                                        t1.myTexture = TileSprites[7];
                                        break;
                                }
                                break;
                            default://something is really wrong if this gets called
                                Debug.LogError("You have too many or few cardinal neighbors(" + cardinalTotal + ") at: (" + x + ", " + y + ")");
                                t1.myTexture = TileSprites[7];
                                break;
                        }
                        //after texture and rotation is set init the tile
                        t1.RunInit();
                        break;
                }
            }
        }
    }


    //returns an array of the neighbors of a tile and the total neighbors
    int[] GetNeighbors(int x, int y)
    {
        int[] neighbors = new int[] { 0, 0, 0,
                                      0, 0, 0,
                                      0, 0, 0 };
        int total = 0;
        //make sure it doesnt go out of bounds, out of bounds counts as a neighbor
        bool xPos = true, xNeg = true, yPos = true, yNeg = true;
        if (y - 1 < 0)//if y-1 is not greater or equal to 0
        {
            yNeg = false;
            neighbors[4] = 1;
            neighbors[5] = 1;
            neighbors[6] = 1;
            total += 3;
        }

        if (x - 1 < 0)//if x-1 is not greater or equal to 0
        {
            xNeg = false;
            neighbors[0] = 1;
            neighbors[7] = 1;
            neighbors[6] = 1;
            total += 3;
        }

        if (y + 1 >= ySize)//if y+1 is greater than size
        {
            yPos = false;
            neighbors[0] = 1;
            neighbors[1] = 1;
            neighbors[2] = 1;
            total += 3;
        }

        if (x + 1 >= xSize)//if x+1 is greater than size
        {
            xPos = false;
            neighbors[2] = 1;
            neighbors[3] = 1;
            neighbors[4] = 1;
            total += 3;
        }

        //now into checking individual squares
        if (xNeg && yPos && intMap[x - 1, y + 1] == 1)//top left
        {
            total++;
            neighbors[0] = 1;
        }
        if (yPos && intMap[x, y + 1] == 1) //top middle
        {
            total++;
            neighbors[1] = 1;
        }
        if (xPos && yPos && intMap[x + 1, y + 1] == 1)//top right
        {
            total++;
            neighbors[2] = 1;
        }
        if (xPos && intMap[x + 1, y] == 1)//middle right
        {
            total++;
            neighbors[3] = 1;
        }
        if (xPos && yNeg && intMap[x + 1, y - 1] == 1)//bottom right
        {
            total++;
            neighbors[4] = 1;
        }
        if (yNeg && intMap[x, y - 1] == 1)//bottom middle
        {
            total++;
            neighbors[5] = 1;
        }
        if (xNeg && yNeg && intMap[x - 1, y - 1] == 1)//bottom left
        {
            total++;
            neighbors[6] = 1;
        }
        if (xNeg && intMap[x - 1, y] == 1)//middle left
        {
            total++;
            neighbors[7] = 1;
        }
        neighbors[8] = total;
        return neighbors;
    }

    void FillBackground(int xS, int yS)
    {
        int offset = 30;
        for (int x = 0; x < xS + offset * 2; x++)
        {
            for (int y = 0; y < yS + offset * 2; y++)
            {
                if (x - offset < 0 || x - offset >= xS || y - offset < 0 || y - offset >= yS)
                {
                    GameObject temp = Instantiate(TileTypes[0], gameObject.transform);
                    temp.transform.position = new Vector3(x - offset, y - offset, 0.1f);
                    temp.GetComponent<Tile_Wall>().myTexture = TileSprites[1];
                    temp.GetComponent<Tile_Wall>().RunInit();
                }
                else
                {
                    GameObject temp = Instantiate(TileTypes[1], gameObject.transform);
                    temp.transform.position = new Vector3(x - offset, y - offset, 0.1f);
                    temp.GetComponent<Tile_Floor>().myTexture = TileSprites[0];
                    temp.GetComponent<Tile_Floor>().RunInit();
                }
            }
        }
    }

    void SetTrapData(int spear, int spike, int turret, int enemy)//converts the number of deaths to a percentage of deaths stores it in TrapData
    {
        float totalDeaths = spear + spike + turret + enemy;
        trapData[0] = spear / totalDeaths;
        trapData[1] = spike / totalDeaths;
        trapData[2] = turret / totalDeaths;
        trapData[3] = enemy / totalDeaths;
    }
    void SpawnTraps()
    {
        for (int t = 0; t < numTraps; t++)
        {
            int index = Random.Range(0, playableArea.Count - 1);
            Vector2 location = playableArea[index];
            //Debug.Log("index"+index);
            Debug.Log("count at trap spawn: "+playableArea.Count);
            Debug.Log("index trap spawn: "+index);
            float trap = Random.Range(0, 1.0f);
            if (trap < trapData[0])//spawn a spear
            {
                GameObject temp = Instantiate(TileTypes[2], gameObject.transform);
                temp.transform.position = new Vector3(location.x, location.y, -0.01f);
            }
            else if (trap < trapData[0] + trapData[1])//spawn spikes
            {
                GameObject temp = Instantiate(TileTypes[3], gameObject.transform);
                temp.transform.position = new Vector3(location.x, location.y, -0.01f);
            }
            else if (trap < trapData[0] + trapData[1] + trapData[2])//spawn turret
            {
                GameObject temp = Instantiate(TileTypes[4], gameObject.transform);
                temp.transform.position = new Vector3(location.x, location.y, -0.01f);
            }
            else if (trap < trapData[0] + trapData[1] + trapData[2] + trapData[3])//spawn enemy
            {
                GameObject temp = Instantiate(TileTypes[5], gameObject.transform);
                temp.GetComponent<Tile_SpawnEnemy>().enemy = Instantiate(enemyToSpawn, temp.transform);
                temp.transform.position = new Vector3(location.x, location.y, -0.01f);
            }
        }


    }

    void SpawnExit()
    {

    }
}
