using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCrash : MonoBehaviour
{   
    public float crashDamage = 5f;
    [Range(0,1)]
    public float selfDamageRate = 0.2f;
    void OnTriggerEnter(Collider other){
        //Debug.Log(other.name);
        Vector3 thisVelocity = GetComponent<Rigidbody>().velocity;
        if(thisVelocity != Vector3.zero){
            TankHealth otherTH = other.GetComponent<TankHealth>();
            if(otherTH != null){
                Vector3 thisToOther = other.transform.position - transform.position;
                float angle = Vector3.Angle(thisToOther, thisVelocity);
                if(angle < 90){
                    otherTH.TakeDamage(crashDamage);
                } 
            }
            GetComponent<TankHealth>().TakeDamage(crashDamage * selfDamageRate);
        }
    }
}
 