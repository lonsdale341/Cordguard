using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetIPadress : MonoBehaviour
{
    public InputField IP_adress;
    public InputField port;
    public GameObject BUtton_Next;
    // Use this for initialization
    void Start()
    {
        BUtton_Next.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!string.IsNullOrEmpty(IP_adress.text) && !string.IsNullOrEmpty(port.text))
        {
            BUtton_Next.SetActive(true);
        }
    }
    public void SetScene()
    {
        CommonData.IP_adress = IP_adress.text;
        CommonData.port =int.Parse( port.text);
        SceneManager.LoadScene("Marker");
    }
}
