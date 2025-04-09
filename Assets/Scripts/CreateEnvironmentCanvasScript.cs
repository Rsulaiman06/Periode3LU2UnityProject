using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateEnvironmentCanvasScript : MonoBehaviour
{
    //TODO: 
    // - de namen van de environment buttons kloppen niet altijd met de environment die je aanmaakt. Dit komt omdat de buttons niet goed worden geupdate. Dit kan opgelost worden door de buttons opnieuw te updaten na het aanmaken van een environment.

    [Header("Panel")]
    public GameObject createEnvironmentPanel;
    public GameObject notAllFieldFillledErrorPanel;
    public GameObject inputExceededPanel;
    public GameObject nameCantBeTheSameErrorPanel;

    [Header("Buttons")]
    public GameObject environmentButtons;
    public GameObject environmentDeleteButtons;
    public GameObject createEnvironmentButton;

    [Header("InputFields")]
    public TMP_InputField nameInputField;
    public TMP_InputField lengthInputField;
    public TMP_InputField heightInputField;

    [Header("Scripts")]
    public ControlRoom controlRoom;

    [Header("Environment2D")]
    public string environmentIdString;
    public string environmentCreateErrorMessage;
    public List<Button> deleteButtons = new();

    [Header("Canvas")]
    public GameObject createEnvironmentCanvas;
    public GameObject GameRoomCanvas;

    [Header("text")]
    public TMP_Text NameErrorText;
    public TMP_Text LengthErrorText;
    public TMP_Text HeightErrorText;


    void Start()
    {
        nameInputField.onValueChanged.AddListener(MaxNameTextColorChange);
        lengthInputField.onValueChanged.AddListener(MaxLengthHeightColorChange);
        heightInputField.onValueChanged.AddListener(MaxHeightColorChange);
    }

    private void MaxNameTextColorChange(string nameInput)
    {
        if (nameInputField.text.Length < 25)
        {
            NameErrorText.color = Color.green;
        }
        else if (nameInputField.text.Length > 25)
        {
            NameErrorText.color = Color.red;
        }
    }

    private void MaxLengthHeightColorChange(string lengthInput)
    {
        if (Convert.ToInt32(lengthInputField.text) > 150)
        {
            LengthErrorText.color = Color.red;
        }
        else
        {
            LengthErrorText.color = Color.green;
        }
    }

    private void MaxHeightColorChange(string heightInput)
    {
        if (int.Parse(heightInputField.text) > 150)
        {
            HeightErrorText.color = Color.red;
        }
        else
        {
            HeightErrorText.color = Color.green;
        }
    }

    public void SetCreateEnvironmentPanelActive()
    {
        environmentCreateErrorMessage = string.Empty;
        createEnvironmentButton.SetActive(false);
        environmentButtons.SetActive(false);
        environmentDeleteButtons.SetActive(false);
        notAllFieldFillledErrorPanel.SetActive(false);
        inputExceededPanel.SetActive(false);
        nameCantBeTheSameErrorPanel.SetActive(false);
        createEnvironmentPanel.SetActive(true);
    }

    public void SetCreateEnvironmentPanelInactive()
    {
        if (string.IsNullOrEmpty(nameInputField.text) || string.IsNullOrEmpty(lengthInputField.text) || string.IsNullOrEmpty(heightInputField.text))
        {
            createEnvironmentButton.SetActive(false);
            environmentButtons.SetActive(false);
            environmentDeleteButtons.SetActive(false);
            createEnvironmentPanel.SetActive(false);
            inputExceededPanel.SetActive(false);
            nameCantBeTheSameErrorPanel.SetActive(false);
            notAllFieldFillledErrorPanel.SetActive(true);
        }
        else if (nameInputField.text.Length > 25 || int.Parse(lengthInputField.text) > 150 || int.Parse(heightInputField.text) > 150)
        {
            createEnvironmentButton.SetActive(false);
            environmentButtons.SetActive(false);
            environmentDeleteButtons.SetActive(false);
            createEnvironmentPanel.SetActive(false);
            notAllFieldFillledErrorPanel.SetActive(false);
            nameCantBeTheSameErrorPanel.SetActive(false);
            inputExceededPanel.SetActive(true);
        }
        else
        {
            createEnvironmentPanel.SetActive(false);
            notAllFieldFillledErrorPanel.SetActive(false);
            inputExceededPanel.SetActive(false);
            nameCantBeTheSameErrorPanel.SetActive(false);
            environmentButtons.SetActive(true);
            environmentDeleteButtons.SetActive(true);
            createEnvironmentButton.SetActive(true);
        }
    }

    public void GoToGameRoomCanvas()
    {
        createEnvironmentCanvas.SetActive(false);
        GameRoomCanvas.SetActive(true);
    }

    public EnvironmentModel GetEnvironmentCreateInputfields()
    {
        if(string.IsNullOrEmpty(nameInputField.text) || string.IsNullOrEmpty(lengthInputField.text) || string.IsNullOrEmpty(heightInputField.text))
        {
            Debug.Log("Please fill in all fields.");
            return null;
        }

        if (nameInputField.text.Length > 25)
        {
            return null;
        }
        else if (int.Parse(lengthInputField.text) > 150)
        {
            return null;
        }
        else if (int.Parse(heightInputField.text) > 150)
        {
            return null;
        }

            var environment = new EnvironmentModel
            {
                name = nameInputField.text,
                maxLength = int.Parse(lengthInputField.text),
                maxHeight = int.Parse(heightInputField.text)
            };

        return environment;
    }

    public void SetEnvironmentButtonsActive()
    {
        int index = 0; 

        foreach (Transform child in environmentButtons.transform)
        {
            Button button = child.GetComponent<Button>();
            button.onClick.AddListener(() => GoToGameRoomCanvas());
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

            if (string.IsNullOrWhiteSpace(buttonText.text))
            {
                button.gameObject.SetActive(false);
            }
            else
            {
                button.gameObject.SetActive(true);
                index++;
            }
        }

        foreach (Transform child in environmentDeleteButtons.transform)
        {
            Button deleteButton = child.GetComponent<Button>();
            deleteButtons.Add(deleteButton);
        }

        switch (index)
        {
            case 0:
                deleteButtons[0].gameObject.SetActive(false);
                deleteButtons[1].gameObject.SetActive(false);
                deleteButtons[2].gameObject.SetActive(false);
                deleteButtons[3].gameObject.SetActive(false);
                deleteButtons[4].gameObject.SetActive(false);
                break;
            case 1:
                deleteButtons[0].gameObject.SetActive(true);
                deleteButtons[1].gameObject.SetActive(false);
                deleteButtons[2].gameObject.SetActive(false);
                deleteButtons[3].gameObject.SetActive(false);
                deleteButtons[4].gameObject.SetActive(false);
                break;
            case 2:
                deleteButtons[0].gameObject.SetActive(true);
                deleteButtons[1].gameObject.SetActive(true);
                deleteButtons[2].gameObject.SetActive(false);
                deleteButtons[3].gameObject.SetActive(false);
                deleteButtons[4].gameObject.SetActive(false);
                break;
            case 3:
                deleteButtons[0].gameObject.SetActive(true);
                deleteButtons[1].gameObject.SetActive(true);
                deleteButtons[2].gameObject.SetActive(true);
                deleteButtons[3].gameObject.SetActive(false);
                deleteButtons[4].gameObject.SetActive(false);
                break;
            case 4:
                deleteButtons[0].gameObject.SetActive(true);
                deleteButtons[1].gameObject.SetActive(true);
                deleteButtons[2].gameObject.SetActive(true);
                deleteButtons[3].gameObject.SetActive(true);
                deleteButtons[4].gameObject.SetActive(false);
                break;
            case 5:
                deleteButtons[0].gameObject.SetActive(true);
                deleteButtons[1].gameObject.SetActive(true);
                deleteButtons[2].gameObject.SetActive(true);
                deleteButtons[3].gameObject.SetActive(true);
                deleteButtons[4].gameObject.SetActive(true);
                break;
        }
    }

    public void AddEnvironmentIdToObject2D_1() => environmentIdString = controlRoom.environmentId[0];
    public void AddEnvironmentIdToObject2D_2() => environmentIdString = controlRoom.environmentId[1];
    public void AddEnvironmentIdToObject2D_3() => environmentIdString = controlRoom.environmentId[2];
    public void AddEnvironmentIdToObject2D_4() => environmentIdString = controlRoom.environmentId[3];
    public void AddEnvironmentIdToObject2D_5() => environmentIdString = controlRoom.environmentId[4];


    public void DeleteEnvironmentButton1()
    {
        controlRoom.environment2D.id = controlRoom.environmentId[0];
        controlRoom.DeleteEnvironment2D();
    }
    public void DeleteEnvironmentButton2()
    {
        controlRoom.environment2D.id = controlRoom.environmentId[1];
        controlRoom.DeleteEnvironment2D();
    }
    public void DeleteEnvironmentButton3()
    {
        controlRoom.environment2D.id = controlRoom.environmentId[2];
        controlRoom.DeleteEnvironment2D();
    }
    public void DeleteEnvironmentButton4()
    {
        controlRoom.environment2D.id = controlRoom.environmentId[3];
        controlRoom.DeleteEnvironment2D();
    }
    public void DeleteEnvironmentButton5()
    {
        controlRoom.environment2D.id = controlRoom.environmentId[4];
        controlRoom.DeleteEnvironment2D();
    }
}