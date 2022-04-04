using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public static GameAudio instance = null;
    [HideInInspector]
    public int bgmIndex = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

