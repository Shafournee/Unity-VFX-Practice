using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    const int scorePoints = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            GameManager.instance.ModifyScore(scorePoints);
            GameManager.instance.SpawnNewTarget();
            Destroy(gameObject);
        }
    }
}
