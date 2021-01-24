using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StaticDataInitialization : SingletonBase<StaticDataInitialization>
{
    // Start is called before the first frame update
    void Awake()
    {
        SkillData.Init();
        LevelData.Init();
        SceneManager.LoadScene("scLobby");
    }

}
