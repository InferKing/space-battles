namespace _Project.Scripts.Controller
{
    public class SceneManager
    {
        // TODO: по необходимости подписываться на событие смены сцены
        
        public void LoadScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}