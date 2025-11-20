using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene01Event : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject mom;
    public GameObject lantom;
    public GameObject textbox;

    public AudioSource Ah;
    public AudioSource Amm;

    void Start()
    {
        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(2);
        fadeScreenIn.SetActive(false);
        mom.SetActive(true);
        yield return new WaitForSeconds(2);
        mom.SetActive(false);

        textbox.SetActive(true);
        Ah.Play();
        yield return new WaitForSeconds(2);
        lantom.SetActive(true);

    }

}