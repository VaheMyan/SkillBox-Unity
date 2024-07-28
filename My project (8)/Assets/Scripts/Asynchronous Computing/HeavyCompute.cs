using UnityEngine;
using System.Threading.Tasks;
using Unity.Collections;
using System.Threading;
using Unity.Jobs;
using Unity.Burst;
using UnityEngine.Jobs;
using UnityEngine.InputSystem;
using UniRx;
using System;
using System.IO;

public class HeavyCompute : MonoBehaviour
{
    private string configFilePath = "Assets/Resources/File.txt";
    public int health;

    async void Start()
    {
        await LoadConfigurationAsync();
    }

    private async Task LoadConfigurationAsync()
    {
        if (!File.Exists(configFilePath))
        {
            Debug.LogError("File config not found: " + configFilePath);
            return;
        }

        string textContent;
        using (StreamReader reader = new StreamReader(configFilePath))
        {
            textContent = await reader.ReadToEndAsync();
        }

        if (!string.IsNullOrEmpty(textContent))
        {
            if (int.TryParse(textContent, out int healthValue))
            {
                health = healthValue;
                Debug.Log("Health: " + health);
            }
            else
            {
                Debug.LogError("Failed to parse health value from config file.");
            }
        }
        else
        {
            Debug.LogError("Failed to load configuration.");
        }
    }


    public void HeavyMethod()
    {
        //var heavyMethod = Observable.Start(() =>
        //{
        //    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1)); // dadarecnum e hosqy 1 vayrkyan
        //    Debug.Log("Finish 1");
        //    return "Heavy Method 1";
        //});
        //var heavyMethod2 = Observable.Start(() =>
        //{
        //    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3)); // dadarecnum e hosqy 3 vayrkyan
        //    Debug.Log("Finish 2");
        //    return "Heavy Method 2";
        //});

        //// spasum e aynqan jamanak minchev very nshvatsnery katarven
        //// ev hetevum enq dranc ashqatanqi ardzyunqin
        //Observable.WhenAll(heavyMethod, heavyMethod2)
        //    .ObserveOnMainThread()
        //    .Subscribe(xs =>
        //    {
        //        Debug.Log(xs[0]);
        //        Debug.Log(xs[1]);
        //    });

        // *****

        // Click 2 and mor
        //var clickStream = Observable.EveryUpdate()
        //    .Where(_ => Mouse.current.leftButton.wasPressedThisFrame);

        //clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(250)))
        //    .Where(xs => xs.Count >= 2)
        //    .Subscribe(xs => Debug.Log("DoubleClick Detected! Count: " + xs.Count));
    }

    //private NativeArray<float> result;
    //private NativeArray<float> input;
    //private HeavyJob jobData;
    //public void HeavyMethod1()
    //{
    //    result = new NativeArray<float>(10, Allocator.TempJob);
    //    input = new NativeArray<float>(10, Allocator.TempJob);

    //    for (var i = 0; i < result.Length; i++)
    //    {
    //        input[i] = i + 10;
    //    }
    //    jobData = new HeavyJob
    //    {
    //        number = input,
    //        result = result
    //    };
    //    JobHandle handle = jobData.Schedule(result.Length, 1);
    //    handle.Complete();
    //    foreach (var t in result)
    //    {
    //        Debug.Log(t);
    //    }
    //    input.Dispose();
    //    result.Dispose();
    //}
}
//[BurstCompile] // sksum a arag ashxatel
//public struct HeavyJob : IJobParallelFor
//{
//    public NativeArray<float> result;

//    [ReadOnly] public NativeArray<float> number;
//    public void Execute(int index)
//    {
//        result[index] = number[index];
//    }

//    //public void Execute(int index, TransformAccess transform)
//    //{

//    //}
//}