using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static MyGameManager;

public class TankHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;          
    public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;    
    public GameObject m_ExplosionPrefab;

    private MyDropManager dropManager;
    private AudioSource m_ExplosionAudio;          
    private ParticleSystem m_ExplosionParticles;   
    private float m_CurrentHealth;  
    private bool invincible = false;
    private bool m_Dead;          


    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
        dropManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<MyDropManager>();
    }


    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }
   

    public void TakeDamage(float amount)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        if(! invincible){
            m_CurrentHealth -= amount;
        }
        SetHealthUI();
        if(!m_Dead && m_CurrentHealth <= 0){
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        m_Slider.value = m_CurrentHealth;

        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor,m_CurrentHealth / m_StartingHealth);
    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        m_Dead = true;
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();
        gameObject.SetActive(false);
        Board.GetInstance().activeEnemiesCount --;
        dropManager.DropRequest(new Vector3(transform.position.x, 1, transform.position.z));//申请掉落物
        // Debug.Log(gameObject.name);
    }

    public bool isDead(){
        return m_Dead;
    }
    // public void SelfDestroy(){
    //     Destroy(gameObject);
    // }

    public void SetInvincible(bool invincible){
        this.invincible = invincible;
    }
}