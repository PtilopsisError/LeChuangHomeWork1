using System;
using System.Collections.Generic;
using UnityEngine;

public class TankBuff : MonoBehaviour
{
    private Dictionary<DropBuff.DropsType, BuffInf> buffDic;
    private class BuffInf{
        public int counter{get;set;}
        public GameObject resumeable;
        public BuffInf(GameObject resumeable){
            counter = 0;
            this.resumeable = resumeable;
        }
    }
    
    [Serializable]
    public struct TypeAndInterface{
        public DropBuff.DropsType type;
        public GameObject resumeable;
    }
    
    public List<TypeAndInterface> buffs;
    
    public void Start(){
        buffDic = new();
        foreach(TypeAndInterface i in buffs){
            buffDic.Add(i.type,new BuffInf(i.resumeable));
        }
    }

    public void FixedUpdate(){
        foreach(DropBuff.DropsType type in buffDic.Keys){
            if(buffDic[type].counter > 0){
                buffDic[type].counter --;
                if(buffDic[type].counter == 0){
                    IResumeable target = buffDic[type].resumeable.GetComponent<IResumeable>();
                    target.Resume();
                }
            }
        }
    }

    public void AffectBuff(DropBuff.DropsType type, int affectTime){
        if(!buffDic.ContainsKey(type)){
            //对象不可获取目标buff
            return;
        }

        IResumeable target = buffDic[type].resumeable.GetComponent<IResumeable>();
        // Debug.Log(target);
        target.Active();
        buffDic[type].counter = affectTime;
    }

}

public interface IResumeable{
    public void Resume();
    public void Active();
}
