using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Contains data from GreetingCardResponse and add button*/

public class GreetingCard : MonoBehaviour
{   
    public string card; 
    [SerializeField] public GameObject addButton; 
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDetails(string details) {
        card = details; 
    }

    public void onAddButtonClick(){
        Debug.Log("Add button clicked");
        ShoppingCart cart = GameObject.Find("ShoppingCart").GetComponent<ShoppingCart>();
        cart.add(this); 
        cart.getItems();
    }
}
