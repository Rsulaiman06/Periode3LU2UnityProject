using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;

public class Panels : MonoBehaviour
{
    [Header("Panels")]
    public GameObject StartPanel;
    public GameObject LoginPanel;
    public GameObject registerPanel;
    public GameObject errorPanelForLogin;
    public GameObject succesPanelForRegister;
    public GameObject errorPanelForRegister;

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
        errorPanelForRegister.SetActive(false);
        succesPanelForRegister.SetActive(false);
        LoginPanel.SetActive(true);
    }

    public void GoToRegisterPanel()
    {
        StartPanel.SetActive(false);
        LoginPanel.SetActive(false);
        errorPanelForLogin.SetActive(false);
        errorPanelForRegister.SetActive(false);
        succesPanelForRegister.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void GoToStartPanel()
    {
        LoginPanel.SetActive(false);
        registerPanel.SetActive(false);
        errorPanelForLogin.SetActive(false);
        errorPanelForRegister.SetActive(false);
        succesPanelForRegister.SetActive(false);
        StartPanel.SetActive(true);
    }

    public void SetLoginErrorPanelActive()
    {
        StartPanel.SetActive(false);
        LoginPanel.SetActive(false);
        registerPanel.SetActive(false);
        errorPanelForRegister.SetActive(false);
        succesPanelForRegister.SetActive(false);
        errorPanelForLogin.SetActive(true);
    }

    public void SetRegisterSuccessPanelActive()
    {
        StartPanel.SetActive(false);
        LoginPanel.SetActive(false);
        registerPanel.SetActive(false);
        errorPanelForLogin.SetActive(false);
        errorPanelForRegister.SetActive(false);
        succesPanelForRegister.SetActive(true);
    }
    public void SetRegisterErrorPanelActive()
    {
        StartPanel.SetActive(false);
        LoginPanel.SetActive(false);
        registerPanel.SetActive(false);
        errorPanelForLogin.SetActive(false);
        succesPanelForRegister.SetActive(false);
        errorPanelForRegister.SetActive(true);
    }
}
