using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetColorPlayer : MonoBehaviour
{
    [Header("References")]
    public PlayerController player;
    public Image colorButton;
    public Color colorChange;

    private void OnValidate()
    {
        colorButton = GetComponent<Image>();
    }

    private void Start()
    {
        colorButton.color = colorChange;
    }

    public void ChangeColorPlayer()
    {
        player.ChangeColor(colorChange);
    }
}
