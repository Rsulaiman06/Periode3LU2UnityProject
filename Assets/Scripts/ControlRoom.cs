using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlRoom : MonoBehaviour
{
    [Header("Models")]
    public UserModel user;
    public EnvironmentModel environment2D;
    public ObjectModel object2D;

    [Header("ApiControllers")]
    public UserControllerConnection userApiClient;
    public EnvironmentControllerConnection enviroment2DApiClient;
    public ObjectControllerConnection object2DApiClient;

    [Header("Scripts")]
    public LoginRegisterInputFieldScript loginRegisterInputFieldScript;
    public CreateEnvironmentCanvasScript createEnvironmentCanvasScript;
    public Panels panelsScript;


    [Header("Canvas")]
    public GameObject loginRegisterCanvas;
    public GameObject environment2DCanvas;
    public GameObject object2DCanvas;

    [Header("Text")]
    public GameObject registerErrorText;
    public GameObject registerSuccesText;
    public List<TMP_Text> environmentButtonsText = new();
    public List<string> environmentId = new();


    #region Login

    [ContextMenu("User/Register")]
    public async void Register()
    {
        user = loginRegisterInputFieldScript.RegisterInputFieldData();
        IWebRequestReponse webRequestResponse = await userApiClient.Register(user);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log("Register succes!");
                registerErrorText.SetActive(false);
                registerSuccesText.SetActive(true);
                panelsScript.SetRegisterSuccessPanelActive();
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Register error: " + errorMessage);
                registerSuccesText.SetActive(false);
                registerErrorText.SetActive(true);
                panelsScript.SetRegisterErrorPanelActive();
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    [ContextMenu("User/Login")]
    public async void Login()
    {
        user = loginRegisterInputFieldScript.LoginInputFieldData();
        IWebRequestReponse webRequestResponse = await userApiClient.Login(user);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log("Login succes!");
                ReadEnvironment2Ds();
                loginRegisterCanvas.SetActive(false);
                environment2DCanvas.SetActive(true);
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Login error: " + errorMessage);
                panelsScript.SetLoginErrorPanelActive();
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    #endregion

    #region Environment

    [ContextMenu("Environment2D/Read all")]
    public async void ReadEnvironment2Ds()
    {
        IWebRequestReponse webRequestResponse = await enviroment2DApiClient.ReadEnvironment2Ds();
        int environmentsCount = 0;
        switch (webRequestResponse)
        {
            case WebRequestData<List<EnvironmentModel>> dataResponse:
                List<EnvironmentModel> environment2Ds = dataResponse.Data;
                Debug.Log("List of environment2Ds: ");
                environment2Ds.ForEach(environment2D => Debug.Log(environment2D.id));

                environmentsCount = 0;
                environmentId.Clear();
                for (int i = 0; i < environmentButtonsText.Count; i++)
                {
                    environment2Ds.Reverse();
                    if (i < environment2Ds.Count)
                    {
                        environmentButtonsText[i].text = environment2Ds[i].name;
                        environmentId.Add(environment2Ds[i].id);
                        environmentsCount++;
                    }
                    else
                    {
                        environmentButtonsText[i].text = string.Empty;
                    }
                }
                createEnvironmentCanvasScript.SetEnvironmentButtonsActive();

                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Read environment2Ds error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }

        if(environmentsCount == 5)
        {
            createEnvironmentCanvasScript.createEnvironmentButton.SetActive(false);
        }
        else
        {
            createEnvironmentCanvasScript.createEnvironmentButton.SetActive(true);
        }
    }

    [ContextMenu("Environment2D/Create")]
    public async void CreateEnvironment2D()
    {
        environment2D = createEnvironmentCanvasScript.GetEnvironmentCreateInputfields();
        IWebRequestReponse webRequestResponse = await enviroment2DApiClient.CreateEnvironment(environment2D);

        switch (webRequestResponse)
        {
            case WebRequestData<EnvironmentModel> dataResponse:
                environment2D.id = dataResponse.Data.id;
                ReadEnvironment2Ds();
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Create environment2D error: " + errorMessage);
                createEnvironmentCanvasScript.environmentCreateErrorMessage = errorMessage;
                createEnvironmentCanvasScript.createEnvironmentButton.SetActive(false);
                createEnvironmentCanvasScript.environmentButtons.SetActive(false);
                createEnvironmentCanvasScript.environmentDeleteButtons.SetActive(false);
                createEnvironmentCanvasScript.createEnvironmentPanel.SetActive(false);
                createEnvironmentCanvasScript.notAllFieldFillledErrorPanel.SetActive(false);
                createEnvironmentCanvasScript.inputExceededPanel.SetActive(false);
                createEnvironmentCanvasScript.nameCantBeTheSameErrorPanel.SetActive(true);
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    [ContextMenu("Environment2D/Delete")]
    public async void DeleteEnvironment2D()
    {
        IWebRequestReponse webRequestResponse = await enviroment2DApiClient.DeleteEnvironment(environment2D.id);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                string responseData = dataResponse.Data;
                ReadEnvironment2Ds();
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Delete environment error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    #endregion Environment

    #region Object2D

    [ContextMenu("Object2D/Read all")]
    public async void ReadObject2Ds()
    {
        IWebRequestReponse webRequestResponse = await object2DApiClient.ReadObject2Ds(object2D.environmentId);

        switch (webRequestResponse)
        {
            case WebRequestData<List<ObjectModel>> dataResponse:
                List<ObjectModel> object2Ds = dataResponse.Data;
                Debug.Log("List of object2Ds: " + object2Ds);
                object2Ds.ForEach(object2D => Debug.Log(object2D.id));
                // TODO: Succes scenario. Show the enviroments in the UI
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Read object2Ds error: " + errorMessage);
                // TODO: Error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    [ContextMenu("Object2D/Create")]
    public async void CreateObject2D()
    {
        IWebRequestReponse webRequestResponse = await object2DApiClient.CreateObject2D(object2D);

        switch (webRequestResponse)
        {
            case WebRequestData<ObjectModel> dataResponse:
                object2D.id = dataResponse.Data.id;
                // TODO: Handle succes scenario.
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Create Object2D error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    [ContextMenu("Object2D/Update")]
    public async void UpdateObject2D()
    {
        IWebRequestReponse webRequestResponse = await object2DApiClient.UpdateObject2D(object2D);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                string responseData = dataResponse.Data;
                // TODO: Handle succes scenario.
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Update object2D error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    #endregion

}
