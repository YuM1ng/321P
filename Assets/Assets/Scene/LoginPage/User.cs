using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class User
{
	public string Username;
	public string Password;

	public User(string Username, string Password)
	{
		this.Username = Username;
		this.Password = Password;
	}
}
