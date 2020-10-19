using UnityEngine;

namespace SceneHandling
{
    //Non static class for event based callbacks
    public class MainMenuHelper : MonoBehaviour
    {
        public void LoadGame()
        {
            SceneController.LoadSceneSingle(1);
        }

        public void Quit()
        {
            SceneController.QuitGame();
        }
    }
}