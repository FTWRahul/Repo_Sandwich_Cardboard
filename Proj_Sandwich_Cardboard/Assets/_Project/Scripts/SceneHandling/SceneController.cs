using UnityEngine;
using UnityEngine.SceneManagement;
namespace SceneHandling
{
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