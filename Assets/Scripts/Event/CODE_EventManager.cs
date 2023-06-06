using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CODE_EventManager : MonoBehaviour
{
    CODE_Player player;
    CODE_UIManager uiManager;

    void Start()
    {
        player = FindObjectOfType<CODE_Player>();
        uiManager = GetComponent<CODE_UIManager>();

        player.ChangedItem += uiManager.OnChangedItem;

    }

}
