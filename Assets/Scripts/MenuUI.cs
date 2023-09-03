using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void OnToHomeButton()
    {
        //SceneManager.LoadScene("");
    }
    public void OnToShopButton()
    {
        //SceneManager.LoadScene("");
    }

    public void OnToGameButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}
