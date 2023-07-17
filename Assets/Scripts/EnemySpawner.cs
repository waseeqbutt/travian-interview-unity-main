using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Vector2 spawnOffset;

    private float _spawnDelay = 5f;
    private GameObject _spawnedEnemy = null;
    private WaitForSeconds _waitForSpawn;
    private Coroutine _spawnCoroutine;

    private void OnEnable()
    {
        _waitForSpawn = new WaitForSeconds(_spawnDelay);
        BroadcastSystem.OnHitWallEvent += IsGameOver;
        BroadcastSystem.OnHitEnemyEvent += IsGameOver;
    }

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawningEnemyRoutine());
    }

    private void OnDisable()
    {
        BroadcastSystem.OnHitWallEvent -= IsGameOver;
        BroadcastSystem.OnHitEnemyEvent -= IsGameOver;
    }

    private void IsGameOver(string hit)
    {
        StopCoroutine(_spawnCoroutine);
    }

    private void SpawnEnemy()
    {
        float horizontalPosition = Random.Range(Bounds.Instance.LeftBound.position.x + spawnOffset.x,
            Bounds.Instance.RightBound.position.x - spawnOffset.x);
        float verticalPosition = Random.Range(Bounds.Instance.LowerBound.position.y + spawnOffset.y,
            Bounds.Instance.UpperBound.position.y - spawnOffset.y);

        _spawnedEnemy = Instantiate(enemyPrefab.gameObject, new Vector2(horizontalPosition, verticalPosition), Quaternion.identity);
    }

    private IEnumerator SpawningEnemyRoutine()
    {
        while(true)
        {
            yield return _waitForSpawn;

            if(_spawnedEnemy != null)
                Destroy(_spawnedEnemy);

            SpawnEnemy();
        }
    }
}
