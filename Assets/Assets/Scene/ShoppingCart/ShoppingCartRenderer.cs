using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// get all cards from ShoppingCart and render them as list of OrderItem objects onto ShoppingCart scene

public class ShoppingCartRenderer : MonoBehaviour
{
    [SerializeField] public GameObject orderItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ShoppingCart cart = GameObject.Find("ShoppingCart").GetComponent<ShoppingCart>();
        render(cart); 
    }

    public void render(ShoppingCart cart)
    {
        Debug.Log("Rendering order item"); 
        List<GreetingCard> cards = cart.getCards(); 
        foreach (GreetingCard x in cards){
            // instantiate orderItem 
            Debug.Log("Instantiating OrderItem: " + x.nameObj.text);
            Vector3 position = new Vector3(0, 0, 0);
            GameObject orderItemObj = Instantiate(orderItemPrefab, position, Quaternion.identity); 
            OrderItem orderItem = orderItemObj.GetComponent<OrderItem>(); 
            orderItem.transform.SetParent(transform, false); 
            Debug.Log(x);
            Debug.Log(orderItem); 
            orderItem.setGreetingCardData(x); 
        }
    }
}
