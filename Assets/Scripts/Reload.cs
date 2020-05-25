using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reload : MonoBehaviour
{
    [SerializeField] private Image atackButton;
    void Update()
    {
        if (Player.instance.IsRelaoding)
        {
            atackButton.fillAmount = Player.instance.DeltaReload;
        }
        else
        {
            atackButton.fillAmount = 1.0f;
            Player.instance.DeltaReload = 0f;
        }
    }
}
