using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHaveUserAccount : MonoBehaviour
{
    private void Awake()
    {
        User user = GameObject.Find("UserManager").GetComponent<User>();
        if(user.session_id == "")
        {
            gameObject.SetActive(false);
        }
    }
}
