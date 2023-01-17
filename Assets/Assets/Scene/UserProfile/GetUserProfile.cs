using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUserProfile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        User user = GameObject.Find("UserManager").GetComponent<User>();
        Debug.Log(user.session_id);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
