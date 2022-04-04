using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Soundmanager : MonoBehaviour
{
    public static Soundmanager instance = null;

    public GameObject ambient;
    public GameObject music;
    public AudioClip[] clips;
    AudioSource musicSrc;
    public Button button;
    public TMP_Text buttonText;
    int id = 0;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        musicSrc = music.GetComponent<AudioSource>();
    
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        id++;
        if (id > clips.Length)
            id = 0;

        if (id > 0)
        {
            ambient.SetActive(false);
            music.SetActive(true);
            musicSrc.clip = clips[id - 1];

            buttonText.text = clips[id - 1].name;
            musicSrc.Play();
        }
        else
        {
            buttonText.text = "Ambient Music";
            ambient.SetActive(true);
            music.SetActive(false);
        }
    }
}

