using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement; // Để quản lý các cảnh
using UnityEngine.UI; // Để sử dụng UI

public class MainMenu : MonoBehaviour
{
    public GameObject loeaderUi;
    public Slider progressSlider;

    public void LoadScene(int index)
    {
        StartCoroutine(LoeadScene_Coroutine(index));
    }

    public IEnumerator LoeadScene_Coroutine(int index)
    {
        progressSlider.value = 0;
        loeaderUi.SetActive(true);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = true;
        float progress = 0;
        while (!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress,asyncOperation.progress,Time.deltaTime);
            progressSlider.value = progress;
            if( progress >= 0.9f)
            {
                progressSlider.value = 1;
                asyncOperation.allowSceneActivation = true ;
            }
            yield return null;
        }
    }
    public void ExitGame()
    {
        // Thoát ứng dụng
        Application.Quit();
        Debug.Log("Game Exited");
    }
}