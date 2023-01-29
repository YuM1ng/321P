using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.SceneManagement;


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

            // TODO: compute position for each greeting card
            for (int i=0; i < greetingCardsResponse.Count; i ++){
                Debug.Log(greetingCardsResponse[i].name);
                GameObject greetingCard = Instantiate(greetingCardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                greetingCard.GetComponent<GreetingCard>().setDetails(greetingCardsResponse[i]);
                greetingCard.transform.SetParent(transform, false); 
            }
        }
    }
}
