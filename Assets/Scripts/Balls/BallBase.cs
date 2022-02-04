using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBase : MonoBehaviour
{
    [Header("Movement")]
    public Vector3 move;
    public float speed = 200f;

    [Header("Tags")]
    public string limitTag;
    public string playerTag01;
    public string playerTag02;

    [Header("Range")]
    public Vector2 rangeX;
    public Vector2 rangeY;

    private bool _canMove = false;
    private Vector3 _startPosition;
    private Vector3 _startMove;

    private void MoveBall()
    {
        if (!_canMove) return;
        transform.Translate(-move * Time.deltaTime * speed);
    }

    public void SetFreeBall(bool state)
    {
        _canMove = state;
    }

    public void ResetBall(string tag)
    {
        transform.position = _startPosition;
        if (tag == playerTag01)
            move = -_startMove;
        else if (tag == playerTag02)
            move = _startMove;
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == limitTag)
            CollisionOnBarrier();
        if (collision.transform.tag == playerTag01 || collision.transform.tag == playerTag02)
            CollisionOnPlayer();
    }

    private void CollisionOnBarrier()
    {
        move.y *= -1;
    }

    private void CollisionOnPlayer()
    {
        move.x *= -1;
        float range = Random.Range(rangeX.x, rangeX.y);
        if (move.x < 0)
            move.x = -range;
        else if (move.x > range)
            move.x = range;

        range = Random.Range(rangeY.x, rangeY.y);
        move.y = range;
    }

    void Update()
    {
        MoveBall();
    }

    private void Awake()
    {
        _startPosition = transform.position;
        _startMove = move;
    }
}
