using Code.Player.Components;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Code.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private InputReceiver input;
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private ContactChecker contactChecker;
        

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            input.OnJumpInput += HandleJump;
            input.OnMoveInput += HandleMoveInput;
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

        private void Update()
        {
            rb.linearVelocity = rb.linearVelocity;
        }

        private void OnDestroy()
        {
            input.OnJumpInput -= HandleJump;
            input.OnMoveInput -= HandleMoveInput;
        }
    }
}