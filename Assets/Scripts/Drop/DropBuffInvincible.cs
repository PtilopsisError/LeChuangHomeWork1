using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBuffInvincible : DropBuff
{
    public int lifeTime = 500;
    public int affectTime = 250;

    public override void Affect(Collider other)
    {
        if(! other.transform.CompareTag("Player")){
            return;
        }//仅限玩家拾取

        gameObject.SetActive(false);

        TankBuff tankBuff = other.GetComponent<TankBuff>();
        if(! tankBuff){
            return;
        }
        tankBuff.AffectBuff(type, affectTime);
    }

    public override void Init()
    {
        //重置计时器
        lifeTimeCounter = lifeTime;
        gameObject.SetActive(true);
    }
}
