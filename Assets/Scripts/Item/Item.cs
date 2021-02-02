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
    protected abstract void GetItem(); 
    private void Start()
    {
        player = PlayerManager.Instance.Player;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetItem();
            gameObject.SetActive(false);
        }
    }
}
