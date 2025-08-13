using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public LayerMask layerMask;
    public TextMeshPro textMeshPro;
    public ItemObject itemObject;

    public void OnInteraction(InputAction.CallbackContext context)
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (context.phase == InputActionPhase.Started) //상호작용 입력키(우클릭) 받았을 때
            {
                if (Physics.Raycast(ray, out hit, 100, layerMask))
                {
                    Debug.Log(hit.collider.name); //이름 출력
                    Debug.DrawLine(ray.origin, hit.point, Color.red);
                }
            }
    }

    public void ItemInformation(ItemObject itemObject)
    {
        //아이템아이템 정보
    }
}
