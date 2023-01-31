using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/* Contains data from GreetingCardResponse and add button*/

public class OrderItem : MonoBehaviour
{   
    [SerializeField] public TextMeshProUGUI  nameObj; 
    [SerializeField] public TextMeshProUGUI  priceObj; 
    [SerializeField] public GameObject addButton; 
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPrice(string price) {
        priceObj.text = "$" + price; 
    }

    public void setName(string name) {
        nameObj.text = name; 
    }

    // public void onAddButtonClick(){
    //     Debug.Log("Add button clicked");
    //     ShoppingCart cart = GameObject.Find("ShoppingCart").GetComponent<ShoppingCart>();
    //     cart.add(this); 
    //     cart.getItems();
    // }
}
