using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerShootTest
{
    private ShootAbility shootAbility;

    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene(0);
    }

    [UnityTest]
    public IEnumerator ShootTest()
    {
        yield return new WaitForSeconds(5);
        shootAbility = GameObject.FindObjectOfType<ShootAbility>();
        shootAbility.Execute();
        yield return new WaitForSeconds(2);
        UnityEngine.Assertions.Assert.IsTrue(shootAbility.isSuccessful);
    }
}
