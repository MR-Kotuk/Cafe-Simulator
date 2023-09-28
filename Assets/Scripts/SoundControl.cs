using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    [SerializeField] private RandomOrder _randomOrder;
    [SerializeField] private OnCoffee _onCoffee;

    [SerializeField] private Animator _anim;

    [SerializeField] private AudioSource _buttonSFX, _buttonSFX2, _coffeeMakerSFX, _poulSFX, _timerSFX;
    [SerializeField] private AudioSource _getMoneySFX, _getTipsSFX;

    [SerializeField] private List<AudioSource> _backgroundMusic;

    private bool isTime = true;
    private int _noPlaysSounds, _randomSound;


    public void OnButtonAnCoffeeMaker()
    {
        if(_onCoffee.isInMaker)
            _buttonSFX.Play();
    }

    public void OnPoulButton()
    {
        if (_onCoffee.isInMaker)
        {
            _coffeeMakerSFX.Play();
            _poulSFX.Play();
        }
    }

    public void GameSFX(string sfxName)
    {
        switch (sfxName)
        {
            case "GetTips":
                _getTipsSFX.Play();
                break;
            case "GetMoney":
                _getMoneySFX.Play();
                break;
            case "OnButton":
                _buttonSFX2.Play();
                break;
        }
    }
    private void Awake()
    {
        _randomSound = Random.Range(0, _backgroundMusic.Count);
        _backgroundMusic[_randomSound].Play();
        _noPlaysSounds = 0;
    }

    private void FixedUpdate()
    {  
        if (_noPlaysSounds == _backgroundMusic.Count)
        {
            _noPlaysSounds = 0;

            if (_randomSound != _backgroundMusic.Count - 1)
                _randomSound++;
            else
                _randomSound = 0;

            _backgroundMusic[_randomSound].Play();
        }
        else
        {
            for (int i = 0; i < _backgroundMusic.Count; i++)
            {
                if (!_backgroundMusic[i].isPlaying)
                    _noPlaysSounds++;
                else
                {
                    _noPlaysSounds = 0;
                    break;
                }

            }
        }

        if (_randomOrder.time <= 8f && isTime)
        {
            isTime = false;
            _timerSFX.Play();
            _anim.SetBool("isWork", true);
        }
        else if(_randomOrder.time <= 0 || _randomOrder.time > 25)
            _anim.SetBool("isWork", false);

        if (_randomOrder.time <= 0.5f)
            _timerSFX.Stop();
    }
}
