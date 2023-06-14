using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public float TimeBetweenSpawn;
    public float StartTime;
    public int[] MaxEnemy;
    public int Wave;
    public int EnemiesAlaive;
    public int score;
    public Text ScoreText;
    public Text WaveText;

    void Start(){
        ScoreText.text = "SCORE: 0";
        WaveText.text = "WAVE: 1";
    }

    public void AddEnemy(){
        EnemiesAlaive += 1;
    }

    public void RemoveEnemy(){
        EnemiesAlaive -= 1;
        score += 1;
        ScoreText.text = "SCORE: " + score.ToString();

        if(EnemiesAlaive <= 0)
        {
            Wave += 1;
            WaveText.text = "WAVE: " + Wave.ToString();
        }
    }

    void Update()
    {
        if(EnemiesAlaive > 0)
        {
            return;
        }

        else
        {
            for (int i = 0; i < MaxEnemy[Wave]; i++)
            {
                Vector3 RandSpawnPlaces = new Vector3(Random.Range(-43, 44), 2.6f , Random.Range(40, -31));
                Instantiate(Enemy, RandSpawnPlaces, transform.rotation);
            }
        }
    }
}
