using UnityEditor.Build.Content;
using UnityEngine;

public class Panels : MonoBehaviour
{
    [Header("Panels")]
    public GameObject StartPanel;
    public GameObject LoginPanel;
    public GameObject registerPanel;
    public GameObject errorPanelForLogin;
    public GameObject succesErrorPanelForRegister;
    //public GameObject createEnvironmentPanel;

    [Header("Canvas")]
    public GameObject loginRegisterCanvas;
    public GameObject createEnvironmentCanvas;
    public GameObject GameRoomCanvas;

    public void GobackToCreateEnvironmentRoom()
    {
        loginRegisterCanvas.SetActive(false);
        GameRoomCanvas.SetActive(false);
        createEnvironmentCanvas.SetActive(true);
    }

    public void GoToLoginPanel()
    {
        StartPanel.SetActive(false);
        registerPanel.SetActive(false);
        errorPanelForLogin.SetActive(false);
        succesErrorPanelForRegister.SetActive(false);
        LoginPanel.SetActive(true);
    }

    public void GoToRegisterPanel()
    {
        StartPanel.SetActive(false);
        LoginPanel.SetActive(false);
        errorPanelForLogin.SetActive(false);
        succesErrorPanelForRegister.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void GoToStartPanel()
    {
        LoginPanel.SetActive(false);
        registerPanel.SetActive(false);
        errorPanelForLogin.SetActive(false);
        succesErrorPanelForRegister.SetActive(false);
        StartPanel.SetActive(true);
    }
}
