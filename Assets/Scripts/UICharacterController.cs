using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterController : MonoBehaviour
{
    [SerializeField] private ButtonController left;
    [SerializeField] private ButtonController right;
    [SerializeField] private Button atack;
    [SerializeField] private Button jump;
    public ButtonController Left => left;
    public ButtonController Right => right;
    public Button Atack => atack;
    public Button Jump => jump;

    void Start()
    {
        Player.instance.InitUIController(this);
    }

}
