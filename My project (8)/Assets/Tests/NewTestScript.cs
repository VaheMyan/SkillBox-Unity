using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class NewTestScript
{
    CharacterHealth characterHealth;

    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(0);
    }

    [UnityTest]
    public IEnumerator PlayerLifeCycleTest() //Spawn Test
    {
        //yield return new WaitForSeconds(5);
        //characterHealth = GameObject.FindObjectOfType<CharacterHealth>();
        //UnityEngine.Assertions.Assert.IsNotNull(characterHealth);

        //characterHealth.Health = 0;
        yield return new WaitForSeconds(4);
        //UnityEngine.Assertions.Assert.IsNull(characterHealth);
    }

    [TearDown]
    public void TearDown()
    {

    }
}
