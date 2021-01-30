using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StaticDataInitialization : SingletonBase<StaticDataInitialization>
{
    // Start is called before the first frame update
    public SkillInfo[] baseSkill;
    public Stat[] stats;
    public Dungeon[] dungeons;
    void Awake()
    {
        SkillData.Init(baseSkill);
        LevelData.Init(stats);
        DungeonDataIO.Init(dungeons);
        SceneManager.LoadScene("scLobby");
    }

}
