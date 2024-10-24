#pragma warning disable IDE0063 // Use simple 'using' statement
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

    public void Login(string username, string password)
    {
        PlayerPrefs.SetString("username", username);
        WWWForm loginForm = CreateUserAuthForm(username, password);
        StartCoroutine(AuthPostCoroutine(loginForm, "login"));

    }

    public void Signup(string username, string password)
    {
        PlayerPrefs.SetString("username", username);
        WWWForm signupForm = CreateUserAuthForm(username, password);
        StartCoroutine(AuthPostCoroutine(signupForm, "register"));
    }

    public void Delete()
    {
        throw new NotImplementedException();
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

    private IEnumerator AuthPostCoroutine(WWWForm form, string endpoint)
    {

        using (UnityWebRequest www = UnityWebRequest.Post(url + endpoint, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                var errorMsg = GetErrorStatus(www);
                Debug.LogError("Error: " + errorMsg);
            }
            else
            {
                var sessionToken = GetSessionTokenFromDownloadHandler(www.downloadHandler.text);
                PlayerPrefs.SetString("sessionToken", sessionToken);

                // start game after player successfully logged in or signup
                GameObject.Find("Canvas").GetComponent<MainMenu>().LoadLevel();
            }
        }
    }

}
#pragma warning restore IDE0063 // Use simple 'using' statement
