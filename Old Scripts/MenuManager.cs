using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] string testFlightScene;

    [SerializeField] GameObject optionsMenu;

    GameObject currentMenu;

    public void LoadTestFlight()
    {
        SceneManager.LoadScene(testFlightScene);
    }

    public void OpenOptions()
    {
        OpenSubMenu(optionsMenu);
    }

    void OpenSubMenu(GameObject menu)
    {
        menu.SetActive(true);

        currentMenu = menu;
    }

    void CloseCurrentMenu()
    {
        currentMenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
