using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTrigger : MonoBehaviour
{
    FSMPlayer player;

    [SerializeField]
    private float battleTime;
    [SerializeField]
    private float spawnInterval;
    


    private BoxCollider box;
    private bool isEnd=false;
    private Collider[] hits;

    public EnemySpawn[] enemySpawn;
    [HideInInspector]
    public List<Door> Doors_Cur;
    [HideInInspector]
    public List<Door> Doors_Next;
    private void Awake()
    {
        box = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        for (int i = 0; i < Doors_Cur.Count; i++)
        {
            Doors_Cur[i].Open();
        }
        for (int i = 0; i < Doors_Next.Count; i++)
        {
            Doors_Next[i].Close();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player=other.gameObject.GetComponent<FSMPlayer>();
            player.IsInStageBtn = true;
            player.stageTrigger = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<FSMPlayer>();
            player.IsInStageBtn = false;
            player.stageTrigger = null;
        }
    }
    public void StartBattle()
    {
        GameSceneManager.Instance.OnBattle = true;
        box.enabled = false;
        player.stageTrigger = null;
        player.IsInStageBtn = false;
        StartCoroutine(OnBattle());
        for (int i = 0; i < Doors_Cur.Count; i++)
        {
            Doors_Cur[i].Close();
        }
    }
    private IEnumerator OnBattle()
    {
        float time = 0f;
        float timeInterval = 0f;
        
        while(time<= battleTime)
        {
            if (timeInterval >= spawnInterval)
            {
                timeInterval = 0.0f;
                for (int i = 0; i < enemySpawn.Length; i++)
                {
                    enemySpawn[i].Spawn();
                }
            }
            yield return null;
            time += Time.deltaTime;
            timeInterval += Time.deltaTime;
        }
        yield return StartCoroutine(CheckMonster());
        for (int i = 0; i < Doors_Cur.Count; i++)
        {
            Doors_Cur[i].Open();
        }
        for (int i = 0; i < Doors_Next.Count; i++)
        {
            Doors_Next[i].Open();
        }
        player.Damaged(player.health.MaxHP*-1);
        GameSceneManager.Instance.OnBattle = false;

    }
    private IEnumerator CheckMonster()
    {
        do
        {
            yield return null;
            hits=Physics.OverlapBox(this.transform.position, new Vector3(25f, 12.5f, 25f), Quaternion.identity, 1 << 8);
        } while (hits.Length!=0);
    }
}
