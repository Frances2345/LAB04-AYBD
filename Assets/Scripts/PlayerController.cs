using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions inputs;
    private CharacterController controller;

    public CinemachineCamera characterCamera;
    public TextMeshProUGUI healthText;

    private Vector2 moveInput;

    public float health = 100f;
    public int attack = 20;

    public float moveSpeed = 10f;
    public float rotationSpeed = 200f;

    private void Awake()
    {
        inputs = new();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        inputs.Enable();
        inputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * moveInput.x * rotationSpeed * Time.deltaTime);
        Vector3 moveDir = (transform.forward * moveInput.y) * moveSpeed;

        if (!controller.isGrounded) moveDir.y = -9.81f;

        controller.Move(moveDir * Time.deltaTime);
    }

    public void ActualizarUI()
    {
        if (healthText != null)
        {
            healthText.text = "Vida: " + health;
        }
    }
}