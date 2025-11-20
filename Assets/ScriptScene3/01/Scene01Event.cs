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

    public GameObject nextButton;

    public int eventPos = 0;

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

        eventPos = 1;

        textbox.SetActive(true);

        nextButton.SetActive(true);

        Ah.Play();

    }

    IEnumerator EventOne()

    {

        nextButton.SetActive(false);

        textbox.SetActive(true);

        nextButton.SetActive(true);

        yield return new WaitForSeconds(2);

        lantom.SetActive(true);

    }

    public void NextButton()

    {

        if (eventPos == 1)

        {

            StartCoroutine(EventOne());

            eventPos = 2;

        }

    }

}
