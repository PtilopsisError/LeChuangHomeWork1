using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBuffFrezze : DropBuff
{
    public int lifeTime = 500;
    public int affectTime = 250;
    public float affectRadius = 20f;
    public LayerMask tankLayer;
    public override void Affect(Collider other)
    {
        if(! other.transform.CompareTag("Player")){
            return;
        }//仅限玩家拾取
        
        gameObject.SetActive(false);

        Collider[] colliders = Physics.OverlapSphere(transform.position, affectRadius, tankLayer);
        foreach(Collider i in colliders){
            TankBuff tankBuff = i.GetComponent<TankBuff>();
            if(! tankBuff){
                continue;
            }
            tankBuff.AffectBuff(type, affectTime);
        }
    }

    public override void Init()
    {
        //重置计时器
        lifeTimeCounter = lifeTime;
        gameObject.SetActive(true);
    }
}
