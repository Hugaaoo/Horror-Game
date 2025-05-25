using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    // SceneAsset permet de drag & drop une sc�ne depuis l'�diteur
    public SceneAsset sceneToLoad;

    public void StartGame()
    {
#if UNITY_EDITOR
        if (sceneToLoad == null)
        {
            Debug.LogWarning("Aucune sc�ne s�lectionn�e pour StartGame !");
            return;
        }
        // R�cup�re le chemin de la sc�ne
        string scenePath = AssetDatabase.GetAssetPath(sceneToLoad);
        // R�cup�re le nom de la sc�ne sans extension
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        SceneManager.LoadScene(sceneName);
#else
        if (sceneToLoad == null)
        {
            Debug.LogWarning("Aucune sc�ne s�lectionn�e pour StartGame !");
            return;
        }
        // En build, on ne peut pas utiliser AssetDatabase, on passe par le nom stock� dans string
        Debug.LogError("Le chargement de sc�ne via SceneAsset n'est pas support� en build. Utilisez une autre m�thode.");
#endif
    }

    public void QuitGame()
    {
        Debug.Log("Quitter le jeu");
        Application.Quit();
    }
}
