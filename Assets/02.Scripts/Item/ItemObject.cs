using System.Collections;
using System.Collections.Generic;
using _02.Scripts;
using _02.Scripts.Player;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemInfo itemInfo;
    public PlayerCondition playerCondition;


    public string GetInteractionInfo()
    {
        string str = $"{itemInfo.name}\n{itemInfo.description}"; //화면에 띄울 정보(이름, 설명)
        return str;
    }
}
