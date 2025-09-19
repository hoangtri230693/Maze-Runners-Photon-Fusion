using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIStartGameManager : MonoBehaviour
{
    public TextMeshProUGUI PlayGame;
    public GameObject SoundOn;
    public GameObject SoundOff;
    public GameObject PanelTutorials;
    
    private float blinkSpeed = 0.5f;


    private void Start()
    {
        if (PlayGame != null)
        {
            StartCoroutine(Blink(PlayGame));
        }
    }

    IEnumerator Blink(TextMeshProUGUI text)
    {
        while (true)
        {
            text.alpha = 1f;
            yield return new WaitForSeconds(blinkSpeed);
            text.alpha = 0f;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    public void OnClickPlayGame()
    {
        SceneManager.LoadScene("Multiplayer");
        StopAllCoroutines();
    }

    public void OnClickSoundOn()
    {
        SoundOn.SetActive(false);
        SoundOff.SetActive(true);
        AudioManager.Instance.SoundBGM.Stop();
    }

    public void OnClickSoundOff()
    {
        SoundOff.SetActive(false);
        SoundOn.SetActive(true);
        AudioManager.Instance.SoundBGM.Play();
    }

    public void OnClickTutorials()
    {
        if (PanelTutorials.activeSelf)
        {
            PanelTutorials.SetActive(false);
        }
        else
        {
            PanelTutorials.SetActive(true);
        }
    }
}
