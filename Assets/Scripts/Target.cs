using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    const int scorePoints = 10;
    const float velocityIncrease = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            GameManager.instance.ModifyScore(scorePoints);
            GameManager.instance.SpawnNewTarget();
            player.GetComponent<Rigidbody2D>().velocity += player.GetComponent<Rigidbody2D>().velocity.normalized * velocityIncrease;
            Destroy(gameObject);
        }
    }
}
