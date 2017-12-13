using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private int score = 5;
    public ParticleSystem explosionParticle;    //만약 이게 private이면 어떻게 가져오지? public이니까 손쉽게 에디터 상에서 드래그앤드롭해서 가져왔지만. [SerializeField]하면 되나?
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
