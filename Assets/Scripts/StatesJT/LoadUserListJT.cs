using System.Collections;
using System.Collections.Generic;
using Menus;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

namespace States
{
    internal class LoadUserListJT : BaseState
    {
        protected bool wasSuccessful = false;

        public override void Initialize()
        {


            Debug.Log("LoadUserListJT");
            manager.PushState(new WaitingLoadImageFB_JT(CommonDataJT.currentUser.data.PathTexture));

        }
        public override void Suspend()
        {

            HideUI();
        }

        public override StateExitValue Cleanup()
        {

            DestroyUI();
            return new StateExitValue(
              typeof(LoadUserListJT), new Results(wasSuccessful));
        }
        public class Results
        {


            
            public bool wasSuccessful;


            public Results( bool wasSuccessful)
            {

                Debug.Log("Results Constract");
                this.wasSuccessful = wasSuccessful;

            }
        }
        public override void Resume(StateExitValue results)
        {
            ShowUI();
            if (results.sourceState == typeof(WaitingLoadImageFB_JT))
            {
                Debug.Log("Resume LoadUserListJT");
                var resultData = results.data as WaitingLoadImageFB_JT.Results;
                if (resultData.wasSuccessful)
                {
                    Debug.Log("Resume LoadUserListJT___2");
                    wasSuccessful = true;
                    CommonDataJT.TextureBelt = resultData.results;
                }

            }
            manager.PopState();
        }
        public override void HandleUIEvent(GameObject source, object eventData)
        {



        }

    }
}
