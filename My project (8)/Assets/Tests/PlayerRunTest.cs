using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerRunTest
{
    private UserInputData userInputData;

    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene(0);
    }

    [UnityTest]
    public IEnumerator RunTest()
    {
        yield return new WaitForSeconds(5);
        userInputData = GameObject.FindObjectOfType<UserInputData>();
        var startsSpeed = userInputData.speed;
        userInputData.isTestRun = true;
        yield return new WaitForSeconds(1);
        var finishSpeed = userInputData.speed;
        UnityEngine.Assertions.Assert.IsTrue(finishSpeed > startsSpeed);
    }
}
