using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] spawnPowerHealths;
    [SerializeField] private GameObject tanker;
    [SerializeField] private GameObject soldier;
    [SerializeField] private GameObject ranger;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject healthPowerUp;
    [SerializeField] private GameObject speedPowerUp;
    [SerializeField] private Text levelText;
    [SerializeField] private int maxPowerUps;
    [SerializeField] private Text endGameText;
    [SerializeField] private int finalLevel;

    private bool isGameOver;
    private int currentLevel;
    private float generatedSpawnTime = 1;
    private float currentSpawnTime;
    private float powerUpSpawnTime = 60;
    private float currentPowerUpSpawnTime;
    private int numOfPowerups;
    private GameObject newPowerUp;

    private GameObject newEnemy;
    private List<EnemyHealth> enemies = new List<EnemyHealth>();
    private List<EnemyHealth> killedEnemies = new List<EnemyHealth>();

    public bool IsGameOver { get { return isGameOver; } }
    public GameObject Player { get { return player; } }
    public GameObject Arrow { get { return arrow; } }

    public static GameManager instance = null;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
        else if(instance != this) {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);  //cause problems cause we switch between game scenes
    }

    private void Start () {
        endGameText.enabled = false;
        currentLevel = 1;
        StartCoroutine(SpawnEnemy());
        StartCoroutine(PowerUpSpawn());
    }

    private void Update () {
        currentSpawnTime += Time.deltaTime;
        currentPowerUpSpawnTime += Time.deltaTime;

    }

    public void PlayerHit(int currentHP) {
        isGameOver = currentHP > 0 ? false : true;
        if(isGameOver) {
            StartCoroutine(EndGame("Defeat"));
        }
    }

    public void RegisterEnemy(EnemyHealth enemy) {
        enemies.Add(enemy);
    }

    public void KilledEnemy(EnemyHealth enemy) {
        killedEnemies.Add(enemy);
    }

    private IEnumerator SpawnEnemy() {
        // Psuedo code:
        // check that spawn time is greater than the current 
        // if there are less enemies on screen than the current level, randomaly select a spawn point, and spawn
        // a random enemy
        // if we have killed the same number of enemies as the current level, clear out the enemies and killed
        // enemies arrays, increment the current level by 1, and start over

        if(currentSpawnTime > generatedSpawnTime) {
            currentSpawnTime = 0;
            if(currentLevel > enemies.Count) {
                int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
                GameObject spawnLocation = spawnPoints[randomSpawnIndex];
                int randomEnemyIndex = Random.Range(0, 3);
                switch(randomEnemyIndex) {
                    case 0:
                        newEnemy = Instantiate(soldier) as GameObject;
                        break;
                    case 1:
                        newEnemy = Instantiate(ranger) as GameObject;
                        break;
                    case 2:
                        newEnemy = Instantiate(tanker) as GameObject;
                        break;
                    default: break;
                }
                newEnemy.transform.position = spawnLocation.transform.position;
            }
            if(killedEnemies.Count == currentLevel && currentLevel != finalLevel) {
                enemies.Clear();
                killedEnemies.Clear();

                yield return new WaitForSeconds(3f);

                currentLevel++;
                levelText.text = "Level " + currentLevel;
            }

            if(killedEnemies.Count == finalLevel) {
                StartCoroutine(EndGame("Victory!"));
            }
        }
        yield return null;
        StartCoroutine(SpawnEnemy());
    }

    public void RegisterPowerUp() {
        numOfPowerups++;
    }

    public void UnregisterPowerUp() {
        numOfPowerups--;
    }

    private IEnumerator PowerUpSpawn() {
        if(currentPowerUpSpawnTime > powerUpSpawnTime) {
            currentPowerUpSpawnTime = 0;
            if(maxPowerUps > numOfPowerups) {
                int randomSpawnIndex = Random.Range(0, spawnPowerHealths.Length - 1);
                GameObject spawnLocation = spawnPowerHealths[randomSpawnIndex];
                int randomPowerUp = Random.Range(0, 2);

                if(randomPowerUp == 0) {
                    newPowerUp = Instantiate(healthPowerUp) as GameObject;
                }
                else if(randomPowerUp == 1) {
                    newPowerUp = Instantiate(speedPowerUp) as GameObject;
                }
                newPowerUp.transform.position = spawnLocation.transform.position;
            }
        }
        yield return null;
        StartCoroutine(PowerUpSpawn());
    }

    private IEnumerator EndGame(string outcome) {
        endGameText.text = outcome;
        endGameText.enabled = true;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("GameMenu");
    }
}
