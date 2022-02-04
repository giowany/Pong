using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeToWait = 1f;

    [Header("References")]
    public BallBase ball;
    public List<PlayerController> player;
    public TextMeshProUGUI textPlayer01;
    public TextMeshProUGUI textPlayer02;
    public TextMeshProUGUI textName01;
    public TextMeshProUGUI textName02;
    public TextMeshProUGUI nameWinerText;
    public GameObject winerMenu;
    public GameObject mainMenu;

    public static GameManager Instance;

    private int _currentPoints01;
    private int _currentPoints02;
    private string _currentName01;
    private string _currentName02;
    private int _maximumPoints = 5;

    public void AddPoint(string tagPlayer)
    {
        if (tagPlayer == ball.playerTag01)
            _currentPoints01++;
        else if (tagPlayer == ball.playerTag02)
            _currentPoints02++;

        UpdateUIText(tagPlayer);

        if (_currentPoints01 >= _maximumPoints || _currentPoints02 >= _maximumPoints)
            StateEndGame();

    }

    private void UpdateUIText(string tag)
    {
        if (tag == ball.playerTag01)
            textPlayer01.text = _currentPoints01.ToString();
        else if (tag == ball.playerTag02)
            textPlayer02.text = _currentPoints02.ToString();
    }

    public void StartGame()
    {
        ball.SetFreeBall(true);
        foreach (PlayerController player in player)
        {
            player.SetFreePlayer(true);
        }
    }

    public void ButtonStart()
    {
        Invoke(nameof(StartGame), timeToWait);
    }

    public void ResetGamePosition()
    {
        ball.ResetBall(ball.playerTag01);
        foreach (PlayerController player in player)
        {
            player.ResetPosition();
        }
    }

    public void CaptiveAll()
    {
        ball.SetFreeBall(false);
        foreach (PlayerController player in player)
        {
            player.SetFreePlayer(false);
        }
    }

    public void RestartGame()
    {
        CaptiveAll();
        ResetGamePosition();
        _currentPoints01 = -1;
        _currentPoints02 = -1;
        AddPoint(ball.playerTag01);
        AddPoint(ball.playerTag02);
    }

    public void ResetGame(string tag)
    {
        if (winerMenu.activeSelf) return;

        CaptiveAll();
        ball.ResetBall(tag);
        foreach (PlayerController player in player)
        {
            player.ResetPosition();
        }
        Invoke(nameof(StartGame), timeToWait);
    }

    public void SavePoints()
    {
        PlayerPrefs.SetInt(ball.playerTag01, _currentPoints01);
        _currentPoints01 = PlayerPrefs.GetInt(ball.playerTag01);
        PlayerPrefs.SetInt(ball.playerTag02, _currentPoints02);
        _currentPoints02 = PlayerPrefs.GetInt(ball.playerTag02);

    }

    public void SetNamePlayer(string tag, string name)
    {
        if (tag == ball.playerTag01)
        {
            textName01.text = name;
            PlayerPrefs.SetString(tag, textName01.text);
            _currentName01 = PlayerPrefs.GetString(tag, "Player 1");
        }
        else if (tag == ball.playerTag02)
        {
            textName02.text = name;
            PlayerPrefs.SetString(tag, textName02.text);
            _currentName02 = PlayerPrefs.GetString(tag, "Player 2");
        }
    }

    public void ShowWiner()
    {
        if (_currentPoints01 > _currentPoints02)
            nameWinerText.text = _currentName01 + " " + "Pontos: " + _currentPoints01.ToString();
        else if(_currentPoints02 > _currentPoints01) 
            nameWinerText.text = _currentName02 + " " + "Pontos: " + _currentPoints02.ToString();

        winerMenu.SetActive(true);
    }

    public void SetPointsToWin(int points)
    {
        _maximumPoints = points;
    }

    public void EndGame()
    {
        CaptiveAll();
        ResetGamePosition();
        SavePoints();
        ShowWiner();
    }

    public void PouseGame()
    {
        if (mainMenu.activeSelf)
        {
            mainMenu.SetActive(false);
            ButtonStart();
        }

        else
        {
            mainMenu.SetActive(true);
            CaptiveAll();

        }
    }
    #region StateMachine
    public void StateStart()
    {
        StateMachine.Instance.StartGame();
    }

    public void StateMenu()
    {
        StateMachine.Instance.EnterMenu();
    }

    public void StateEndGame()
    {
        StateMachine.Instance.EndGame();
    }
    #endregion

    private void Awake()
    {
        Instance = this;
        _currentPoints01 = 0;
        _currentPoints02 = 0;
        _currentName01 = PlayerPrefs.GetString(ball.playerTag01, "Player 1");
        _currentName02 = PlayerPrefs.GetString(ball.playerTag02, "Player 2");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            StartGame();

        if (Input.GetKeyDown(KeyCode.Escape))
            StateMenu();
    }
}
