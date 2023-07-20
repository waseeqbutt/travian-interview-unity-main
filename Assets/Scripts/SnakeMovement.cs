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
    private Vector2 _headLastPosition;
    private int _itemIndex = 0;

    private List<BodyList> _bodyList = new List<BodyList> ();

    private class BodyList
    {
        public Transform Body;
        public Vector2 NewPosition;
        public Vector2 LastPosition;

        public BodyList(Transform Body, Vector2 NewPosition, Vector2 LastPosition)
        {
            this.Body = Body;
            this.NewPosition = NewPosition;
            this.LastPosition = LastPosition;
        }
    }

    private void OnEnable()
    {
        BroadcastSystem.OnHitFoodEvent += OnHitFood;
        BroadcastSystem.OnClickChangeDirectionEvent += OnChangeDirection;
        BroadcastSystem.OnHitWallEvent += OnDamage;
        BroadcastSystem.OnHitEnemyEvent += OnDamage;
        BroadcastSystem.OnHitBodyEvent += OnDamage;
    }

    private void OnDisable()
    {
        BroadcastSystem.OnHitFoodEvent -= OnHitFood;
        BroadcastSystem.OnClickChangeDirectionEvent -= OnChangeDirection;
        BroadcastSystem.OnHitWallEvent -= OnDamage;
        BroadcastSystem.OnHitEnemyEvent -= OnDamage;
        BroadcastSystem.OnHitBodyEvent -= OnDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead) 
            return;

        if (Time.time > next)
        {
            snakeHead.transform.Translate(_snakeDirection * snakeSpeed);

            if(_bodyList.Count > 0)
            {
                for (int i = 0; i < _bodyList.Count; i++)
                {
                    if (i == 0)
                    {
                        _bodyList[i].LastPosition = _bodyList[i].Body.transform.position;
                        _bodyList[i].NewPosition = _headLastPosition;
                        _bodyList[i].Body.transform.position = _bodyList[i].NewPosition;
                    }
                    else
                    {

                        _bodyList[i].LastPosition = _bodyList[i].Body.transform.position;
                        _bodyList[i].NewPosition = _bodyList[i - 1].LastPosition;
                        _bodyList[i].Body.transform.position = _bodyList[i].NewPosition;
                    }
                }
            }

            _headLastPosition = snakeHead.transform.position;

            next = Time.time + 0.15f;
        }
    }

    private void OnHitFood(GameObject hitObject)
    {
        Debug.Log($"Got {hitObject.name}");
        GameObject body = Instantiate(snakeBody, transform.position, Quaternion.identity);

        Transform lastBody;

        if (_itemIndex == 0)
            lastBody = snakeHead.transform;
        else
            lastBody = _bodyList[_itemIndex - 1].Body.transform;


        Vector2 pos = body.transform.position;

        if (GetCurrentDirection() == Vector2.up)
        {
            pos.y = lastBody.transform.position.y - 0.25f;
        }
        else
        if (GetCurrentDirection() == Vector2.down)
        {
            pos.y = lastBody.transform.position.y + 0.25f;
        }
        else
        if (GetCurrentDirection() == Vector2.left)
        {
            pos.x = lastBody.transform.position.y + 0.25f;
        }
        else
        if (GetCurrentDirection() == Vector2.right)
        {
            pos.x = lastBody.transform.position.y - 0.25f;
        }

        body.transform.position = pos;
        _bodyList.Add(new BodyList(body.transform, body.transform.position, Vector2.zero));

        _itemIndex++;
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
