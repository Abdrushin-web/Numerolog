using Numerology;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace NumerologyTests
{
    public class AlphabetTests
    {
        [Fact]
        public void ComputeLetters()
        {
            var result = Alphabets.German.ComputeLetters("Abdruschin");
            AssertResult(result, 356, 5);
        }

        [Fact]
        public void ComputeLinesAndWords()
        {
            var result = Alphabets.German.ComputeLinesAndWords("Imanuel\nParzival\nAbdruschin");
            AssertResult(result, 869, 5);
            Assert.NotNull(result.Source);
            Assert.Equal(3, result.Source.Count);
            AssertResult(result.Source[0], 735, 6);
            AssertResult(result.Source[1], 777, 3);
            AssertResult(result.Source[2], 356, 5);
        }

        [Fact]
        public void JoinByHasNumber()
        {
            var result = Alphabets.German.ComputeLetters("1. Was sucht Ihr?");
            AssertResult(result, 674, 8);
            var parts = result.Letters.JoinByHasNumber().ToArray();
            Assert.NotNull(parts);
            Assert.Equal(7, parts.Length);
            AssertResult(parts[0], "1. ", false);
            AssertResult(parts[1], "Was", true);
            AssertResult(parts[2], " ", false);
            AssertResult(parts[3], "sucht", true);
            AssertResult(parts[4], " ", false);
            AssertResult(parts[5], "Ihr", true);
            AssertResult(parts[6], "?", false);
        }

        private void AssertResult([NotNull] TextNumber? result, ushort value, byte singleDigitSum)
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Number);
            Assert.Equal(value, result.Number.Value);
            Assert.Equal(singleDigitSum, result.Number.SingleDigitSum);
        }

        private void AssertResult([NotNull] TextNumber? result, string text, bool hasNumber)
        {
            Assert.NotNull(result);
            Assert.Equal(text, result.Text);
            Assert.Equal(hasNumber, result.HasNumber);
        }
    }
}