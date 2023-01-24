using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TMPro;
using System.Text;
using UnityEngine.SceneManagement;

public class GetUserProfile : MonoBehaviour
{
    [SerializeField] 
        private TextMeshProUGUI gender;
    
    // Start is called before the first frame update
    void Start()
    {   
        
        User user = GameObject.Find("UserManager").GetComponent<User>();
        string session_id = user.session_id.ToString();
        //User obj = GameObject.Find("UserManager").GetComponent<User>();
        // string serializedSessionId = JsonConvert.SerializeObject(Obj);
        Debug.Log(session_id);
        StartCoroutine(RetrieveUserProfile(session_id));
        
        
       
        
    }
    

     private IEnumerator RetrieveUserProfile(string session_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("session_id", session_id);

        UnityWebRequest www = UnityWebRequest.Post("https://lunar-byte-371808.et.r.appspot.com/api/fetchuserprofilesid", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            gender.text = www.downloadHandler.text;
             

        }


}
}

