using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class DungeonUI : MonoBehaviour
{
    private string dungeonSceneName;
    public TextMeshProUGUI NeedHeartText;
    public TextMeshProUGUI DungeonNameText;
    public Image Thumbnail;
    public SFXButton btn;
    public void SetDungeonInfo(Dungeon dungeon)
    {

        NeedHeartText.text = dungeon.NeedHeart.ToString();
        DungeonNameText.text = dungeon.DungeonName;
        dungeonSceneName = dungeon.DungeonSceneName;
        Thumbnail.sprite = dungeon.thumbnail;
        btn.onClick.AddListener(GotoDungeon);
        this.gameObject.SetActive(true);
    }
    public void GotoDungeon()
    {
        LoadingSceneManager.LoadScene(dungeonSceneName);
    }

}
