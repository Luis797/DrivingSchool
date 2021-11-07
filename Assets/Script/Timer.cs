using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int i = 3;
    [SerializeField] Text countDownText ;
    public MovementControls movementControls;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip countDown;
    void OnEnable()
    {
        i = 3;
        StartCoroutine(NextCountDown(i));
    }
   void OnDisable()
    {
        countDownText.text = "";
    }
    IEnumerator NextCountDown(int i)
    {
       
        yield return new WaitForSeconds(1f);
        audioSource.clip = countDown;
        audioSource.Play();
        if (i == 0)
        {
            countDownText.text = "GO";
            StartCoroutine(NextCountDown(i - 1));

        }
        else if(i == -1)
        {
            movementControls.enabled = true;

            this.gameObject.SetActive(false);
        }
        else
        {
            countDownText.text = i.ToString();
            StartCoroutine(NextCountDown(i - 1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
