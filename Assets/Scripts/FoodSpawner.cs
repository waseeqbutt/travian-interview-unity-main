using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject foodPrefab;

    [SerializeField]
    private Vector2 spawnOffset;

    private void OnEnable()
    {
        BroadcastSystem.OnHitFoodEvent += OnEatenFood;
    }

    private void Start()
    {
        SpawnFood();
    }

    private void OnDisable()
    {
        BroadcastSystem.OnHitFoodEvent -= OnEatenFood;
    }

    private void OnEatenFood(GameObject eatenFood)
    {
        Destroy(eatenFood);
        SpawnFood();
    }

    private void SpawnFood()
    {
        float horizontalPosition = Random.Range(Bounds.Instance.LeftBound.position.x + spawnOffset.x, 
            Bounds.Instance.RightBound.position.x - spawnOffset.x);
        float verticalPosition = Random.Range(Bounds.Instance.LowerBound.position.y + spawnOffset.y, 
            Bounds.Instance.UpperBound.position.y - spawnOffset.y);

        Instantiate(foodPrefab.gameObject, new Vector2(horizontalPosition, verticalPosition), Quaternion.identity);
    }
}
