using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    public GameObject ambient;
    public GameObject music;
    public AudioClip[] clips;
    AudioSource musicSrc;
    public Button button;
    public TMP_Text buttonText;

    GameAudio gameAudio;

    private void Start()
    {
        musicSrc = music.GetComponent<AudioSource>();
        if(button)
            button.onClick.AddListener(OnClick);

        gameAudio = GameAudio.instance;
        if(gameAudio != null)
            Play(gameAudio.bgmIndex);
    }

    private void OnClick()
    {
        gameAudio.bgmIndex++;

        if (gameAudio.bgmIndex > clips.Length)
            gameAudio.bgmIndex = 0;
        Play(gameAudio.bgmIndex);
    }

    void Play(int id)
    {
        if (id > 0)
        {
            ambient.SetActive(false);
            music.SetActive(true);
            musicSrc.clip = clips[id - 1];
            if(buttonText)
                buttonText.text = clips[id - 1].name;
            musicSrc.Play();
        }
        else
        {
            if (buttonText)
                buttonText.text = "Ambient Music";
            ambient.SetActive(true);
            music.SetActive(false);
        }
    }

}

