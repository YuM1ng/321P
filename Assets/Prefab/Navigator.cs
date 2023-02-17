using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Navigator : MonoBehaviour
{

    [SerializeField] private string OrderEndPoint = "https://lunar-byte-371808.et.r.appspot.com/api/insertOrder/";
    public void onClickHome(){
        SceneManager.LoadScene("MainPage");
    }

    public void onClickCamera(){
        SceneManager.LoadScene("ARMenu");
    }

    public void onClickCart(){
        SceneManager.LoadScene("ShoppingCartPage");
    }

    public void onClickCheckOut(){
       StartCoroutine(TrySubmit()); 
        SceneManager.LoadScene("OrderSummaryPage");
    }


     private IEnumerator TrySubmit(){

        ShoppingCart cart = GameObject.Find("ShoppingCart").GetComponent<ShoppingCart>();
        var cards = cart.getCards();
        Debug.Log(cards);
        var InstanceString = JsonUtility.ToJson(cards);
        WWWForm form = new WWWForm();
        form.AddField("cart", InstanceString);
        Debug.Log(InstanceString);

        UnityWebRequest www = UnityWebRequest.Post(OrderEndPoint, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
          
            
            Debug.Log("Form upload complete!");
            
        }
    }
}
