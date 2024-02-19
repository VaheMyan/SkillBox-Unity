using UnityEngine;
using Zenject;

public class MyInstaller : MonoInstaller
{
    [SerializeField] private bool useScriptableObjectData = true;
    DummyDataProvider dummyDataProvider;
    PlayerDataLoader playerDataLoader;

    public override void InstallBindings()
    {
        if (useScriptableObjectData == true)
        {
            playerDataLoader.LoadData();

            Container.Bind<Data>().FromResources("PlayerData").AsSingle();
            Container.Bind<PlayerDataLoader>().AsSingle();
            //Container.Bind<IDataProvider>().To<PlayerDataLoader>().AsSingle();
        }
        else
        {
            dummyDataProvider.LoadData();
            Container.Bind<DummyDataProvider>().AsSingle();
            //Container.Bind<DummyDataProvider>().AsSingle();
            //Container.Bind<IDataProvider>().To<DummyDataProvider>().AsSingle();
        }

        //Container.Bind<string>().FromInstance("INJECT");
        //Container.Bind<GreetMe>().AsSingle().NonLazy();
        //Container.Bind<ITest>().To<Test1>().AsSingle().NonLazy();
    }
}

//public class GreetMe
//{
//    public GreetMe(string message)
//    {
//        Debug.Log(message);
//    }
//}
//public class Test1 : ITest
//{
//    public void Echo()
//    {
//        Debug.Log("Test1");
//    }
//}
//public class Test2 : ITest
//{
//    public void Echo()
//    {
//        Debug.Log("Test2");
//    }
//}
//public interface ITest
//{
//    void Echo();
//}