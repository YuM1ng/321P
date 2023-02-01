using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        foreach (GreetingCard x in cart.getCards()){
            // instantiate orderItem 
            Vector3 position = new Vector3(0, 0, 0);
            GameObject orderItemObj = Instantiate(orderItemPrefab, position, Quaternion.identity); 
            OrderItem orderItem = orderItemObj.GetComponent<OrderItem>(); 
            orderItem.transform.SetParent(transform, false); 
            orderItem.setGreetingCardData(x); 
        }
    }
}
