﻿#pragma warning disable IDE0063 // Use simple 'using' statement
using SpaceShooterPro.API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class BackendAPI : MonoBehaviour
{
    public static readonly string url = "http://127.0.0.1:5000/";
    public static BackendAPI Instance { get; private set; }

    private GameObject _canvas;


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _canvas = GameObject.Find("Canvas");
    }



    public void Login(string username, string password)
    {
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("password", password);

        WWWForm loginForm = CreateUserAuthForm(username, password);
        StartCoroutine(AuthPostCoroutine(loginForm, "login"));

    }

    public void Signup(string username, string password)
    {
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("password", password);

        WWWForm signupForm = CreateUserAuthForm(username, password);
        StartCoroutine(AuthPostCoroutine(signupForm, "register"));
    }

    public void Delete(string sessionToken)
    {
        StartCoroutine(DeleteUserCoroutine(sessionToken));
    }
    public void UpdateScore()
    {
        throw new NotImplementedException();
    }
    private WWWForm CreateUserAuthForm(string username, string password)
    {
        var form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        return form;
    }
    private string GetErrorStatus(UnityWebRequest www) =>
        JsonUtility.FromJson<ErrorResponseWrapper>(www.downloadHandler.text)
            .error.message;
    private string GetSessionTokenFromDownloadHandler(string jsonRes)
    {
        string sessionToken = "";
        try
        {
            AuthResponseWrapper responseWrapper = JsonUtility.FromJson<AuthResponseWrapper>(jsonRes);
            sessionToken = responseWrapper.response.session_token;
        }
        catch (Exception e)
        {
            Debug.LogError("JSON Parsing Error: " + e.Message);
        }
        return sessionToken;
    }

    private bool CheckIfNetworkError(UnityWebRequest www)
    {

        if (www.isNetworkError || www.isHttpError)
        {
            var errorMsg = GetErrorStatus(www);
            if (_canvas != null)
                _canvas.GetComponent<MainMenu>().DisplayNetworkError(errorMsg);

            return false;
        }
        return true;
    }

    private IEnumerator DeleteUserCoroutine(string sessionToken)
    {
        using (UnityWebRequest www = UnityWebRequest.Delete(url + "delete_user"))
        {
            www.SetRequestHeader("Authorization", sessionToken);
            yield return www.SendWebRequest();

            if(!CheckIfNetworkError(www))
            {
                //return user back to main menu
                PlayerPrefs.SetString("username", "");
                PlayerPrefs.SetString("password", "");
                PlayerPrefs.SetString("sessionToken", "");
                Debug.Log("successfully deleted user");
            }
        }
    }
    private IEnumerator AuthPostCoroutine(WWWForm form, string endpoint)
    {

        using (UnityWebRequest www = UnityWebRequest.Post(url + endpoint, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                var errorMsg = GetErrorStatus(www);
                if (_canvas != null)
                    _canvas.GetComponent<MainMenu>().DisplayNetworkError(errorMsg);
            }
            else
            {
                var sessionToken = GetSessionTokenFromDownloadHandler(www.downloadHandler.text);
                PlayerPrefs.SetString("sessionToken", sessionToken);
                Debug.Log($"session token is {sessionToken}");

                // start game after player successfully logged in or signup
                if (_canvas != null)
                    _canvas.GetComponent<MainMenu>().LoadLevel();
            }
        }
    }

}
#pragma warning restore IDE0063 // Use simple 'using' statement
