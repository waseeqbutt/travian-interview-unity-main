using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementDirection
{
    Up,
    Down,
    Left,
    Right
}

public class SnakeMovement : MonoBehaviour
{
    [SerializeField]
    private float snakeSpeed = 0.5f;

    [SerializeField]
    private GameObject snakeHead;

    [SerializeField]
    private GameObject snakeBody;

    private Vector2 _snakeDirection = Vector2.up;
    private float next;
    private bool _isDead = false;
    private Vector2 _lastPosition;

    private List<Transform> _bodies = new List<Transform> ();

    private void OnEnable()
    {
        BroadcastSystem.OnHitFoodEvent += OnHitFood;
        BroadcastSystem.OnClickChangeDirectionEvent += OnChangeDirection;
        BroadcastSystem.OnHitWallEvent += OnDamage;
        BroadcastSystem.OnHitEnemyEvent += OnDamage;
    }

    private void OnDisable()
    {
        BroadcastSystem.OnHitFoodEvent -= OnHitFood;
        BroadcastSystem.OnClickChangeDirectionEvent -= OnChangeDirection;
        BroadcastSystem.OnHitWallEvent -= OnDamage;
        BroadcastSystem.OnHitEnemyEvent -= OnDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead) 
            return;

        if(Time.time > next)
        {
            snakeHead.transform.Translate(_snakeDirection * snakeSpeed);

            if(_bodies.Count > 0)
            {
                for(int i = 0; i < _bodies.Count; i++)
                {
                    _bodies[i].transform.Translate(_snakeDirection * snakeSpeed);
                }
            }
            else
            {
                _lastPosition = snakeHead.transform.position;
            }

            next = Time.time + 0.15f;
        }
    }

    private void OnHitFood(GameObject hitObject)
    {
        Debug.Log($"Got {hitObject.name}");
        GameObject body = Instantiate(snakeBody, transform.position, Quaternion.identity);
        _bodies.Insert(0, body.transform);
        
        if(GetCurrentDirection() == Vector2.up)
        {

        }
        else
        if (GetCurrentDirection() == Vector2.down)
        {

        }
        else
        if (GetCurrentDirection() == Vector2.left)
        {

        }
        else
        if (GetCurrentDirection() == Vector2.up)
        {

        }
    }    

    private void OnDamage(string hitObject)
    {
        _isDead = true;
        Debug.Log($"Hit by a {hitObject}");
    }

    private Vector2 GetCurrentDirection()
    {
        return _snakeDirection;
    }

    private void OnChangeDirection(MovementDirection direction)
    {
        switch(direction)
        {
            case MovementDirection.Up:
                _snakeDirection = Vector2.up;
                break;
            case MovementDirection.Down:
                _snakeDirection = Vector2.down;
                break;
            case MovementDirection.Left:
                _snakeDirection = Vector2.left;
                break;
            case MovementDirection.Right:
                _snakeDirection = Vector2.right;
                break;
        }
    }
}
