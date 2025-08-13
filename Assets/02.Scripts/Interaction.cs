using System;
using System.Collections;
using System.Collections.Generic;
using _02.Scripts.Item;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Interaction : MonoBehaviour
{
    public LayerMask layerMask;
    public GameObject curInteractGameObject;
    private ItemObject curInteractable;
    public TextMeshProUGUI promptText;
    public float time = 2f; //아이템 효고 설명 떠 있는 시간
    public bool isOnCorutine = false;
    public Coroutine curCoroutine;

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) //상호작용 입력키(우클릭) 받았을 때
        {
            Debug.Log("우클릭 되는중");
            if (curInteractGameObject.CompareTag("Food") && curInteractGameObject.TryGetComponent(out ItemUse effectInfo)) //현재 보고있는 오브젝트가 Food 태그이고, effectInfo값이 있다면
            {
                Debug.Log("eat 작동중");
                string effect = effectInfo.Eat();
                promptText.text = effect;
                Destroy(curInteractGameObject);
                curCoroutine = StartCoroutine(WaitTime(time, () =>
                {
                    isOnCorutine = true;
                    Debug.Log("시작");
                    Debug.Log(promptText.text);
                    promptText.gameObject.SetActive(true);
                }, () =>
                {
                    Debug.Log($"{time}초 동안 호출됨");
                    isOnCorutine = false;
                    curCoroutine = null;
                }));
            }
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
                    curInteractGameObject = hit.collider.gameObject; //확인하려는 콜라이더에 현재 콜라이더에 넣고
                    curInteractable = hit.collider.GetComponent<ItemObject>(); //확인하려는 콜라이더(맞은 애) 정보 가져옴
                    promptText.gameObject.SetActive(true);
                    promptText.text = curInteractable.GetInteractionInfo(); //정보 띄우기
                }
                //Debug.Log(hit.collider.name);
                Debug.DrawLine(ray.origin, hit.point, Color.red);
            }
            else if(curCoroutine == null)
            {
                promptText.gameObject.SetActive(false);
            }
    }
    
    IEnumerator WaitTime(float duration, Action onStart = null, Action onEnd = null)
    {
        onStart?.Invoke();  //'?' = null이면 넘어가고 아니면 실행
        yield return new WaitForSeconds(duration);
        onEnd?.Invoke();
    }
}
