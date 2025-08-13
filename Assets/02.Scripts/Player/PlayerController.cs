using UnityEngine;
using UnityEngine.InputSystem;

namespace _02.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed;
        private Vector2 _curMovementInput; //현재 입력된 이동방향(wasd 등) 저장
        public float jumpForce;
        public LayerMask groundLayerMask;
        
        [Header("Look")]
        public Transform cameraContainer; //카메라가 들어있는 오브젝트
        public float minXLook; //위아래 회전 제한 각도
        public float maxXLook;
        public float camCurXrot; //현재 카메라의 x축 회전 값
        public float lookSensitivity; //마우스 감도
        private Vector2 _mouseDelta; //마우스 입력값 저장
        
        [HideInInspector]
        public bool canLook = true; //커서, 추후 인벤토리 켰을 때 화면 못 움직이게 하는 용
        
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked; //마우스 커서 숨기고 화면 중앙에 고정
        }

        void FixedUpdate() //설정값에 따라 일정 간격으로 호출됨
        {
            Move();
        }

        private void LateUpdate() //모든 update 호출 후 마지막으로 호출됨
        {
            if (canLook)
            {
                CameraLook();
            }
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            _mouseDelta = context.ReadValue<Vector2>(); //마우스 입력을 읽어서 저장
        }

        public void OnMove(InputAction.CallbackContext context) //이동 입력 처리
        {
            if (context.phase == InputActionPhase.Performed) //performed. 이동 키 입력을 받았을 때
            {
                _curMovementInput = context.ReadValue<Vector2>(); //읽은 키를 저장
            }
            else if(context.phase == InputActionPhase.Canceled) //canceled. 입력키에서 손 뗐을 때
            {
                _curMovementInput = Vector2.zero; //초기화. 이동 멈춤.
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started && IsGrounded()) //점프키 눌렀을 때, 땅 위에 있을 때만 시작
            {
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse); //힘(Impulse)을 가해서 점프 적용
            }
        }

        private void Move() //움직임 적용
        {
            Vector3 dir = transform.forward * _curMovementInput.y + transform.right * _curMovementInput.x; //오브젝트가 보고있는 방향(앞)*앞뒤이동 + 오브젝트의 오른쪽방향*좌우이동
            dir *= moveSpeed; //이동방향 dir * 속도
            dir.y = _rigidbody.velocity.y; //중력 유지. 현재 y축 속도를 유지하여 점프나 중력 영향을 받게 함
            _rigidbody.velocity = dir; //최종 속도 적용. Rigidbody의 속도를 갱신하여 물리 기반 이동 적용
        }
        
        void CameraLook()
        {
            camCurXrot += _mouseDelta.y *  lookSensitivity; //위아래 회전 적용. y값 곱해서 위아래 시점 변경
            camCurXrot = Mathf.Clamp(camCurXrot, minXLook, maxXLook); //Mathf.Clamp()로 minXLook ~ MaxXLook 범위로 제한
            cameraContainer.localEulerAngles = new Vector3(-camCurXrot, 0, 0); //카메라 회전 적용

            transform.eulerAngles += new Vector3(0, _mouseDelta.x * lookSensitivity, 0); //좌우방향 변경
        }
        
        bool IsGrounded()
        {
            Ray[] rays = new Ray[4] //플레이어 바닥 4곳에서 아래 방향으로 Ray 발사
            {
                    new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
                    new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
                    new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
                    new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
            };
        
            for(int i = 0; i < rays.Length; i++)
            {
               if (Physics.Raycast(rays[i], 0.1f, groundLayerMask)) //Ray가 groundLayerMask에 닿으면
                {
                    return true; //true 반환(땅위에 있음)
                }
            }
            return false;
        }
    }
}
