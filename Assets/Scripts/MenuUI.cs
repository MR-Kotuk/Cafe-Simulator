using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private AudioSource _onButton;

    public void OnToHomeButton()
    {
        NextScene("HomeScene");
    }
    public void OnToShopButton()
    {
        NextScene("ShopScene");
    }

    public void OnToGameButton()
    {
        NextScene("GameScene");
    }

    private void NextScene(string nameScene)
    {
        _onButton.Play();
        PlayerPrefs.Save();
        SceneManager.LoadScene(nameScene);
    }
}
