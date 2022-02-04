using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public PlayerController player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckPoint();
    }
    private void CheckPoint()
    {
        GameManager.Instance.AddPoint(player.tag);
        GameManager.Instance.ResetGame(this.tag);
    }
}
