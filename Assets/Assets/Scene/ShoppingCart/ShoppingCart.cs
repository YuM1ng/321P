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


/* The ShoppingCart consists of a list of GreetingCard objects.  */

public class ShoppingCart : MonoBehaviour
{

	public List<GreetingCard> cards = new List<GreetingCard>();

	public static ShoppingCart Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

	public void add(GreetingCard item)
	{   
        Debug.Log("Adding item:" + item.name); 
        cards.Add(item);
        getCards();
	}

	public void remove(GreetingCard item)
	{
        // if (cart.Contains(item)){
        //     cart.Remove(item);
        // }

        Debug.Log("Removing item:" + item.name);
        getCards();

    }

    public List<GreetingCard> getCards(){
        foreach (GreetingCard x in cards){
            Debug.Log(x.name);
        }
        return cards;
    }

    public void onClick(){
        SceneManager.LoadScene("ShoppingCartPage");
    }
}
