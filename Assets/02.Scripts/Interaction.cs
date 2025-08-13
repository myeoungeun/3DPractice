using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour
{
    public LayerMask layerMask;
    public GameObject curInteractGameObject;
    private ItemObject curInteractable;
    public TextMeshProUGUI promptText;

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) //상호작용 입력키(우클릭) 받았을 때
        { 
            //아이템 먹기/사용 가능하도록
        } 
    }

    void Update()
    {
        LookItem();
    }

    public void LookItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); //화면 가운데 체크
        RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, layerMask)) //레이어마스크로 상호작용 가능한 물건인지 체크
            {
                if(hit.collider.gameObject != curInteractGameObject) //확인하려는 콜라이더가 현재 게임오브젝트와 다르면
                {
                    curInteractGameObject = hit.collider.gameObject; //확인하려는 콜라이더를 현재 콜라이더에 넣고
                    curInteractable = hit.collider.GetComponent<ItemObject>(); //확인하려는 콜라이더 정보 가져옴
                    promptText.gameObject.SetActive(true);
                    promptText.text = curInteractable.GetInteractionInfo(); //정보 띄우기
                }
                //Debug.Log(hit.collider.name);
                Debug.DrawLine(ray.origin, hit.point, Color.red);
            }
            else
            {
                promptText.gameObject.SetActive(false);
            }
    }
}
