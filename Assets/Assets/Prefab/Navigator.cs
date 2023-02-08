using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigator : MonoBehaviour
{
    public void onClickHome(){
        SceneManager.LoadScene("MainPage");
    }

    public void onClickCamera(){
        SceneManager.LoadScene("CameraPage");
    }

    public void onClickCart(){
        SceneManager.LoadScene("ShoppingCartPage");
    }

    public void onClickCheckOut(){
        SceneManager.LoadScene("OrderSummaryPage");
    }
}
