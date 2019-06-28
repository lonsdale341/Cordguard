using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonDataJT  {
    public static PrefabListJT prefabs;
    public static Firebase.FirebaseApp app;
    public static MainManagerJT mainManager;
    public static GameObject canvasHolder;
    public const string DBUserTablePath = "DB_Cordguard/";
    public static string User_ID ;
    public static DBStruct<UserJT> currentUser;

    public static TestRepeated ControllerWindow;
    public static Texture2D TextureBelt;

    public static void ChangeSizePosition()
    {
        if(ControllerWindow)
        {
            ControllerWindow.ChangeSizePosition();
        }
    }
}
