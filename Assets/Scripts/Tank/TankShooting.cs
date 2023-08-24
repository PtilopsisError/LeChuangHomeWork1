using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    public Rigidbody m_Shell;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;
    public int m_shootCD = 25;

    
    //private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    //private bool m_Fired;
    private bool m_charging;  
    private int cdCounter;               


    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
       // m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }
   

    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
        if(m_charging){
            if(m_CurrentLaunchForce >= m_MaxLaunchForce){
                EndCharging();
            }
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
            m_AimSlider.value = m_CurrentLaunchForce;
        }else{
            m_AimSlider.value = m_MinLaunchForce;
        }

    }
    public void FixedUpdate(){
        if(cdCounter > 0){cdCounter --;}
    }

    public void StartCharging(){
        if(! m_charging && cdCounter == 0){
        // m_Fired = false;
        m_charging = true;
        cdCounter = m_shootCD;
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_ShootingAudio.clip = m_ChargingClip;
        m_ShootingAudio.Play();
        }
    }

    public void EndCharging(){
        if(m_charging){
            Fire();
        }
    }

    private void Fire()
    {
        // Instantiate and launch the shell.
        //m_Fired = true;
        m_charging = false;

        Rigidbody shell = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation);
        shell.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();

        m_CurrentLaunchForce = m_MinLaunchForce;
    }
}