using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CodeLoader : IDisposable
{
    public List<Type> hotfixTypes = new List<Type>();
    public static CodeLoader Instance = new CodeLoader();
    private ILRuntime.Runtime.Enviorment.AppDomain appDomain;
    private MemoryStream fs;
    private MemoryStream p;
    
    public Action Update;
    public Action FixedUpdate;
    public Action LateUpdate;
    public Action OnApplicationQuit;
    
    public void Start() {
        ILRuntimeLoad();
    }

    private void ILRuntimeLoad() {
        appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();
#if UNITY_EDITOR
        appDomain.DebugService.StartDebugService(56000);
#endif
        byte[] dll = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "HotFix_Project.dll"));
        fs = new MemoryStream(dll);
        byte[] pdb = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "HotFix_Project.pdb"));
        p = new MemoryStream(pdb);
        appDomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());

        Type[] types = appDomain.LoadedTypes.Values.Select(x => x.ReflectionType).ToArray();
        hotfixTypes.Clear();
        foreach (Type type in types)
        {
            hotfixTypes.Add(type);
        }
        
        InitializeILRuntime();
        //Run();
    }

    public void Run() {
        appDomain.Invoke("HotFix_Project.Entry", "Start", null);
    }
    
    private void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        appDomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //这里做一些ILRuntime的注册
    }
    
    public void Dispose()
    {
        this.appDomain?.Dispose();
    }
}
