using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manger : MonoBehaviour {

    public static Manger Instance { get; private set; } //singlton
    public string currentScene;
  public  float[] data;
    //0 - spear
    //1 - spikes
    //2 - turret
    //3 - enemy


    private void Awake()
    {
        //setting up singlton so theres only one manger and the manger will always stay
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addData(string newData)
    {
        Debug.Log("Player killed by: " + newData);
        if(newData == "SpearTrap")
        {
            data[0] += 1;
        }
        else if (newData == "Spikes")
        {
            data[1] += 1;
        }
        else if (newData == "Turret")
        {
            data[2] += 1;
        }
        else if (newData == "Enemy")
        {
            data[3] += 1;
        }
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(currentScene);
    }
}
