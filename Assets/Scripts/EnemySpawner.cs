using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

    public GameObject enemyPrefab;
    public int numberOfEnemies;

    private float minSpawnPosition = -8.0f;
    private float maxSpawnPosition = 8.0f;

    private float minSpawnRotation = 0.0f;
    private float maxSpawnRotation = 180.0f;

    public override void OnStartServer() {
        for (int i = 0; i < numberOfEnemies; i++) {
            var spawnPosition = new Vector3(
                Random.Range(minSpawnPosition, maxSpawnPosition),    
                0.0f,    
                Random.Range(minSpawnPosition, maxSpawnPosition));
            
            var spawnRotation = Quaternion.Euler(
                0.0f,
                Random.Range(minSpawnRotation, maxSpawnRotation),
                0.0f);

            var enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }
    }

}
