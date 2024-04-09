using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using System.IO;
using System.Net;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;

#endif

namespace StudioXP.Scripts.Utils
{
    [ExecuteInEditMode]
    public class SceneBuilder : MonoBehaviour
    { 
        [SerializeField] private SceneConfiguration configuration;
        
        public void Awake() 
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                //SceneManager.LoadScene()
                //Debug.Log();
            }
        }

#if UNITY_EDITOR
       // [MenuItem("Studio XP/Exporter le projet")]
        public static void ExportPackage()
        {
            //AssetDatabase.ExportPackage();
        }
        
        [MenuItem("Studio XP/Configurer la scène")]
        public static void ConfigureActiveScene()
        {
            var configuration =
                AssetDatabase.LoadAssetAtPath<SceneConfiguration>("Assets/StudioXP/Configuration/SceneConfiguration.asset");
            
            foreach (var prefab in configuration.BaseSceneObjects)
            {
                var isInstantiated = false;
                foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
                {
                    if (PrefabUtility.GetCorrespondingObjectFromSource(go) == prefab)
                        isInstantiated = true;
                }

                if (!isInstantiated)
                    PrefabUtility.InstantiatePrefab(prefab);
            }
            
            // Save scene in a new file if not done yet
            var currentScene = SceneManager.GetActiveScene();
            if (!string.IsNullOrEmpty(currentScene.path))
                return;

            var sceneFolderPath = $"{Application.dataPath}/{Game.Paths.StudentScenes}";
            var levels = 1;
            var filePaths = Directory.GetFiles(sceneFolderPath, "*.unity");
            var levelPaths = filePaths.Select(filepath => Path.GetFileNameWithoutExtension(filepath)).Where(name => name.Contains("Niveau")).ToList();
            while (levelPaths.Contains($"Niveau_{levels}"))
                levels++;
            
            var scenePath = $"Assets/{Game.Paths.StudentScenes}/Niveau_{levels}.unity";
            EditorSceneManager.SaveScene(currentScene, scenePath);
        }

        [MenuItem("Studio XP/Exporter Projet")]
        public static void ExportProject()
        {
            var flags =
                ExportPackageOptions.Default
                | ExportPackageOptions.Recurse
                | ExportPackageOptions.Interactive;

            var savePath = EditorUtility.SaveFilePanel("Sauvegarder Package Projet", "", "", "unitypackage");
            var projectContent = new string[]
            {
                "Assets",
                "ProjectSettings/AudioManager.asset",
                "ProjectSettings/EditorBuildSettings.asset",
                "ProjectSettings/EditorSettings.asset",
                "ProjectSettings/GraphicsSettings.asset",
                "ProjectSettings/InputManager.asset",
                "ProjectSettings/Physics2DSettings.asset",
                "ProjectSettings/ProjectSettings.asset",
                "ProjectSettings/QualitySettings.asset",
                "ProjectSettings/TagManager.asset",
                "ProjectSettings/TimeManager.asset"
            };

            if (!string.IsNullOrEmpty(savePath))
                AssetDatabase.ExportPackage(projectContent, savePath, flags);
        }
#endif
    }
}
