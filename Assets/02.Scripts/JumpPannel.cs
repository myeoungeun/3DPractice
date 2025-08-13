using System;
using _02.Scripts.Player;
using UnityEngine;

namespace _02.Scripts
{
    public class JumpPannel : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public float jumpForce;
        
        private void OnCollisionEnter(Collision collision) //트램펄린
        {
           if(collision.gameObject.CompareTag("Player"))
           {
               _rigidbody.AddForce(Vector3.up * jumpForce * 2, ForceMode.Impulse);
           }
        }
        
        private void OnCollisionExit(Collision collision)
        {
            Debug.Log("Player exited");
        }
        
        void Start()
        {
            _rigidbody = CharacterManager.Instance.Player.GetComponent<Rigidbody>();
        }
    }
}
