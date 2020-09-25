﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Login : MonoBehaviour
{
		
    public InputField userField;
	public InputField passwordField;
		
	public GameObject login;
	public GameObject interfacebase;
	public GameObject register;
		
	public Button submitButton;
	
	public void Start()
	{
		if(SaverScript.LoadUser() != null){
			UsersData data = SaverScript.LoadUser();
			userField.text = data.user;
			passwordField.text = data.pass;
			
			StartCoroutine(LoginEnum());
		}
	}
	
	public void CallLogin()
	{
		StartCoroutine(LoginEnum());
	}
	
	IEnumerator LoginEnum()
	{
		WWWForm form = new WWWForm();
		form.AddField("user", userField.text);
		form.AddField("password", passwordField.text);
		WWW www = new WWW("https://alpine207.000webhostapp.com/login.php", form);
		yield return www;
		if(www.text[0] == '0')
		{
			DBManager.username = userField.text;
			DBManager.firstname = www.text.Split('\t')[2];
			DBManager.name = www.text.Split('\t')[1];
			SaverScript.SaveUser(userField.text, passwordField.text);
			interfacebase.SetActive(true);
			login.SetActive(false);
		}
		else
		{
			Debug.Log("User login failed. Error 0" + www.text);
		}
	}
	
	public void VerifyInputs()
	{
		submitButton.interactable = (userField.text.Length >= 4 && passwordField.text.Length >= 8 );
	}
	
	public void Register()
	{
		login.SetActive(false);
		register.SetActive(true);
	}
}
