using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reload : MonoBehaviour
{
    [SerializeField] private Image atackButton;
    private bool currentReload;
    void Update()
    {
        currentReload = Player.instance.ReadyReload;
        if (!currentReload)
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
