﻿using CommunityToolkit.HighPerformance;

namespace AetherRadio.Numerics.Tests;

[TestFixture]
public class RecTransposeTests
{
    [Test]
    public void TransposingOneElementDoesNothing()
    {
        int[] input = { 42 };
        int[] output = new int[1];

        var A = new ReadOnlySpan2D<int>(input, 1, 1);
        var B = new Span2D<int>(output, 1, 1);

        Functions.RecTranspose<int>(A, B);

        Assert.That(input[0], Is.EqualTo(output[0]));
    }

    [Test]
    public void TransposingTwoElementsDoesNotSwapThem()
    {
        int[] input = { 42, 43 };
        int[] output = new int[2];

        var A = new ReadOnlySpan2D<int>(input, 1, 2);
        var B = new Span2D<int>(output, 2, 1);

        Functions.RecTranspose<int>(A, B);

        Assert.Multiple(() =>
        {
            Assert.That(input[0], Is.EqualTo(output[0]));
            Assert.That(input[1], Is.EqualTo(output[1]));
        });
    }

    [Test]
    public void TransposingADiagonalMatrixDoesNothing()
    {
        int[,] input = { { 42, 0 }, { 0, 43 } };
        int[,] output = new int[2, 2];

        var A = new ReadOnlySpan2D<int>(input);
        var B = new Span2D<int>(output);

        Functions.RecTranspose<int>(A, B);

        Assert.Multiple(() =>
        {
            Assert.That(input[0, 0], Is.EqualTo(output[0, 0]));
            Assert.That(input[0, 1], Is.EqualTo(output[0, 1]));
            Assert.That(input[1, 0], Is.EqualTo(output[1, 0]));
            Assert.That(input[1, 1], Is.EqualTo(output[1, 1]));
        });
    }

    [Test]
    public void TransposingA4by4NonDiagonalMatrix()
    {
        int[,] input = { { 11, 22 }, { 33, 44 } };
        int[,] output = new int[2, 2];
        int[,] expected = { { 11, 33 }, { 22, 44 } };

        var A = new ReadOnlySpan2D<int>(input);
        var B = new Span2D<int>(output);

        Functions.RecTranspose<int>(A, B);

        Assert.Multiple(() =>
        {
            Assert.That(output[0, 0], Is.EqualTo(expected[0, 0]));
            Assert.That(output[0, 1], Is.EqualTo(expected[0, 1]));
            Assert.That(output[1, 0], Is.EqualTo(expected[1, 0]));
            Assert.That(output[1, 1], Is.EqualTo(expected[1, 1]));
        });
    }

    [Test]
    public void TransposingANonSquareMatrixChagesTheShape()
    {
        int[,] input = { { 11, 22, 33 }, { 44, 55, 66 } };
        int[,] output = new int[3, 2];
        int[,] expected = { { 11, 44 }, { 22, 55 }, { 33, 66 } };

        var A = new ReadOnlySpan2D<int>(input);
        var B = new Span2D<int>(output);

        Functions.RecTranspose<int>(A, B);

        Assert.Multiple(() =>
        {
            Assert.That(output[0, 0], Is.EqualTo(expected[0, 0]));
            Assert.That(output[0, 1], Is.EqualTo(expected[0, 1]));
            Assert.That(output[1, 0], Is.EqualTo(expected[1, 0]));
            Assert.That(output[1, 1], Is.EqualTo(expected[1, 1]));
            Assert.That(output[2, 0], Is.EqualTo(expected[2, 0]));
            Assert.That(output[2, 1], Is.EqualTo(expected[2, 1]));
        });
    }
}
