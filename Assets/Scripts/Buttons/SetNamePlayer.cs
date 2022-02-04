using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetNamePlayer : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI namePlayer;

    private string nameP;

    public void ChangeNamePlayer()
    {
        nameP = namePlayer.text;
        GameManager.Instance.SetNamePlayer(this.tag, nameP);
    }
}
