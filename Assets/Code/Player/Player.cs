using Code.Player.Components;
using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private InputReceiver input;
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private ContactChecker contactChecker;
        [SerializeField] private PlayerRenderer playerRenderer;

        private static readonly int XVelocityHash = Animator.StringToHash("XVelocity");
        private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            input.OnJumpInput += HandleJump;
            input.OnMoveInput += HandleMoveInput;
        }
        
        private void Update()
        {
            playerRenderer.SetFloatValue(XVelocityHash, Mathf.Abs(playerMover.Velocity.x));
            playerRenderer.SetBoolValue(IsGroundedHash, contactChecker.IsGrounded);

            //bool isSeeRight = true;
            //if (Keyboard.current.aKey.wasPressedThisFrame && isSeeRight)
            //{
            //    playerRenderer.SetFlip(true);
            //    isSeeRight = false;
            //    Debug.Log("turned");
            //}
            //if (Keyboard.current.dKey.wasPressedThisFrame && !isSeeRight)
            //{
            //    playerRenderer.SetFlip(true);
            //    isSeeRight= true;
            //    Debug.Log("turned");

            //}
        }

        private void HandleMoveInput(float obj)
        {
            playerMover.SetMoveInput(obj);
        }

        private void HandleJump()
        {
            if(contactChecker.IsGrounded)
                playerMover.Jump();
        }
        
        private void OnDestroy()
        {
            input.OnJumpInput -= HandleJump;
            input.OnMoveInput -= HandleMoveInput;
        }
    }
}