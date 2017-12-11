using UnityEngine;

public class Ball : MonoBehaviour
{

    private ParticleSystem explosionParticle;
    private AudioSource explosionAudio;
    private float maxDamage = 100f;
    private float explosionForce = 100f;
    private float lifeTIme = 10f;
    private float explosionRadius = 20f;

    void Start()
    {
        explosionParticle = GetComponentInChildren<ParticleSystem>();
        explosionAudio = GetComponentInChildren<AudioSource>();
        Destroy(gameObject, lifeTIme);
    }

    private void OnTriggerEnter(Collider other)
    {
        explosionParticle.transform.parent = null;
        explosionParticle.Play();
        explosionAudio.Play();
        Destroy(gameObject);
        Destroy(explosionParticle.gameObject, explosionParticle.duration);
    }
}
