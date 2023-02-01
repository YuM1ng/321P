using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/* Contains GreetingCard */

public class OrderItem : MonoBehaviour
{   
    public GreetingCard gcObj; 
    [SerializeField] public GameObject removeButton; 
    public string id; 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setId(string _id){
        id = _id; 
    }

    public void setGreetingCardData(GreetingCard card){
        gcObj = card;
    }

    public void setPrice(string price) {
        // priceObj.text = "$" + price; 
    }

    public void setName(string name) {
        // nameObj.text = name; 
    }

    public void onRemoveButtonClick(){
        Debug.Log("Remove bbutton clicked");
        ShoppingCart cart = GameObject.Find("ShoppingCart").GetComponent<ShoppingCart>();
        cart.remove(this.gcObj); 
        cart.getCards();
    }
}
