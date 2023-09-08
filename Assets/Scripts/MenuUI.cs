using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void OnToHomeButton()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("HomeScene");
    }
    public void OnToShopButton()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("ShopScene");
    }

    public void OnToGameButton()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
}
