using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

public class SetPointsToWin : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI textPointsToWin;

    private int _points;

    public void ChangePointsToWin()
    {
        string s = textPointsToWin.text;
        Regex r = new Regex("[^a-zA-z0-9 -]");
        s = r.Replace(s, "");
        _points = int.Parse(s);
        GameManager.Instance.SetPointsToWin(_points);
        Debug.Log(_points);
    }

}
