using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;
    [SerializeField] GameObject[] fruitPrefabs;

    [Header ("BombSettings")]
    [SerializeField] GameObject bombPrefab;
    [Range(0f, 1f)]
    [SerializeField] float bombChance = 0.05f;


    [Header ("SpawnDelay's")]
    [SerializeField] float minSpawnDelay = 0.25f;
    [SerializeField] float maxSpawnDelay = 1.0f;

    [Header ("R_Angles")]
    [SerializeField] float minAngle = -15f;
    [SerializeField] float maxAngle = 15f;

    [Header ("Force")]
    [SerializeField] float minForce = 18f;
    [SerializeField] float maxForce = 22f;

    [Header ("LifeTime")]
    [SerializeField] float maxLifeTime = 5f;

    private void Awake()
    { 
      spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

            if (Random.value < bombChance)
            {
                prefab = bombPrefab;
            }

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject fruit = Instantiate(prefab, position, rotation);
            Destroy( fruit, maxLifeTime);

            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
