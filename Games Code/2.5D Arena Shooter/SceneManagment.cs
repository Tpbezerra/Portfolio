using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagment : MonoBehaviour {

    public InputField nameField;
    public Dropdown colorChanger;

    public void ChangeScene (string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    public void SetOverlay(GameObject overlayToSet)
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        foreach (Transform child in canvas)
        {
            if (child.gameObject == overlayToSet)
                child.gameObject.SetActive(true);
            else
                child.gameObject.SetActive(false);
        }
    }

    public void ChangeName()
    {
        PlayerPrefs.SetString("PlayerName", nameField.text);
    }

    public void SetColor()
    {
        PlayerPrefs.SetInt("PlayerColorIndex", colorChanger.value);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
