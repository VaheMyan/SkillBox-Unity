using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerMoveTest
{
    private DogMoveComponent dogMoveComponent;

    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene(0);
    }

    [UnityTest]
    public IEnumerator MoveTest()
    {
        yield return new WaitForSeconds(5);
        dogMoveComponent = GameObject.FindObjectOfType<DogMoveComponent>();
        var startPosition = dogMoveComponent.gameObject.transform.position;
        dogMoveComponent.isTest = true;
        yield return new WaitForSeconds(0.5f);
        var finishPosition = dogMoveComponent.gameObject.transform.position;
        dogMoveComponent.isTest = false;
        UnityEngine.Assertions.Assert.IsTrue(
            finishPosition.x > startPosition.x || finishPosition.x < startPosition.x
            &&
            finishPosition.z > startPosition.z || finishPosition.z < startPosition.z);
    }
}
