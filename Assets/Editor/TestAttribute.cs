using UnityEngine;
using RemoteFileExplorer.Editor;

public class TestAttribute
{
    [CustomMenu("pull game log")]
    public static void PullLog(ManipulatorWrapper manipulator)
    {
        manipulator.Download("game.log", Application.dataPath.Replace("/Assets", "") + "RemoteLogs/game.log");  // 将log文件下载到本地
    }
}