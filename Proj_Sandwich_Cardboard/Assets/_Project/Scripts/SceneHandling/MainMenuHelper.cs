using UnityEngine;

namespace SceneHandling
{
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