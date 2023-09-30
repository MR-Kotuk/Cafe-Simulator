using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private AudioSource _onButton;
    public void NextScene(string nameScene)
    {
        _onButton.Play();
        PlayerPrefs.Save();
        SceneManager.LoadScene(nameScene);
    }
}
