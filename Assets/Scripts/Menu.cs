using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private InputField nameField;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Player_Name"))
        {
            nameField.text = PlayerPrefs.GetString("Player_Name");
        }
    }

    public void OnEndEditName()
    {
        PlayerPrefs.SetString("Player_Name", nameField.text);
    }

    public void OnClickPlay(int namberScene)
    {
        SceneManager.LoadScene(namberScene);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
