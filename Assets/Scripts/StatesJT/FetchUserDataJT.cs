using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{

    class FetchUserJT : BaseState
    {




        public FetchUserJT()
        {

        }
        public override void Initialize()
        {
            Debug.Log("Init FetchUserJT");
            manager.PushState(
              new WaitingForDBLoadJT<UserJT>(CommonDataJT.DBUserTablePath));
        }
        public override StateExitValue Cleanup()
        {
            return new StateExitValue(typeof(FetchUserJT), null);
        }
        public override void Resume(StateExitValue results)
        {
            Debug.Log("Resume FetchUserJT");
            if (results != null)
            {
                Debug.Log("Resume FetchUserJT__2");
                if (results.sourceState == typeof(WaitingForDBLoadJT<UserJT>))
                {
                    Debug.Log("Resume FetchUserJT__3");
                    var resultData = results.data as WaitingForDBLoadJT<UserJT>.Results;
                    if (resultData.wasSuccessful)
                    {
                        if (resultData.results != null)
                        {
                            // Got some results back!  Use this data.
                            CommonDataJT.currentUser = new DBStruct<UserJT>(
                                    CommonDataJT.DBUserTablePath, CommonDataJT.app);
                            CommonDataJT.currentUser.Initialize(resultData.results);

                            Debug.Log("Fetched user ");
                        }

                    }
                    else
                    {
                        // Can't fetch data.  Assume internet problems, stay offline.
                        // CommonDataJT.currentUser = null;
                        // Make a new user, using default credentials.
                        Debug.Log("Could not find user " + " - Creating new profile.");
                        UserJT temp = new UserJT();

                        CommonDataJT.currentUser = new DBStruct<UserJT>(
                                 CommonDataJT.DBUserTablePath, CommonDataJT.app);
                        CommonDataJT.currentUser.Initialize(temp);
                        CommonDataJT.currentUser.PushData();
                    }
                }
            }
            // Whether we successfully fetched, or had to make a new user,
            // return control to the calling state.
            manager.PopState();
        }
    }
}
