using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float sprintSpeed = 8f;
    public float gravity = 9.81f;

    [Header("Rotación de Cámara")]
    public Transform cameraTransform;
    public float mouseSensitivity = 100000f;

    private CharacterController controller;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Bloquear el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MovePlayer();
        RotateCamera();

        if (Input.GetKeyDown(KeyCode.KeypadPlus)) // Tecla "+" del teclado numérico
            mouseSensitivity += 10f;

        if (Input.GetKeyDown(KeyCode.KeypadMinus)) // Tecla "-" del teclado numérico
            mouseSensitivity = Mathf.Max(10f, mouseSensitivity - 10f); // Evita valores negativos

    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D o flechas izquierda/derecha
        float moveZ = Input.GetAxis("Vertical");   // W/S o flechas arriba/abajo

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Evitar movimiento diagonal más rápido
        if (move.magnitude > 1)
            move.Normalize();

        // Aplicar sprint solo si el jugador está en el suelo
        float currentSpeed = (controller.isGrounded && Input.GetKey(KeyCode.LeftShift)) ? sprintSpeed : speed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Aplicar gravedad
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Pequeño empuje hacia abajo para mantenerlo pegado al suelo
        }

        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX); // Rotación horizontal del jugador

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Limitar la mirada arriba/abajo
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        float MouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 9f * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 9f * Time.deltaTime;
    }
}

