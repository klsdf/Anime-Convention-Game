using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACG
{
    public class TestMObject : MObject
    {
        // Start is called before the first frame update
    
        void Start()
        {
            SubMsg();
        }
        void SubMsg()
        {
            #region 反注册
            UnRegistListener(EvenDefine.TestBroadcast, HandleTestBroadcast);
            #endregion
    
            #region 注册
            RegistListener(EvenDefine.TestBroadcast, HandleTestBroadcast);
            #endregion
        }
    
        void HandleTestBroadcast()
        {
            Debug.Log("TestBroadcast");
        }
    }
}