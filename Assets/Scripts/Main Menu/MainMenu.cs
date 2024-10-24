using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private InputField[] signupInputFields, loginInputFields;
    private GameObject _loginPanel, _signupPanel, _loginSignupPanel;
    private BackendAPI _api;


    private void Start()
    {
        _loginPanel = GameObject.Find("login_panel");
        _signupPanel = GameObject.Find("signup_panel");
        _loginSignupPanel = GameObject.Find("login_signup_panel");
        _api = GameObject.Find("NetworkManager").GetComponent<BackendAPI>();

        if (_loginPanel != null && _signupPanel != null && _loginSignupPanel != null)
        {
            _loginPanel.SetActive(false);
            _signupPanel.SetActive(false);

        }
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
    /*
     Navigates from the main menu to either the Login or Signup Panel. 
     Also navigates back to main menu. Normally, I would have condensed
     these methods into one but OnClick does not support mult. params.
     */
    public void NavigateToPanel(string panelName)
    {

        _loginSignupPanel.SetActive(false);

        if (panelName == _loginPanel.name)
            _loginPanel.SetActive(true);
        else if (panelName == _signupPanel.name)
            _signupPanel.SetActive(true);

    }
    public void NavigateFromPanel(string panelName)
    {

        _loginSignupPanel.SetActive(true);

        if (panelName == _loginPanel.name)
            _loginPanel.SetActive(false);
        else if (panelName == _signupPanel.name)
            _signupPanel.SetActive(false);

    }

    public void SubmitLoginForm()
    {
        Debug.Log("submitting login form");
        var password = loginInputFields[0].text;
        var username = loginInputFields[1].text;

        if (_api != null)
        {
            _api.Login(username, password);
        }
        else
            Debug.LogError("api obj is null");


    }

    public void SubmitSignupForm()
    {
        Debug.Log("submitting signup form");
        var password = signupInputFields[0].text;
        var username = signupInputFields[1].text;

        if (_api != null)
        {
            _api.Signup(username,password);
        }

    }


}
