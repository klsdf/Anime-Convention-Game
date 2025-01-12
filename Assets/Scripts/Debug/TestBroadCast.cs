using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACG
{
    public class TestBroadCast : MObject
    {
        // Start is called before the first frame update
       private void Update() {
              if (Input.GetKeyDown(KeyCode.Space))
              {
                Broadcast(EvenDefine.TestBroadcast);
              }
       }
    }
}