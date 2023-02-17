using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubmittedCanvas : MonoBehaviour
{
    public void onSubmit(){
        gameObject.SetActive(true);
    }

    public void onReturn(){
        SceneManager.LoadScene("MainPage");
        gameObject.SetActive(false);
    }
}
