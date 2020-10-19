using UnityEngine;
using UnityEngine.SceneManagement;
namespace SceneHandling
{
    //Static class that encapsulates scene management duties
    public static class SceneController
    {
        public static void LoadSceneSingle(int index)
        {
            SceneManager.LoadScene(index);
        }

        public static void QuitGame()
        {
            Application.Quit();
        }
    }
}