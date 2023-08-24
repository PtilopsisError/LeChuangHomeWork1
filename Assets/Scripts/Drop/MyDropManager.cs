using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDropManager : MonoBehaviour
{   
    private Dictionary<int, DropBuff.DropsType> drops;
    private int totalRight;
    private List<DropBuff> dropInstances;

    [Serializable]
    public struct dropAndRight{
        [Range(1,1000)]
        public int right;
        public DropBuff.DropsType type;
    }
    public List<dropAndRight> list;

    // Start is called before the first frame update
    void Start()
    {
        drops = new();
        dropInstances = new();
        int sum = 0;
        foreach(dropAndRight i in list){
            sum += i.right;
            drops.Add(sum, i.type);
        }
        totalRight = sum;
    }

    public void DropRequest(Vector3 position){
        DropBuff.DropsType dropsType = RandomDrop();
        if(dropsType == DropBuff.DropsType.NoDrop){
            return;
        }
        DropBuff newDropBuff = GetDropInstance(dropsType);
        newDropBuff.transform.position = position;
    }

    private DropBuff.DropsType RandomDrop(){
        int random = UnityEngine.Random.Range(0,totalRight);
        int index = 0;
        foreach(int i in drops.Keys){
            if(random < i){
                return list[index].type;
            }
            index ++;
            //Debug.Log(String.Format("{0},{1},{2}",random,totalRight,i));
        }
        return DropBuff.DropsType.NoDrop;
    }

    private DropBuff GetDropInstance(DropBuff.DropsType type){
        foreach(DropBuff i in dropInstances){
            if(i != null && ! i.gameObject.activeInHierarchy){
                if(i.type == type){
                    i.Init();
                    return i;
                }
            }
        }
        // Debug.Log(String.Format("DropBuff{0}", type));
        GameObject newDropBuff = Resources.Load(String.Format("DropBuff{0}", type)) as GameObject;
        newDropBuff = Instantiate(newDropBuff);
        dropInstances.Add(newDropBuff.GetComponent<DropBuff>());
        newDropBuff.GetComponent<DropBuff>().Init();
        return newDropBuff.GetComponent<DropBuff>();
    }
}
