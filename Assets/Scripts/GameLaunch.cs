using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logger = Litchi.Logger;

namespace GameLaunch 
{
    public class GameLaunch : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // 准备生成用于拉取的日志文件
            Logger.instance.Init();
            Logger.Info("[GameLaunch] start game...");

            XLuaManager.instance.LoadScript("changetextcontent");
            Debug.Log("[GameLaunch] load changetextcontent finish...");
        }
    }
}


