using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType{
    Buff,Potion,Goods
}

public abstract class Item : MonoBehaviour
{

    public ItemType type;
    protected FSMPlayer player;
    protected abstract void GetItem(FSMPlayer player); 
    private void Start()
    {
        player = PlayerManager.Instance.Player;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetItem(player);
            gameObject.SetActive(false);
        }
    }
}
