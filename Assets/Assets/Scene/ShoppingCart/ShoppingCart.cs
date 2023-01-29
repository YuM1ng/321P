using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.SceneManagement;


/* CartManager handles adding and removing items from cart tagged to a user.
Cart data is saved as ??? */

public class ShoppingCart : MonoBehaviour
{
	public List<GreetingCardResponse> cart = new List<GreetingCardResponse>();

	public static ShoppingCart Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

	public void add(GreetingCardResponse item)
	{   
        Debug.Log("Adding item:" + item.name); 
        cart.Add(item);

	}

	public void remove(GreetingCardResponse item)
	{
        if (cart.Contains(item)){
            cart.Remove(item);
        }
    }

    public void getItems(){
        foreach (GreetingCardResponse x in cart){
            Debug.Log(x.name);
        }
    }
}
