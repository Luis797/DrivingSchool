using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;
    AudioSource audioSource;
    [SerializeField] AudioClip pickedValuableCoin;
    [SerializeField] AudioClip pickedUnvaluableCoin;
    void Start()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void CoinCollect()
    {
        audioSource.clip = pickedValuableCoin;
        audioSource.Play();
    }
    public void WrongCoinCollect()
    {
        audioSource.clip = pickedUnvaluableCoin;
        audioSource.Play();
    }
}
