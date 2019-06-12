using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace States
{

    class StartupJT : BaseState
    {
        
        public override void Initialize()
        {

            
            manager.PushState(new States.FetchUserJT());

        }
        public override void Resume(StateExitValue results)
        {
            Debug.Log("resume StartUp");
            if (results.sourceState == typeof(FetchUserJT))
            {
                Debug.Log("resume5 StartUp");

                // manager.SwapState(new WaitingLoadImageFB_JT(CommonDataJT.currentUser.data.PathTexture));

                if (CommonDataJT.currentUser == null)
                {
                    Debug.Log("resume4 StartUp");
                    //  If we can't fetch data, tell the user.
                    manager.PushState(new BasicDialog(StringConstantsJT.CouldNotFetchUserData));
                }
                else
                {
                    manager.PushState(new States.LoadUserListJT());
                }

            }
            if (results.sourceState == typeof(LoadUserListJT))
            {
                Debug.Log("resume StartUp LoadUserListJT");
                var resultData = results.data as LoadUserListJT.Results;
                if (resultData.wasSuccessful)
                {
                    Debug.Log("Loaded Texture Successfull");
                    Rect rec = new Rect(0, 0, CommonDataJT.TextureBelt.width, CommonDataJT.TextureBelt.height);
                    
                    CommonDataJT.prefabs.assetLookup[StringConstantsJT.Prefab_Sprite_1].GetComponent<SpriteRenderer>().sprite = Sprite.Create(CommonDataJT.TextureBelt, rec, new Vector2(0.5f, 0.5f), 100f);
                    CommonDataJT.prefabs.assetLookup[StringConstantsJT.Prefab_Sprite_2].GetComponent<SpriteRenderer>().sprite = Sprite.Create(CommonDataJT.TextureBelt, rec, new Vector2(0.5f, 0.5f), 100f);
                    CommonDataJT.prefabs.assetLookup[StringConstantsJT.Prefab_Sprite_1].AddComponent<BoxCollider2D>();
                    CommonDataJT.prefabs.assetLookup[StringConstantsJT.Prefab_Sprite_2].AddComponent<BoxCollider2D>();

                   // CommonDataJT.prefabs.assetLookup[StringConstantsJT.Prefab_Sprite_1].GetComponent<BoxCollider2D>().offset = new Vector2(0,0);
                   // CommonDataJT.prefabs.assetLookup[StringConstantsJT.Prefab_Sprite_2].GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                   // CommonDataJT.prefabs.assetLookup[StringConstantsJT.Prefab_Sprite_1].transform.localPosition = new Vector3(0, 0, 0);
                   // CommonDataJT.prefabs.assetLookup[StringConstantsJT.Prefab_Sprite_2].transform.localPosition = new Vector3(0, 0, 0);
                    CommonDataJT.prefabs.assetLookup[StringConstantsJT.Prefab_Portal].GetComponent<TestRepeated>().enabled = true;

                }
                else
                {
                    Debug.Log("No Loaded Texture");
                }
            }
            else
            {
                //throw new System.Exception("Returned from unknown state: " + results.sourceState);
            }
        }
    }
}
