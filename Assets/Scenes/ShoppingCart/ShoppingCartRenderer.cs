using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
// get all cards from ShoppingCart and render them as list of OrderItem objects onto ShoppingCart scene

public class ShoppingCartRenderer : MonoBehaviour
{
    [SerializeField] public GameObject orderItemPrefab;
    public float yPosOffset = 0;
    public TextMeshProUGUI priceDisplay;
    // Start is called before the first frame update
    void Start()
    {
        ShoppingCart cart = GameObject.Find("ShoppingCart").GetComponent<ShoppingCart>();
        render(cart); 
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

   void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        // shift canvas down for order page 
        Scene m_Scene = SceneManager.GetActiveScene();
         string sceneName = m_Scene.name;
         Debug.Log("Current scene: shoppingcartcanvs.cs"+ sceneName);

         if (sceneName == "ShoppingCartPage"){
            // destroy all and rerender new items
            foreach (Transform orderItem in gameObject.transform) {
                GameObject.Destroy(orderItem.gameObject);
            }
            ShoppingCart cart = GameObject.Find("ShoppingCart").GetComponent<ShoppingCart>();
            render(cart);
         }
    }

    // public void render(ShoppingCart cart)
    // {
    //     Debug.Log("Rendering order item"); 
    //     List<GreetingCard> cards = cart.getCards(); 
    //     int y = 0;
    //     int y_displace = -500;
    //     for (int i=0; i < cards.Count; i ++){
    //         // instantiate orderItem 
    //         Debug.Log("Instantiating OrderItem: " + cards[i].nameObj.text);
    //         Vector3 position = new Vector3(0, y + i * y_displace, 0);
    //         GameObject orderItemObj = Instantiate(orderItemPrefab, position, Quaternion.identity); 
    //         OrderItem orderItem = orderItemObj.GetComponent<OrderItem>(); 
    //         orderItem.transform.SetParent(transform, false); 
    //         Debug.Log(cards[i]);
    //         Debug.Log(orderItem); 
    //         orderItem.setGreetingCardData(cards[i]); 
    //     }
    // }

    public void render(ShoppingCart cart)
    {
        Debug.Log("Rendering order item"); 
        List<GreetingCard> cards = cart.getCards(); 
        float totalPrice = 0; 
        for (int i=0; i < cards.Count; i ++){
            // cal price
            string priceStr = cards[i].priceObj.text.Replace("$", ""); 
            float price = float.Parse(priceStr);
            totalPrice += price; 
            // instantiate orderItem 
            Debug.Log("Instantiating OrderItem: " + cards[i].nameObj.text);
            Vector3 position = new Vector3(0, yPosOffset, 0);
            GameObject orderItemObj = Instantiate(orderItemPrefab, position, Quaternion.identity); 
            OrderItem orderItem = orderItemObj.GetComponent<OrderItem>(); 
            orderItem.transform.SetParent(transform, false); 
            Debug.Log(cards[i]);
            Debug.Log(orderItem); 
            orderItem.setGreetingCardData(cards[i]); 
        }
        Debug.Log("total price:" + totalPrice.ToString());
        priceDisplay = GameObject.FindGameObjectsWithTag("totalPrice")[0].GetComponent<TMPro.TextMeshProUGUI>();
        priceDisplay.text = totalPrice.ToString();
    }

}
