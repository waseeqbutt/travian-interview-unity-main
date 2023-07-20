using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.TriggerTag.Wall.ToString()))
        {
            BroadcastSystem.OnHitWallEvent?.Invoke(Constants.TriggerTag.Wall.ToString());
        }
        else
        if (collision.CompareTag(Constants.TriggerTag.Food.ToString()))
        {
            BroadcastSystem.OnHitFoodEvent?.Invoke(collision.gameObject);
        }
        else
        if (collision.CompareTag(Constants.TriggerTag.Enemy.ToString()))
        {
            BroadcastSystem.OnHitEnemyEvent?.Invoke(collision.gameObject.name);
        }

        else
        if (collision.CompareTag(Constants.TriggerTag.Body.ToString()))
        {
            BroadcastSystem.OnHitBodyEvent?.Invoke(collision.gameObject.name);
        }
    }
}
