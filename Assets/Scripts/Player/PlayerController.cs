using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rbPlayer;
    public float speed = 10f;

    [Header("Buttons Controllers")]
    public KeyCode keyUp;
    public KeyCode keyDown;

    private bool _canMove = false;
    private Vector3 _startPosition;
    private Vector3 _currentPosition;
    private Image _imagePlayer;

    private void OnValidate()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        _imagePlayer = GetComponent<Image>();
    }

    private void MovePlayer()
    {
        if (!_canMove) return;

        if (Input.GetKey(keyUp))
            rbPlayer.MovePosition(transform.position + transform.up * speed * Time.deltaTime);

        else if(Input.GetKey(keyDown))
            rbPlayer.MovePosition(transform.position + transform.up * -speed * Time.deltaTime);
    }

    public void ChangeColor(Color c)
    {
        _imagePlayer.color = c;
    }

    public void SetFreePlayer(bool state)
    {
        _canMove = state;
    }

    public void ResetPosition()
    {
        transform.position = _startPosition;
    }

    void Update()
    {
        MovePlayer();
    }

    private void Awake()
    {
        _startPosition = transform.position;
    }
}
