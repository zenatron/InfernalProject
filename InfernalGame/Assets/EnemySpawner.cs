using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject skeletonEnemyPrefab;
    private Transform spanwerTransform;
    public TMP_Text timerText;
    public float countdownTime = 300f;
    public float spawnInterval = 30f;

    private float currentTime;
    private bool canSpawn = true;

    void Start()
    {
        currentTime = countdownTime;
        UpdateTimerUI();
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval); // Start spawning
        spanwerTransform = GetComponentInParent<Transform>();
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            currentTime = Mathf.Max(currentTime, 0); // Prevent negative time
            UpdateTimerUI();

            if (currentTime <= 0)
            {
                StopSpawning(); // Stop spawning when the timer ends
                SceneManager.LoadSceneAsync("Win Screen");
            }
        }
    }

    void SpawnEnemy()
    {
        if (canSpawn && skeletonEnemyPrefab != null)
        {
            Instantiate(skeletonEnemyPrefab, spanwerTransform.position, spanwerTransform.rotation);
        }
    }

    void StopSpawning()
    {
        canSpawn = false;
        CancelInvoke(nameof(SpawnEnemy)); // Stop scheduled spawns
        Debug.Log("Spawning stopped as timer has ended.");
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}