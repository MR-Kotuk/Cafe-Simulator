using UnityEngine;

public class SoundActive : MonoBehaviour
{
    [SerializeField] private GameObject _onSFX, _offSFX;

    private bool isSoundActive;

    public void SwitchSoundActive()
    {
        isSoundActive = !isSoundActive;
        if (isSoundActive)
            PlayerPrefs.SetInt("SoundActive", 1);
        else
            PlayerPrefs.SetInt("SoundActive", 0);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("SoundActive") == 1)
        {
            _onSFX.SetActive(true);
            _offSFX.SetActive(false);
        }
        else
        {
            _onSFX.SetActive(false);
            _offSFX.SetActive(true);
        }

        AudioListener.volume = PlayerPrefs.GetInt("SoundActive");
    }
}
