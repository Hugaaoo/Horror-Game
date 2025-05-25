using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    // SceneAsset permet de drag & drop une scène depuis l'éditeur
    public SceneAsset sceneToLoad;

    public void StartGame()
    {
#if UNITY_EDITOR
        if (sceneToLoad == null)
        {
            Debug.LogWarning("Aucune scène sélectionnée pour StartGame !");
            return;
        }
        // Récupère le chemin de la scène
        string scenePath = AssetDatabase.GetAssetPath(sceneToLoad);
        // Récupère le nom de la scène sans extension
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        SceneManager.LoadScene(sceneName);
#else
        if (sceneToLoad == null)
        {
            Debug.LogWarning("Aucune scène sélectionnée pour StartGame !");
            return;
        }
        // En build, on ne peut pas utiliser AssetDatabase, on passe par le nom stocké dans string
        Debug.LogError("Le chargement de scène via SceneAsset n'est pas supporté en build. Utilisez une autre méthode.");
#endif
    }

    public void QuitGame()
    {
        Debug.Log("Quitter le jeu");
        Application.Quit();
    }
}
