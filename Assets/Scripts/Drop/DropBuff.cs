using System;
using UnityEngine;

abstract public class DropBuff : MonoBehaviour{
    public enum DropsType{
        Frezze,
        Invincible,
        NoDrop
    }
    
    protected int lifeTimeCounter;
    
    public DropsType type;

    public void FixedUpdate(){
        // Debug.Log(String.Format("{0}{1}",type,lifeTimeCounter));
        if(-- lifeTimeCounter == 0){
            // Debug.Log(String.Format("{0}{1}",type,lifeTimeCounter));
            gameObject.SetActive(false);
        }
        // Debug.Log(String.Format("{0}{1}",type,lifeTimeCounter));
    }

    public void OnTriggerEnter(Collider other){
        Affect(other);
    }

    abstract public void Init();
    abstract public void Affect(Collider other);
}