using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShoppingCartCanvas : MonoBehaviour
{
    public static ShoppingCartCanvas Instance;
    
    void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }   
    }

    void Start(){
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        // shift canvas down for order page 
        Scene m_Scene = SceneManager.GetActiveScene();
         string sceneName = m_Scene.name;
         Debug.Log("Current scene: shoppingcartcanvs.cs"+ sceneName);

         if (sceneName != "ShoppingCartPage"){
            gameObject.SetActive(false);
         }else{
            gameObject.SetActive(true);
         }
    }
}
