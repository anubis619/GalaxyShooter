using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;

    private GameManager game_manager_start;
    

    // Start is called before the first frame update
    void Start()
    {
        game_manager_start = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(SpawnEnemy());
        StartCoroutine(powerUpSpawn());
        

    }

    
    public void StartSpawnRoutines()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(powerUpSpawn());
    }

    // create a coroutine to spawn and enemy every 5 seconds

    
   
    
    public IEnumerator SpawnEnemy()
    {

        

        while (game_manager_start.GameIsOn == true)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-8f, 8f), 7, 0), Quaternion.identity);
            
            yield return new WaitForSeconds(5);
            
        }
        

    }

    public IEnumerator powerUpSpawn()
    {
        while (game_manager_start.GameIsOn == true)
        {
        int randomPowerUp = Random.Range(0, 3);
        Instantiate(powerups[randomPowerUp], new Vector3(Random.Range(-8f, 8f), 7, 0), Quaternion.identity);
        yield return new WaitForSeconds(7);
        }
    }
}
