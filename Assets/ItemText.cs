﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ItemText : MonoBehaviour{
    public PlayerControl player;
    private GameObject itemTextPanel;

    void Start()
    {
        itemTextPanel = GameObject.Find("ItemTextPanel");
        itemTextPanel.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.GetComponent<PlayerControl>() == null)
        {
            return;
        }
        itemTextPanel.SetActive(true);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        itemTextPanel.SetActive(false);
    }
}