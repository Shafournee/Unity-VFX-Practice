using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitLeaves : MonoBehaviour
{
    [SerializeField] ParticleSystem leafParticles = default;
    [SerializeField] int minParticleCount = default;
    [SerializeField] int maxParticleCount = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerController>(out var PlayerController))
        {
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.velocity = playerRigidbody.velocity / 7f;

            int particleCount = Random.Range(minParticleCount, maxParticleCount);

            leafParticles.Emit(emitParams, particleCount);
        }
    }
}
