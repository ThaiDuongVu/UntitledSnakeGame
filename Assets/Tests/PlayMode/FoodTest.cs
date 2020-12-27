using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FoodTest
{
    private GameObject food = new GameObject();

    /// <summary>
    /// Test food position randomizing function.
    /// </summary>
    [UnityTest]
    public IEnumerator PositionTest()
    {
        food.AddComponent<Food>();
        Vector2 currentPosition = food.transform.position;

        food.GetComponent<Food>().RandomizePosition();

        Assert.AreNotEqual(currentPosition, food.transform.position);

        yield return null;
    }
}
