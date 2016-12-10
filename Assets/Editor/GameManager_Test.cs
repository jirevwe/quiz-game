using NUnit.Framework;


namespace Tests
{
    [TestFixture]
    [Category("GameManager Tests")]
    class GameManager_Test
    {
        public int GetNextQuestionDifficulty(int currentScoreCombo, int difficulty)
        {
            var newDifficulty = currentScoreCombo + difficulty;

            if (newDifficulty == 0)     //decrease or increase
                newDifficulty = 0;
            else if (newDifficulty < 0) //reduce the difficulty
                newDifficulty = -1;
            else if (newDifficulty > 0) //increase the difficulty
                newDifficulty = 1;

            return newDifficulty;
        }

        [Test]
        [Category("Game Manager Test")]
        public void GetNextQuestionDifficulty_Increase_DIfficulty_1_Test([Values(4, 3, 2, 1)] int currCombo,
            [Values(1)] int currDifficulty, [Values(1)] int value)
        {
            Assert.AreEqual(GetNextQuestionDifficulty(currCombo, currDifficulty), value);
        }

        [Test]
        [Category("Game Manager Test")]
        public void GetNextQuestionDifficulty_Increase_DIfficulty_0_Test([Values(4, 3, 2, 1)] int currCombo,
            [Values(0)] int currDifficulty, [Values(1)] int value)
        {
            Assert.AreEqual(GetNextQuestionDifficulty(currCombo, currDifficulty), value);
        }

        [Test]
        [Category("Game Manager Test")]
        public void GetNextQuestionDifficulty_Increase_DIfficulty_Test([Values(1)] int currCombo,
            [Values(-1)] int currDifficulty, [Values(0)] int value)
        {
            Assert.AreEqual(GetNextQuestionDifficulty(currCombo, currDifficulty), value);
        }

        [Test]
        [Category("Game Manager Test")]
        public void GetNextQuestionDifficulty_Reduce_DIfficulty_Test([Values(-1, -2)] int currCombo,
            [Values(0, -1)] int currDifficulty, [Values(-1)] int value)
        {
            Assert.AreEqual(GetNextQuestionDifficulty(currCombo, currDifficulty), value);
        }


    }
}
