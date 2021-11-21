using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using XLua;

public class XLuaManager : MonoBehaviour
{

    private static XLuaManager s_Instance;
    public static XLuaManager instance 
    {
        get 
        {
            if(s_Instance == null)
            {
                s_Instance = FindObjectOfType<XLuaManager>();
            }
            if(s_Instance == null){
                GameObject obj = new GameObject(typeof(XLuaManager).Name);
                s_Instance = obj.AddComponent<XLuaManager>();
            }
            return s_Instance;
        }
    }

    LuaEnv luaEnv = null;

    void Awake()
    {
        InitLuaEnv();
    }

    public void InitLuaEnv(LuaEnv.CustomLoader loader = null)
    {
        if(luaEnv != null) return;
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(loader ?? DefaultCustomLoader);
    }

    float tickInterval = 10;
    float lastTickTime = 0;
    int fullGcFrameCount = 100;
    void Update()
    {
        if (luaEnv != null)
        {
            if (Time.time - lastTickTime > tickInterval)
            {
                luaEnv.Tick();
                lastTickTime = Time.time;
            }
            if (Time.frameCount % fullGcFrameCount == 0)
            {
                luaEnv.FullGc();
            }
        }
    }

    public object[] DoString(string scriptContent, string chunkName = "chunk", LuaTable env = null)
    {
        if(luaEnv == null)
        {
            InitLuaEnv();
        }
        try
        {
            return luaEnv.DoString(scriptContent, chunkName, env);
        }
        catch (System.Exception ex)
        {
            string msg = string.Format("XLua DoString exception : {0}\n{1}", ex.Message, ex.StackTrace);
            Debug.LogError(msg);
        }
        return null;
    }

    public object[] LoadScript(string scriptName, string chunkName = "chunk", LuaTable env = null)
    {
        return DoString(string.Format("return require('{0}')", scriptName), chunkName, env);
    }

    public object[] ReloadScript(string scriptName, string chunkName = "chunk", LuaTable env = null)
    {
        DoString(string.Format("package.loaded['{0}'] = nil", scriptName));
        return LoadScript(scriptName, chunkName, env);
    }

    public LuaEnv GetLuaEnv()
    {
        return luaEnv;
    }

    public void DisposeLuaEnv()
    {
        if (luaEnv != null)
        {
            try
            {
                luaEnv.Dispose();
                luaEnv = null;
            }
            catch (System.Exception ex)
            {
                string msg = string.Format("XLua Dispose exception : {0}\n{1}", ex.Message, ex.StackTrace);
                Debug.LogError(msg);
            }
        }
    }

    public static byte[] DefaultCustomLoader(ref string luaPath)
    {
        string scriptPath = string.Empty;
        string filepath = string.Empty;
        filepath = luaPath.Replace(".", "/") + ".lua";
        scriptPath = System.IO.Path.Combine(Application.persistentDataPath, filepath);
        if(File.Exists(scriptPath))
        {
            return File.ReadAllBytes(scriptPath);
        }
        else
        {
            scriptPath = System.IO.Path.Combine(Application.streamingAssetsPath, filepath);
            UnityWebRequest request = UnityWebRequest.Get(scriptPath);
            request.SendWebRequest();
            while(true)
            {
                if(request.isNetworkError || request.isHttpError)
                {
                    Debug.LogError("not find " + scriptPath);
                    return null;
                }
                if(request.downloadHandler.isDone)
                {
                    return request.downloadHandler.data;
                }
            }
        }
    }

    void OnDestroy()
    {
        DisposeLuaEnv();
    }
}
