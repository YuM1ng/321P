using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForgotPW : MonoBehaviour
{
   

     public void BackButtonClick()

    {
        SceneManager.LoadScene("LoginPage");
    }
}
