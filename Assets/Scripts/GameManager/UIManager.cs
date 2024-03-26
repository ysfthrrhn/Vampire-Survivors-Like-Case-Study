using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject endPanel;

    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private TextMeshProUGUI coinText;

    [SerializeField] private TextMeshProUGUI endKillText;
    [SerializeField] private TextMeshProUGUI endCoinText;

    public void UpdateCoinText(int count)
    {
        coinText.text = count.ToString();
    }
    public void UpdateKillText(int count)
    {
        killText.text = count.ToString();
    }

    public void OpenEndPanel()
    {
        UpdateEndUI();
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    private void UpdateEndUI()
    {
        endKillText.text += killText.text;
        endCoinText.text += coinText.text;
    }

}
