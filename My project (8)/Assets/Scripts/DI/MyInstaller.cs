using UnityEngine;
using Zenject;

public class MyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<string>().FromInstance("INJECT");
        Container.Bind<GreetMe>().AsSingle().NonLazy();
        Container.Bind<ITest>().To<Test1>().AsSingle().NonLazy();
    }
}

public class GreetMe
{
    public GreetMe(string message)
    {
        Debug.Log(message);
    }
}

public class Test1 : ITest
{
    public void Echo()
    {
        Debug.Log("Test1");
    }
}
public class Test2 : ITest
{
    public void Echo()
    {
        Debug.Log("Test2");
    }
}
public interface ITest
{
    void Echo();
}