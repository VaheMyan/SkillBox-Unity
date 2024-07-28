using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerApplyDamageTest
{
    private ApplyDamage applyDamage;
    private CharacterHealth characterHealth;

    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene(0);
    }

    [UnityTest]
    public IEnumerator ApplyDamageTest()
    {
        yield return new WaitForSeconds(5);
        applyDamage = GameObject.FindObjectOfType<ApplyDamage>();
        characterHealth = GameObject.FindObjectOfType<CharacterHealth>();
        var startHealth = characterHealth._health;
        applyDamage.isTest = true;
        applyDamage.Execute();
        yield return new WaitForSeconds(1);
        var finishHealth = characterHealth._health;
        UnityEngine.Assertions.Assert.IsTrue(startHealth > finishHealth);
    }
}
