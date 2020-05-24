using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance { get; private set; }
    #endregion
    [SerializeField] private Toggle togleSound;
    [HideInInspector] public PlayerInventory inventory;
    public Dictionary<GameObject, Health> healthsContainer;
    public Dictionary<GameObject, Coin> coinContainer;
    public Dictionary<GameObject, BuffReciever> buffRecieverContainer;
    public Dictionary<GameObject, ItemComponent> itemsContainer;
    public ItemBase itemDataBase;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("togleSound"))
        {
           togleSound.isOn = intToBool(PlayerPrefs.GetInt("togleSound"));
        }
        instance = this;
        buffRecieverContainer = new Dictionary<GameObject, BuffReciever>();
        healthsContainer = new Dictionary<GameObject, Health>();
        coinContainer = new Dictionary<GameObject, Coin>();
        itemsContainer = new Dictionary<GameObject, ItemComponent>();
    }

    public void OnEditSound()
    {
        PlayerPrefs.SetInt("togleSound", boolToInt(togleSound.isOn));
    }

    public void OnClickPause()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void OnClickStartMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }

}
