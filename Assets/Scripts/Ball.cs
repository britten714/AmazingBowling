using UnityEngine;

public class Ball : MonoBehaviour
{
    public LayerMask whatIsBox;


    public ParticleSystem explosionParticle;                //how to if private?
    public AudioSource explosionAudio;
    [SerializeField] private float maxDamage = 100f;
    [SerializeField] private float explosionForce = 100f;
    private float lifeTIme = 10f;
    [SerializeField] private float explosionRadius = 20f;

    void Start()
    {       
        Destroy(gameObject, lifeTIme);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, whatIsBox);

        foreach (var collider in colliders)
        {
            Rigidbody targetRigidbody = collider.GetComponent<Rigidbody>();
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            Box targetBox = collider.GetComponent<Box>();
            float damage = CalculateDamage(collider.transform.position);
            targetBox.TakeDamage(damage);


        }

        explosionParticle.transform.parent = null;      //이런 문제를 해결할 때 쓸 수 있는 좋은 방법인듯. 
        explosionParticle.Play();
        explosionAudio.Play();
        Destroy(gameObject);
        Destroy(explosionParticle.gameObject, explosionParticle.main.duration);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;
        float distance = explosionToTarget.magnitude;
        float edgeToCenterDistance = explosionRadius - distance;
        float percentage = edgeToCenterDistance / explosionRadius;
        float damage = maxDamage * percentage;
        damage = Mathf.Max(0, damage);
        return damage;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
