using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_Ending : MonoBehaviour
{
    public void endGame()
    {
        SceneManager.LoadScene(0);
    }
}
