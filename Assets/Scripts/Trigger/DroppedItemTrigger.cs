using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private Item item;
    private FSMPlayer player;

    public ItemType type;
    private void Awake()
    {
        item = new Item(type);
    }
    private void Start()
    {
        player = GameSceneManager.Instance.Player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            item.GetItem(player);
            gameObject.SetActive(false);
        }
    }
}
