using System.IO;
using UnityEngine;
using UnityEngine.UI;
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

        public void SaveLogFile()
        {
            string logDirectory = Application.persistentDataPath + "/Logs";
            Debug.Log(logDirectory);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            Logger.instance.SaveAs(logDirectory + "/game.log");
        }
    }
}


