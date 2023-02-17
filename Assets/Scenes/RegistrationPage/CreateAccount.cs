using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;


public class CreateAccount : MonoBehaviour
{
    private const string PASSWORD_REGEX = "(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,24})";

    [SerializeField] private string registerEndpoint = "https://lunar-byte-371808.et.r.appspot.com/api/insertUser";

    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private Button createButton;
    [SerializeField] private TMP_InputField EmailInputField;
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField confirmpasswordInputField;
    
    public void OnCreateClick()
    {
        
        ActivateButtons(false);

        StartCoroutine(TryCreate());
    }

   private IEnumerator TryCreate()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        string cpassword = confirmpasswordInputField.text;
        string email = EmailInputField.text;
        Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");        
        
        if (username == "" || password == "" || cpassword == "" || email== "" )
        {
            alertText.text = "Please Input your Username, Password and Email";
            ActivateButtons(true);
            yield break;
        }
        if (emailRegex.IsMatch(email))
        {
            alertText.text = "Please enter your email in the correct format";
            yield break;
        }
        if (password.Length <3 || password.Length > 15 )
        {
            alertText.text = "Invalid password length";
            ActivateButtons(true);
            yield break;
        }

        if(password != cpassword)
        {
            alertText.text = "Your password is not the same as repeat password";
            ActivateButtons(true);
            yield break;
        }
    
        
        // if (password.Length <3 || password.Length > 15 )
        // {
        //     alertText.text = "Invalid password length";
        //     ActivateButtons(true);
        //     yield break;
        // }

        // if(password != cpassword)
        // {
        //     alertText.text = "Your password is not the same as repeat password";
        //     ActivateButtons(true);
        //     yield break;
        // }

        
        WWWForm form = new WWWForm();
        form.AddField("rUsername", username);
        form.AddField("rPassword", password);
        form.AddField("rEmail", email);


        UnityWebRequest www = UnityWebRequest.Post(registerEndpoint, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(form);
            Debug.Log("Form upload complete!");
            
        }
    } 

    private void ActivateButtons(bool toggle)
    {
        
        createButton.interactable = toggle;
    }

   public void BackToMainMenu(int sceneID)

    {
        SceneManager.LoadScene("LoginPage");
    }

}
