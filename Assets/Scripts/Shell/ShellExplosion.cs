using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 100f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;              


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Find all the tanks in an area around the shell and damage them.
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);
        foreach(Collider i in colliders){
            Rigidbody targetRigidbody = i.GetComponent<Rigidbody>();
            if(! targetRigidbody){continue;}
            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            TankHealth targetHealth = i.GetComponent<TankHealth>();
            if(!targetHealth){continue;}
            targetHealth.TakeDamage(CalculateDamage(targetRigidbody.position));
        }

        m_ExplosionParticles.transform.parent = null;
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        Destroy(m_ExplosionParticles.gameObject,m_ExplosionParticles.main.duration);
        Destroy(gameObject);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        Vector3 explosionToTarget = targetPosition - transform.position;
        float k = (m_ExplosionRadius - explosionToTarget.magnitude) / m_ExplosionRadius;
        return Mathf.Max(0f, k * m_MaxDamage);
    }
}