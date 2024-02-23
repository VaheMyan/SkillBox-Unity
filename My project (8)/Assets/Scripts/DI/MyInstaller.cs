using UnityEngine;
using Zenject;

public class MyInstaller : MonoInstaller
{
    [SerializeField] private bool useScriptableObjectData = true;

    private PlayerDataLoader playerDataLoader;
    private DummyDataProvider dummyDataProvider;

    public override void InstallBindings()
    {
        if (useScriptableObjectData)
        {
            var data = new Data();
            data._health = 100;
            data._shootCout = 50;

            playerDataLoader = new PlayerDataLoader(data);
            playerDataLoader.LoadData();

            Container.Bind<Data>().FromInstance(data).AsSingle();
            Container.Bind<PlayerDataLoader>().FromInstance(playerDataLoader).AsSingle();
        }
        else
        {
            dummyDataProvider = new DummyDataProvider();
            dummyDataProvider.LoadData();
            Container.Bind<DummyDataProvider>().AsSingle();
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