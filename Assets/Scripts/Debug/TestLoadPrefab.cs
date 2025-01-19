using System.Collections;
using System.Collections.Generic;
using ACG;
using UnityEngine;

public class TestLoadPrefab : MonoBehaviour
{
    GameObject testGo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
           var address = PrefabsUtil.Instance.GetPath("cow");
           AddressModel.LoadAssetAsync<GameObject>(address, (go,key) =>
           {
               Instantiate(go);
               go.name = key;
               testGo = go;
           });
        }
    }
}
