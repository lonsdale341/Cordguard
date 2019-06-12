using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    internal class WaitingLoadImageFB_JT : BaseState
    {

        protected bool isComplete = false;
        protected bool wasSuccessful = false;
        protected string path;
        
        protected Texture2D LoadTexture2D;
        Menus.SingleLabelGUI menuComponent;
        public WaitingLoadImageFB_JT(string path)
        {
            this.path = path;
            
            
        }
        public override void Initialize()
        {
            Debug.Log("Init WaitingLoadImageFB_JT");

            Debug.Log("path="+path);
           
            menuComponent = SpawnUI<Menus.SingleLabelGUI>(StringConstantsJT.PrefabsSingleLabelMenu);
            if (string.IsNullOrEmpty(path))
            {
                isComplete = true;
            }
            else
            {
                menuComponent.StartCoroutine(HandleResult());
            }
           
           // UpdateLabelText();
        }
        IEnumerator HandleResult()
        {
            WWW www = new WWW( path);
            yield return www;
            if (www.error == null)
            {
                LoadTexture2D = www.texture;
                wasSuccessful = true;
                Debug.Log("Successful");
            }
            else
            {
                Debug.Log("No Successful");
            }
            isComplete = true;

        }
        public override void Update()
        {
            //Debug.Log("UPDATE");
            if (isComplete)
            {
                Debug.Log("UPDATE");
                manager.PopState();
            }
            else
            {
                UpdateLabelText();
            }
        }
        private void UpdateLabelText()
        {
            // Debug.Log("UpdateLabelText WaitingForDB");
            if (menuComponent != null)
            {
                menuComponent.LabelText.text = "Loading Texture" + Utilities.StringHelper.CycleDots();
              
            }
        }
        public override StateExitValue Cleanup()
        {
            DestroyUI();
            return new StateExitValue(
              typeof(WaitingLoadImageFB_JT), new Results(LoadTexture2D, wasSuccessful));
        }
        // Class for encapsulating the results of the database load, as
        // well as information about whether the load was successful
        // or not.
        public class Results
        {

            
            public Texture2D results;
            public bool wasSuccessful;
            

            public Results( Texture2D results, bool wasSuccessful)
            {
                
                this.results = results;
                this.wasSuccessful = wasSuccessful;
                
            }
        }

    }
}