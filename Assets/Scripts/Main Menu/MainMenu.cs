using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private InputField[] signupInputFields, loginInputFields;
    private GameObject _loginPanel, _signupPanel, _loginSignupPanel,
        _networkError;
    private Text _networkErrorMessage;
    private BackendAPI _api;


    private void Start()
    {
        _loginPanel = GameObject.Find("login_panel");
        _signupPanel = GameObject.Find("signup_panel");
        _loginSignupPanel = GameObject.Find("login_signup_panel");
        _api = GameObject.Find("NetworkManager").GetComponent<BackendAPI>();
        _networkError = GameObject.FindGameObjectWithTag("NetworkError");
        _networkErrorMessage = GameObject.Find("errorMessage").GetComponent<Text>();

        if (_loginPanel != null && _signupPanel != null && _loginSignupPanel != null)
        {
            _loginPanel.SetActive(false);
            _signupPanel.SetActive(false);

        }

        if(_networkError != null)
        {
            _networkError.SetActive(false);
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
        var password = loginInputFields[0].text;
        var username = loginInputFields[1].text;

        // Encrypt password before sending to API
        var encrypt = new Encryption();
        var encryptedPassword = encrypt.Encrypt(password);

        if (_api != null)
        {
            _api.Login(username, encryptedPassword);
        }
        else
            Debug.LogError("api obj is null");


    }

    public void SubmitSignupForm()
    {
        Debug.Log("submitting signup form");
        var password = signupInputFields[0].text;
        var username = signupInputFields[1].text;

        // Encrypt password before sending to API
        var encrypt = new Encryption();
        var encryptedPassword = encrypt.Encrypt(password);

        if (_api != null)
        {
            _api.Signup(username, encryptedPassword);
        }

    }
    public void DeletePlayer()
    {
        var sessionToken = PlayerPrefs.GetString("sessionToken");
        if (sessionToken != null && _api != null)
        {
            Debug.Log($"session token is {sessionToken}");
            _api.Delete(sessionToken);
        }
        else
            Debug.LogError("sessionToken or backendAPI is null");
        // return to main menu
        LoadMainMenu();
    }
    public void DisplayNetworkError(string message)
    {
        StartCoroutine(NetworkErrorCoroutine(message));
    }
    private IEnumerator NetworkErrorCoroutine(string message)
    {
        if(_networkError != null && _networkErrorMessage != null)
        {
            _networkError.SetActive(true);
            _networkErrorMessage.text = message;
            yield return new WaitForSeconds(5f);
            _networkError.SetActive(false);
        }else
            Debug.LogError(message);
    }

}
