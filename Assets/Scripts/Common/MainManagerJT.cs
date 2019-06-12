using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Unity.Editor;

public class MainManagerJT : MonoBehaviour
{
   
    [HideInInspector]
    public States.StateManager stateManager = new States.StateManager();
    // Use this for initialization
    void Start()
    {
        InitializeFirebaseAndStart();
    }

    // Update is called once per frame
    void Update()
    {
        stateManager.Update();
    }
    void FixedUpdate()
    {
        stateManager.FixedUpdate();
    }
    void InitializeFirebaseAndStart()
    {
        Firebase.DependencyStatus dependencyStatus = Firebase.FirebaseApp.CheckDependencies();

        if (dependencyStatus != Firebase.DependencyStatus.Available)
        {
            Firebase.FirebaseApp.FixDependenciesAsync().ContinueWith(task =>
            {
                dependencyStatus = Firebase.FirebaseApp.CheckDependencies();
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    StartGame();
                }
                else
                {
                    Debug.LogError(
                        "Could not resolve all Firebase dependencies: " + dependencyStatus);
                    Application.Quit();
                }
            });
        }
        else
        {
            StartGame();
        }
    }
    void StartGame()
    {
        
        CommonDataJT.prefabs = FindObjectOfType<PrefabListJT>();
        CommonDataJT.canvasHolder = GameObject.Find("CanvasHolder");
        CommonDataJT.mainManager = this;
        Firebase.AppOptions ops = new Firebase.AppOptions();
        CommonDataJT.app = Firebase.FirebaseApp.Create(ops);
        CommonDataJT.app.SetEditorDatabaseUrl(StringConstantsJT.LinkFirebaseDB);

        //  Screen.orientation = ScreenOrientation.Landscape;





        //stateManager.PushState(new States.Startup());
        stateManager.PushState(new States.StartupJT());
    }
}
