using System.Collections;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour {
    [Inject] private ObjectPooler _objectPooler;
    private bool _canSpawn = true;
    [SerializeField] private float _spawnDelay;

    private void Start() {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner() {
        float delay = _spawnDelay;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (_canSpawn) {
            yield return wait;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy() {
        _objectPooler.SpawnFromPool(
            TagsHolder.enemy,
            transform.position,
            Quaternion.identity);
    }
}
