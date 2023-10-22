using System.Collections.Generic;
using UnityEngine;

public class StartTutorials : MonoBehaviour
{
    [SerializeField] private List<GameObject> _startTutorials;
    private int tutorMunuNum;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("MenuTutorials"))
        {
            PlayerPrefs.SetInt("HowDays", 1);
            Time.timeScale = 0;
            _startTutorials[0].SetActive(true);
            gameObject.SetActive(true);
        }
        else
            gameObject.SetActive(false);
    }
    public void NextMenu()
    {
        if (!PlayerPrefs.HasKey("MenuTutorials"))
        {
            if (tutorMunuNum < _startTutorials.Count)
                _startTutorials[tutorMunuNum].SetActive(false);

            tutorMunuNum++;
            if (tutorMunuNum < _startTutorials.Count)
                _startTutorials[tutorMunuNum].SetActive(true);
            else
            {
                PlayerPrefs.SetInt("MenuTutorials", 1);
                Time.timeScale = 1;

                gameObject.SetActive(false);
            }
        }
    }
}
