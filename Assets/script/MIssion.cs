using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class Mission : MonoBehaviour
{
    static public Mission nP;
    public GameObject NPCPnel; // Panel hiển thị cuộc đối thoại với NPC
    public GameObject QuestPanel; // Panel hiển thị nhiệm vụ
    public int quest; // Theo dõi số lượng quái đã giết
    public TextMeshProUGUI NPCContent; // Hiển thị nội dung đối thoại của NPC
    public TextMeshProUGUI QuestContent; // Hiển thị tiến độ nhiệm vụ

    // Nội dung đối thoại
    public string[] content; // Mảng lưu các dòng thoại của NPC
    private Coroutine coroutine;

    void Start()
    {
        NPCContent.text = "";
        coroutine = StartCoroutine(ReadContent());
        QuestPanel.SetActive(false);
    }

    private IEnumerator ReadContent()
    {
        foreach (var line in content)
        {
            NPCContent.text += " ";
            foreach (var item in line)
            {
                NPCContent.text += item;
                yield return new WaitForSeconds(0.1f); // Hiệu ứng gõ chữ
            }
            NPCContent.text += "\n";
            yield return new WaitForSeconds(0.2f); // Tạm dừng giữa các dòng
        }
        yield return new WaitForSeconds(2f);
           
        NPCContent.text = "";
        NPCPnel.SetActive(false);
        QuestPanel.SetActive(true);
        quest = 0; // Đặt lại số lượng quái khi bắt đầu nhiệm vụ
        QuestContent.text = "Giết quái: " + quest + "/20";
    }

    public void endContent()
    {
        NPCPnel.SetActive(false);
        NPCContent.text = "";
        StopCoroutine(coroutine);
    }

    public void GetQuest()
    {
       
        QuestPanel.SetActive(true);
      
        Debug.Log("Request");
    }

    // Được gọi khi giết quái
    public void killEnermy()
    {
        quest ++; // Tăng tiến trình nhiệm vụ
        QuestContent.text = "Giết quái: " + quest + "/20";
        Debug.Log("killEnermy");
        // Thêm điểm khi giết quái
        // Thêm 10 điểm cho mỗi lần giết quái (có thể điều chỉnh giá trị)

        if (quest >= 20)
        {
            CompleteQuest();
            Debug.Log(quest);
        }
    }

    // Phương thức cộng điểm

    private IEnumerator HideQuestPanelAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        QuestPanel.SetActive(false);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
        SceneManager.LoadSceneAsync(0);
   


    }
    public void CompleteQuest()
    {
        QuestContent.text = "Nhiệm vụ hoàn thành!";
        QuestContent.color = Color.green;
       
        StartCoroutine(HideQuestPanelAfterDelay());
    }
}

