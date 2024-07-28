using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerChangeMaterialTest
{
    private ChangeAbility changeAbility;

    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene(0);
    }

    [UnityTest]
    public IEnumerator ChangeMaterialTest()
    {
        yield return new WaitForSeconds(5);
        changeAbility = GameObject.FindObjectOfType<ChangeAbility>();
        var startState = changeAbility.dissolve;
        changeAbility.isDissolved = true;
        Debug.Log(changeAbility._changeMaterialInput);
        yield return new WaitForSeconds(1);
        changeAbility.isDissolved = false;
        var finishState = changeAbility.dissolve;
        UnityEngine.Assertions.Assert.IsTrue(finishState > startState);

    }
}
