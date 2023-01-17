using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TMPro;
using System.Text;




public class Login : MonoBehaviour
{
    private const string PASSWORD_REGEX = "(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,24})";

    [SerializeField] private string loginEndpoint = "https://lunar-byte-371808.et.r.appspot.com/api/userlogin/";

    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private Button loginButton;
   

    public void OnLoginClick()
    {
        TMP_InputField inpuptID = GameObject.Find("UserName").GetComponent(typeof(TMP_InputField)) as TMP_InputField;
		TMP_InputField inputPswd = GameObject.Find("Password").GetComponent(typeof(TMP_InputField)) as TMP_InputField;
        string username = inpuptID.text;
		string password = inputPswd.text;   
        // User contains username and password
        User user = new User(username, password);
        string userData = JsonUtility.ToJson(user);

        ActivateButtons(false);

        StartCoroutine(TryLogin(userData));
    }

    // public void OnCreateClick()
    // {
    //     alertText.text = "Creating account...";
    //     ActivateButtons(false);

    //     StartCoroutine(TryCreate());
    // }

    private IEnumerator TryLogin(string userData)
    {
        UnityWebRequest www = UnityWebRequest.Post(loginEndpoint, "login", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(userData);
		www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
		www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        // DownloadHandler objects are helper objects. 
        // When attached to a UnityWebRequest, they define how to handle HTTP response body data received from a remote server. 
        // Generally, they are used to buffer, stream and/or process response bodies
	    www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        //return request

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            
        }
        else
        {
        Debug.Log(www.downloadHandler.text);
        Debug.Log("POST done!");
			// StringBuilder sb = new StringBuilder();
            // foreach (System.Collections.Generic.KeyValuePair<string, string> dict in www.GetResponseHeaders())
            // {
            //     sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
            // }
			// if(www.downloadHandler.text == "Loggedin")
			// {
			// 	User user = JsonUtility.FromJson<User>(userData);
			// 	Debug.Log(user.id);
			// }
			// else
			// {
			// 	Debug.Log(www.downloadHandler.text); //convert the result to text
			// }

        }
    }


        // if (!Regex.IsMatch(password, PASSWORD_REGEX))
        // {
        //     alertText.text = "Invalid credentials";
        //     ActivateButtons(true);
        //     yield break;
        // }
        
        // WWWForm form = new WWWForm();
        // form.AddField("rUsername", username);
        // form.AddField("rPassword", password);
        // UnityWebRequest request = UnityWebRequest.Post("https://lunar-byte-371808.et.r.appspot.com/api/loginuser/", form);
        // // var handler = request.SendWebRequest();
        // yield return request.SendWebRequest();
        // float startTime = 0.0f;
        // while (!handler.isDone)
        // {
        //     startTime += Time.deltaTime;

        //     if (startTime > 10.0f)
        //     {
        //         break;
        //     }

        //     yield return null;
        // }
    //     Debug.Log(request.result);
    //     if (request.result == UnityWebRequest.Result.Success)
    //     {
    //         LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);

    //         if (response.code == 0) // login success?
    //         {
    //             ActivateButtons(false);
    //             alertText.text = "Welcome " + ((response.data.adminFlag == 1) ? " Admin" : "");
    //         }
    //         else
    //         {
    //             switch (response.code)
    //             {
    //                 case 1:
    //                     alertText.text = "Invalid credentials";
    //                     ActivateButtons(true);
    //                     break;
    //                 default:
    //                     alertText.text = "Corruption detected";
    //                     ActivateButtons(false);
    //                     break;
    //             }
    //         }
    //     }
    //     else
    //     {
    //         alertText.text = "Error connecting to the server...";
    //         ActivateButtons(true);
    //     }


    //     yield return null;
    // }

    // private IEnumerator TryCreate()
    // {
    //     string username = usernameInputField.text;
    //     string password = passwordInputField.text;

    //     if (username.Length < 3 || username.Length > 24)
    //     {
    //         alertText.text = "Invalid username";
    //         ActivateButtons(true);
    //         yield break;
    //     }

    //     if (!Regex.IsMatch(password, PASSWORD_REGEX))
    //     {
    //         alertText.text = "Invalid credentials";
    //         ActivateButtons(true);
    //         yield break;
    //     }

    //     WWWForm form = new WWWForm();
    //     form.AddField("rUsername", username);
    //     form.AddField("rPassword", password);

    //     UnityWebRequest request = UnityWebRequest.Post(createEndpoint, form);
    //     var handler = request.SendWebRequest();

    //     float startTime = 0.0f;
    //     while (!handler.isDone)
    //     {
    //         startTime += Time.deltaTime;

    //         if (startTime > 10.0f)
    //         {
    //             break;
    //         }

    //         yield return null;
    //     }

    //     if (request.result == UnityWebRequest.Result.Success)
    //     {
    //         Debug.Log(request.downloadHandler.text);
    //         CreateResponse response = JsonUtility.FromJson<CreateResponse>(request.downloadHandler.text);

    //         if (response.code == 0) 
    //         {
    //             alertText.text = "Account has been created!";
    //         }
    //         else
    //         {
    //             switch (response.code)
    //             {
    //                 case 1:
    //                     alertText.text = "Invalid credentials";
    //                     break;
    //                 case 2:
    //                     alertText.text = "Username is already taken";
    //                     break;
    //                 case 3:
    //                     alertText.text = "Password is unsafe";
    //                     break;
    //                 default:
    //                     alertText.text = "Corruption detected";
    //                     break;

    //             }
    //         }
    //     }
    //     else
    //     {
    //         alertText.text = "Error connecting to the server...";
    //     }

    //     ActivateButtons(true);

    //     yield return null;
    // }

    private void ActivateButtons(bool toggle)
    {
        loginButton.interactable = toggle;
        //createButton.interactable = toggle;
    }

   
}
