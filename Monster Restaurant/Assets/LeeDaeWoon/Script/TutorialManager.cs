using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject tutorial;
    [SerializeField] int tutorialNum = 0;

    void Start()
    {
        SoundManager.instance.PlaySoundClip("Ingame_bgm", SoundType.BGM);
    }

    void Update()
    {
        //AnyKeyDown();
        TutorialCheck();
    }

    void AnyKeyDown()
    {
        if (Input.anyKeyDown)
        {
            tutorialNum++;
        }
    }

    void TutorialCheck()
    {
        if (tutorial.transform.childCount > tutorialNum + 1)
        {
            tutorial.transform.GetChild(1 + tutorialNum).gameObject.SetActive(true);

            if (tutorialNum > 0)
                tutorial.transform.GetChild(tutorialNum).gameObject.SetActive(false);
        }

        else
            SceneManager.LoadScene("SubMain");
    }
}
