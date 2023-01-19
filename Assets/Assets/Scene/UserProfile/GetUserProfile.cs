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
        
    //     string json = JsonConvert.SerializeObject(session_id);
    //     UnityWebRequest request = new UnityWebRequest("https://lunar-byte-371808.et.r.appspot.com/api/fetchuserprofilesid", "POST");
    //     // UnityWebRequest request = UnityWebRequest.Post("https://lunar-byte-371808.et.r.appspot.com/api/fetchuserprofilesid", "POST");
    //     // DownloadHandler objects are helper objects. 
    //     // When attached to a UnityWebRequest, they define how to handle HTTP response body data received from a remote server. 
    //     // Generally, they are used to buffer, stream and/or process response bodies
    //     byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
	// 	request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
	// 	request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
    //     request.SetRequestHeader("Content-Type", "application/json");
    //     request.method = UnityWebRequest.kHttpVerbPOST;
    //     Debug.Log("this the json : " + json);
    //     yield return request.SendWebRequest();
        
    //     if (request.isNetworkError || request.isHttpError)
    //     {
    //         Debug.Log(request.downloadHandler.text);
    //         Debug.LogError("Error retrieving user profile: " + request.error);
    //     }
    //     else
    //     {
    //         var userProfile = JsonUtility.FromJson<UserProfile>(request.downloadHandler.text);
    //         Debug.Log("User Profile :" + userProfile);
    
    // }
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
             

        }


}
}

