using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincible : MonoBehaviour, IResumeable
{
    public TankHealth tankHealth;
    public void Active()
    {
        tankHealth.SetInvincible(true);
    }

    public void Resume()
    {
        tankHealth.SetInvincible(false);
    }

}
