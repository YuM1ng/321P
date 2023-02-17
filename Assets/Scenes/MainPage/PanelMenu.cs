using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelMenu : MonoBehaviour
{
    public GameObject panel;
    private bool open = false; 
 
     void Start () 
     { 
        //  panel.SetActive( false); 
     }

     public void OnProfileClick() 
     { 
        Animator mAnimator = gameObject.GetComponent<Animator>(); 
        if (open){
            mAnimator.SetTrigger("close"); 
            open = false; 
        }else{
            mAnimator.SetTrigger("open"); 
            open = true; 
        }
     }

     public void OnProfilePicClick()
     {
        SceneManager.LoadScene("UserProfile");
     }
    
    }

