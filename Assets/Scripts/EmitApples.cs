using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitApples : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out var PlayerController))
        {
            GetComponent<ParticleSystem>().Emit(5);
        }
    }
}
