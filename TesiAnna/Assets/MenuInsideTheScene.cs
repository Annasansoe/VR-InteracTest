using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuInsideTheScene : MonoBehaviour
{

    [Header("Return Buttons")]
    public Button returnToMainMenu;

    // Start is called before the first frame update
    void Start()
    {


        returnToMainMenu.onClick.AddListener(ReturnToMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReturnToMenu()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(0);
    }
}
