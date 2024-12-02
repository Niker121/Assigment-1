using UnityEngine;
using UnityEngine.SceneManagement; // Để quản lý các cảnh
using UnityEngine.UI; // Để sử dụng UI

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Tải cảnh trò chơi (đảm bảo bạn đã thêm cảnh vào Build Settings)
        SceneManager.LoadScene("Map");
    }
    public void ExitGame()
    {
        // Thoát ứng dụng
        Application.Quit();
        Debug.Log("Game Exited");
    }
}