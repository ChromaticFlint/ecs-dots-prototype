using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private PlayerControls playerInput;
  private CharacterController controller;
  private Vector3 playerVelocity;
  private bool groundedPlayer;

  [SerializeField]
  private float playerSpeed = 2.0f;
  [SerializeField]
  private float jumpHeight = 1.0f;
  [SerializeField]
  private float gravityValue = -9.81f;
  [SerializeField]
  private float rotationSpeed = 4.00f;

  private void Awake()
  {
    playerInput = new PlayerControls();
    controller = GetComponent<CharacterController>();
  }

  private void OnEnable()
  {
    playerInput.Enable();
  }

  private void OnDisable()
  {
    playerInput.Disable();
  }

  void Update()
  {
    groundedPlayer = controller.isGrounded;
    if (groundedPlayer && playerVelocity.y < 0)
    {
      playerVelocity.y = 0f;
    }

    Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();

    Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y);
    controller.Move(move * Time.deltaTime * playerSpeed);

    if (move != Vector3.zero)
    {
      gameObject.transform.forward = move;
    }

    playerVelocity.y += gravityValue * Time.deltaTime;
    controller.Move(playerVelocity * Time.deltaTime);
  }
}

