using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] Button playButton;
        [SerializeField] Button optionButton;
        [SerializeField] Button exitButton;

        [SerializeField] GameObject OptionMenu;
        [SerializeField] GameObject mainMenu;

        public void PlayMainScene()
        {
            SceneManager.LoadScene("MainScene");
        }

        public void OpenOptionMenu(bool open)
        {
            mainMenu.gameObject.SetActive(!open);
            OptionMenu.gameObject.SetActive(open);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

    }
}
