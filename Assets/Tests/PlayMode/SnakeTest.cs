using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SnakeTest : MonoBehaviour
{
    private Snake snake = new Snake();
    private SnakeHead snakeHead = new SnakeHead();
    private SnakeBody snakeBody = new SnakeBody();

    /// <summary>
    /// Test snake's direction on changed.
    /// </summary>
    [Test]
    public void DirectionTest()
    {
        Assert.AreEqual(snake.CurrentDirection, Vector2.up);

        snake.ChangeDirection(Vector2.right);

        Assert.AreEqual(snake.CurrentDirection, Vector2.right);

        snake.ChangeDirection(Vector2.left);

        Assert.AreNotEqual(snake.CurrentDirection, Vector2.left);
    }
}
