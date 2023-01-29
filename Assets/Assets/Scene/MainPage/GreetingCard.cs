using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Contains data from GreetingCardResponse and add button*/

public class GreetingCard : MonoBehaviour
{   
    public GreetingCardResponse card; 
    [SerializeField] public GameObject addButton; 
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDetails(GreetingCardResponse details) {
        card = details; 
    }

    public void onAddButtonClick(){
        Debug.Log("Add button clicked");
        ShoppingCart cart = GameObject.Find("ShoppingCart").GetComponent<ShoppingCart>();
        cart.add(card); 
        cart.getItems();
    }
}
