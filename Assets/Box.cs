using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private int score = 5;
    public ParticleSystem explosionParticle;
    [SerializeField] private float hp = 10f;

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            ParticleSystem instance = Instantiate(explosionParticle, transform.position, transform.rotation);

            AudioSource explosionAudio = instance.GetComponent<AudioSource>();
            explosionAudio.Play();

            Destroy(instance.gameObject, instance.main.duration);
            gameObject.SetActive(false);
        }
    }
}
