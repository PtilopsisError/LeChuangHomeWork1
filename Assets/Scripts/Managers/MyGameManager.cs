using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    private int currentWave;
    private int maxWave;
    private int spawnCounter;
    //private TankHealth playerTH;

    private class TankFactory{
        private struct Tank{
            public EnemiesType type;
            public GameObject tankObject;
            public Tank(EnemiesType type,GameObject tankObject){
                this.type = type;
                this.tankObject = tankObject;
            }
        }

        private static TankFactory instance;
        private List<Tank> tanks;
        private TankFactory(){
            tanks = new List<Tank>();
        }

        public static GameObject GetTank(EnemiesType type,Transform spawnPoint){
            if(instance == null){
                instance = new();
            }
            // if(instance.tanks == null){
            //     instance.tanks = new();
            // }
            //Debug.Log(instance.tanks.Count);
            foreach(Tank i in instance.tanks){
                if(i.tankObject != null &&! i.tankObject.activeInHierarchy){
                    if(i.type == type){
                        i.tankObject.SetActive(true);
                        i.tankObject.transform.position = spawnPoint.position;
                        i.tankObject.transform.rotation = spawnPoint.rotation;
                        return i.tankObject;
                    }
                }
            }
            GameObject newTank = Resources.Load(String.Format("Enemy{0}", type)) as GameObject;
            newTank = Instantiate(newTank, spawnPoint.position, spawnPoint.rotation);
            instance.tanks.Add(new Tank(type,newTank));
            return newTank;
        }

        public static void GameOver(){
            instance.tanks.Clear();
            // instance.tanks = new();
        }
    }
    
    public int spawnTime = 1500;
    public enum EnemiesType{
        Stricker,
        Gunner
    }
    [Serializable]
    public struct EnemiesList{
        public int wave;
        public EnemiesType[] enemies;
    }
    public EnemiesList[] enemiesLists;
    public Transform[] spawnPoints;
    public CameraControl cc;
    public TankHealth playerTH;
    public Canvas gameOverCanvas;

    public class Board{
        private static Board instance;
        private Board(){}

        public int activeEnemiesCount = 0;
        public bool win = false;

        public static Board GetInstance(){
            if(instance == null){
                instance = new Board();
            }
            return instance;
        }

        public static void init(){
            if(instance == null){
                instance = new Board();
            }
            instance.activeEnemiesCount = 0;
            instance.win = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(playerTH.isDead()){
            GameOver(false);
        }
        if(Board.GetInstance().activeEnemiesCount <= 0){
            Spawn();
        }
        if(spawnCounter ++ == spawnTime){
            spawnCounter = 0;
            Spawn();
        }
    }

    private void Spawn(){
        //Debug.Log("Spawn");
        if(currentWave < maxWave){
            foreach(EnemiesType i in enemiesLists[currentWave].enemies){
                int index = UnityEngine.Random.Range(0,spawnPoints.Length);
                GameObject newTank = TankFactory.GetTank(i, spawnPoints[index]);
                //Debug.Log(newTank.name);
                cc.m_Targets.Add(newTank.transform);
                Board.GetInstance().activeEnemiesCount ++;
            }
            currentWave ++;
        }else if(Board.GetInstance().activeEnemiesCount == 0){
            //Debug.Log(String.Format("{0},{1}",currentWave, maxWave));
            GameOver(true);
        }
    }

    public void Init(){
        //maxWave = enemiesLists.Length;
        //playerTH = GameObject.FindGameObjectWithTag("Player").GetComponent<TankHealth>();
        gameOverCanvas.enabled = false;
        currentWave = 0;
        maxWave = 5;
        spawnCounter =0;
        Board.init();
    }

    private void GameOver(bool isWin){
        if(isWin){
            Board.GetInstance().win = true;
            //Debug.Log("You win!");
        }else{
            Board.GetInstance().win = false;
            //Debug.Log("You lose!");
        }
        TankFactory.GameOver();
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
    }
}
