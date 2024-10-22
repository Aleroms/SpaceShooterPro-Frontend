using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject loginPanel, signupPanel, loginSignupPanel;

    private void Start()
    {
        loginPanel = GameObject.Find("login_panel");
        signupPanel = GameObject.Find("signup_panel");
        loginSignupPanel = GameObject.Find("login_signup_panel");

        if (loginPanel != null && signupPanel != null && loginSignupPanel != null)
        {
            loginPanel.SetActive(false);
            signupPanel.SetActive(false);

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

        loginSignupPanel.SetActive(false);

        if (panelName == loginPanel.name)
            loginPanel.SetActive(true);
        else if (panelName == signupPanel.name)
            signupPanel.SetActive(true);




    }
    public void NavigateFromPanel(string panelName)
    {

        loginSignupPanel.SetActive(true);

        if (panelName == loginPanel.name)
            loginPanel.SetActive(false);
        else if (panelName == signupPanel.name)
            signupPanel.SetActive(false);



    }


}
