using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonControls : MonoBehaviour {
    public string scene;
    public  Animator animator1;
    public Animator animator2;
    public Animator animator3;
    public Animator back;
    public bool startHide;

    // Use this for initialization
    void Start () {
        if (startHide)
            hideButtons();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void hideButtons()
    {
        animator1.SetBool("isHide", true);
        animator2.SetBool("isHide", true);
        animator3.SetBool("isHide", true);
        if(back !=null)
       back.SetBool("isHide",false);
    }
    public void showButtons()
    {
        animator1.SetBool("isHide", false);
        animator2.SetBool("isHide", false);
        animator3.SetBool("isHide", false);
        if (back != null)
            back.SetBool("isHide", true);
    }
   public void nextScene()
    {
        SceneManager.LoadScene(scene);
    }
}
