using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLaunch 
{
    public class GameLaunch : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            XLuaManager.instance.LoadScript("changetextcontent");
        }
    }
}


