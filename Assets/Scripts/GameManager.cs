using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Text scoreText = default;
    [SerializeField] GameObject targetPrefab = default;

    int score;

    const float leftWall = -10.5f;
    const float rightWall = 10.5f;
    const float bottomWall = -4.4f;
    const float topWall = 4.4f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }


    public void ModifyScore(int scoreMod)
    {
        score += scoreMod;

        if (score < 0)
            score = 0;
    }

    public void SpawnNewTarget()
    {
        float SpawnPosX = Random.Range(leftWall, rightWall);
        float SpawnPosY = Random.Range(bottomWall, topWall);

        Instantiate(targetPrefab, new Vector3(SpawnPosX, SpawnPosY), Quaternion.identity);
    }
}
