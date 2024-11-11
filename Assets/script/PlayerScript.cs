using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;  // Tham chiếu đến CharacterController
    public float speed = 6f;                // Tốc độ di chuyển của nhân vật
    public float gravity = -9.81f;          // Trọng lực nhân vật
    public float jumpHeight = 1.5f;         // Độ cao khi nhảy

    private Vector3 velocity;               // Vector vận tốc hiện tại của nhân vật
    public Transform groundCheck;           // Vị trí kiểm tra xem nhân vật có đang trên mặt đất không
    public float groundDistance = 0.4f;     // Khoảng cách kiểm tra với mặt đất
    public LayerMask groundMask;            // Lớp đối tượng của mặt đất

    private bool isGrounded;

    void Update()
    {
        // Kiểm tra xem nhân vật có đang trên mặt đất không
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset vận tốc rơi nếu đang trên mặt đất
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Nhận input từ bàn phím cho hướng di chuyển
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Tính toán hướng di chuyển theo trục X và Z
        Vector3 move = transform.right * x + transform.forward * z;

        // Di chuyển nhân vật
        controller.Move(move * speed * Time.deltaTime);

        // Nhảy nếu đang trên mặt đất và nhấn phím nhảy
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Áp dụng trọng lực
        velocity.y += gravity * Time.deltaTime;

        // Di chuyển theo trục Y (lên/xuống)
        controller.Move(velocity * Time.deltaTime);
    }
}
