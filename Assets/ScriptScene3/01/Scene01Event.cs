using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using TMPro;

[System.Serializable]

public class DialogueElement

{

    public string speaker;

    [TextArea(2, 6)]

    public string text;

    public bool playVoice = true;

    public VoiceType voiceType;

}

public enum VoiceType

{

    Ah,

    Amm,

    Er,

    Um,

    None

}

public class Scene01Event : MonoBehaviour

{

    public GameObject fadeScreenIn;

    public GameObject mom;

    public GameObject lantom;

    public GameObject textbox;

    public AudioSource Ah;

    public AudioSource Amm;

    public AudioSource Er;

    public AudioSource Um;

    public GameObject nextButton;

    public int eventPos = 0;

    public List<DialogueElement> dialogueElements = new List<DialogueElement>();

    public TextMeshProUGUI textUI;

    public TextMeshProUGUI speakerUI;

    private int currentTextIndex = 0;

    private bool firstNextDone = false;

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

        if (dialogueElements.Count > 0)

        {

            ShowDialogue(dialogueElements[0]);

        }

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

            if (!firstNextDone)

            {

                StartCoroutine(EventOne());

                firstNextDone = true;

            }

            else

            {

                if (currentTextIndex < dialogueElements.Count - 1)

                {

                    currentTextIndex++;

                    ShowDialogue(dialogueElements[currentTextIndex]);

                }

            }

        }

    }

    void ShowDialogue(DialogueElement element)

    {

        textUI.text = element.text;

        speakerUI.text = element.speaker;

        if (element.playVoice)

        {

            switch (element.voiceType)

            {

                case VoiceType.Ah: Ah.Play(); break;

                case VoiceType.Amm: Amm.Play(); break;

                case VoiceType.Er: Er.Play(); break;

                case VoiceType.Um: Um.Play(); break;

            }

        }

    }

}
