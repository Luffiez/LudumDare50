using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void ResetTheScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
