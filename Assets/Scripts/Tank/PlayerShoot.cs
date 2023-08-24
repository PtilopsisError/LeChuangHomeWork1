using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public TankShooting ts;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            ts.StartCharging();
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            ts.EndCharging();
        }
    }
}
