using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLauncher : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadSceneAsync("XRRig-Full", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Default-Environment", LoadSceneMode.Additive);
    }
}
