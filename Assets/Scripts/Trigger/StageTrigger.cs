using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class StageTrigger : MonoBehaviour
{    [SerializeField]
    private float battleTime;
    [SerializeField]
    private float spawnInterval;
    [SerializeField]
    private float bossAppearTime;
    [SerializeField]
    private float maxBattleTime;

    [SerializeField]
    private bool isBossStage=false;
    [SerializeField]
    private string bgmName;

    private FSMPlayer player;
    private ParticleSystem ps;
    private BoxCollider box;
    private Collider[] hits;

    public UnityEvent StartEvent;
    public UnityEvent BossEvent;
    public UnityEvent EndEvent;
    
    
    
    public EnemySpawn[] enemySpawn;

    [HideInInspector]  public List<Door> Doors_Cur;
    [HideInInspector]  public List<Door> Doors_Next;
    private void Awake()
    {
        box = GetComponent<BoxCollider>();
        ps = GetComponent<ParticleSystem>();
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
        if (isBossStage)
        {
            EndEvent.AddListener(delegate { UIManager.Instance.ResultWindow.SetActive(true); });
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player=other.gameObject.GetComponent<FSMPlayer>();
            player.BtnNum = 1;
            player.stageTrigger = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<FSMPlayer>();
            player.BtnNum = 0;
            player.stageTrigger = null;
        }
    }
    public void StartBattle()
    {
        PlayerManager.Instance.OnBattle = true;
        box.enabled = false;
        player.stageTrigger = null;
        player.BtnNum = 0;
        ps.Stop();
        StartCoroutine(OnBattle());
        SoundManager.Instance.PlayBGM(bgmName);

        SoundManager.Instance.PlaySFX("DoorClose");
        if (BossEvent != null)
        {
            StartCoroutine(BossAppearence());
        }
        for (int i = 0; i < Doors_Cur.Count; i++)
        {
            Doors_Cur[i].Close();
        }
    }
    private IEnumerator OnBattle()
    {
        if (StartEvent != null)
        {
            StartEvent.Invoke();
        }
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
            time += Time.deltaTime*Time.timeScale;
            timeInterval += Time.deltaTime * Time.timeScale;
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
        player.RecoverHP((int)player.health.MaxHP);
        player.RecoverMP((int)player.mana.MaxMP);
        PlayerManager.Instance.OnBattle = false;
        if (time < maxBattleTime)
        {
            float score = (10000 * (1 - (time / maxBattleTime)));
            player.redGem+=(int)(10 * (1 - (time / maxBattleTime)));
            PlayerManager.Instance.score += (int)score;
            PlayerManager.Instance.Player.GetExp((int)(score/10));
        }
        GameSceneManager.Instance.playTime += time;

        SoundManager.Instance.PlayBGM("BGM_Dungeon");

        SoundManager.Instance.PlaySFX("DoorOpen");
        if (EndEvent != null)
        {
            EndEvent.Invoke();
        }
    }
    private IEnumerator BossAppearence()
    {
        float t = 0f;
        while (t<bossAppearTime)
        {
            yield return null;
            t += Time.deltaTime * Time.timeScale;
        }
        BossEvent.Invoke();
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
