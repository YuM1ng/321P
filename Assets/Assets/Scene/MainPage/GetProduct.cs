using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.SceneManagement;
using System;


public class GetProduct : MonoBehaviour
{
    [SerializeField] public GameObject greetingCardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        User user = GameObject.Find("UserManager").GetComponent<User>();
        string session_id = "guest";
        if (user != null) {
            session_id = user.session_id.ToString();
        }
        Debug.Log(session_id);
        StartCoroutine(RetrieveProduct(session_id));    
    }

    // Update is called once per frame
     private IEnumerator RetrieveProduct(string session_id)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://lunar-byte-371808.et.r.appspot.com/api/getGreetingCards");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Debug.Log("GetProduct Response: " + www.downloadHandler.text);
            var getGreetingCardResponseList = JsonUtility.FromJson<GreetingCardResponseList>(www.downloadHandler.text);   
            var greetingCardsResponse = getGreetingCardResponseList.greeting_cards; 


            int cols = 2; 
            int col_index = 0; 
            int row_index = 0; 
            int y_displace = 650;
            int x_displace = 500; 
            int y = 0; 
            int x = 0; 
            int z = 0;

            // TODO: compute position for each greeting card
            for (int i=0; i < greetingCardsResponse.Count; i ++){
                
                Debug.Log(greetingCardsResponse[i].name);
                Vector3 position = new Vector3(x, y, z);
                col_index ++; 
                col_index = col_index % cols;
                if (col_index == 0 && i != 0){
                    //new row 
                    row_index ++; 
                }
                x = col_index * x_displace ;   
                y -= row_index * y_displace; 

                GameObject greetingCard = Instantiate(greetingCardPrefab, position, Quaternion.identity);
                greetingCard.GetComponent<GreetingCard>().setName(greetingCardsResponse[i].name);
                greetingCard.GetComponent<GreetingCard>().setPrice(greetingCardsResponse[i].price);

                // set gc image
                string rawImageBytes = greetingCardsResponse[i].image.Split(',')[1];
                greetingCard.GetComponent<GreetingCard>().setImage(rawImageBytes);
                
                greetingCard.transform.SetParent(transform, false); 
            }
        }
    }

}
