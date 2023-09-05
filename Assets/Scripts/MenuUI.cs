using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void OnToHomeButton()
    {
        SceneManager.LoadScene("HomeScene");
    }
    public void OnToShopButton()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void OnToGameButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}
