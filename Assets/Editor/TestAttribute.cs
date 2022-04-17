using UnityEngine;
using RemoteFileExplorer.Editor;

public class TestAttribute
{
    [CustomMenu("pull game log")]
    public static void PullLog(ManipulatorWrapper manipulator)
    {
        string remoteLogPath = manipulator.GetRemotePath("Application.persistentDataPath") + "/Logs/game.log";
        manipulator.Download(remoteLogPath, Application.dataPath.Replace("/Assets", "") + "/Logs/game.log");  // 将log文件下载到本地
    }
}