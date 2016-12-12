using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    [TestFixture]
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

        [Test]
        [Category("Game Manager Test")]
        public void GetQuestionsTest_Get_Question_Count()
        {
            string data = Resources.Load<TextAsset>("data").text; //use dummy data

            List<Question> questions = new List<Question>();

            JSONObject json = JSONObject.Create(data);

            //get the questions
            json = json["questions"];

            for (int i = 0; i<json.list.Count; i++)
            {
                Question ques = new Question();

                ques.question = json[i]["question"].str;
                ques.answerIndex = (int)json[i]["answer"].f;
                ques.difficulty = (int)json[i]["difficulty"].f;

                for (int j = 0; j<json[i]["options"].list.Count; j++)
                {
                    ques.options.Add(json[i]["options"].list[j].str);
                }

                questions.Add(ques);
            }

            Assert.AreEqual(questions.Count, 5);
        }

        [Test]
        [Category("Game Manager Test")]
        public void GetQuestionsTest_Get_Question()
        {
            string data = Resources.Load<TextAsset>("data").text; //use dummy data

            List<Question> questions = new List<Question>();

            JSONObject json = JSONObject.Create(data);

            //get the questions
            json = json["questions"];

            for (int i = 0; i < json.list.Count; i++)
            {
                Question ques = new Question();

                ques.question = json[i]["question"].str;
                ques.answerIndex = (int)json[i]["answer"].f;
                ques.difficulty = (int)json[i]["difficulty"].f;

                for (int j = 0; j < json[i]["options"].list.Count; j++)
                {
                    ques.options.Add(json[i]["options"].list[j].str);
                }

                questions.Add(ques);
            }

            Question testQuestion = questions[0];
            Assert.AreEqual(testQuestion.question, "What is the game about");
        }

        [Test]
        [Category("Game Manager Test")]
        public void GetQuestionsTest_Get_Question_Options()
        {
            string data = Resources.Load<TextAsset>("data").text; //use dummy data

            List<Question> questions = new List<Question>();

            JSONObject json = JSONObject.Create(data);

            //get the questions
            json = json["questions"];

            for (int i = 0; i < json.list.Count; i++)
            {
                Question ques = new Question();

                ques.question = json[i]["question"].str;
                ques.answerIndex = (int)json[i]["answer"].f;
                ques.difficulty = (int)json[i]["difficulty"].f;

                for (int j = 0; j < json[i]["options"].list.Count; j++)
                {
                    ques.options.Add(json[i]["options"].list[j].str);
                }

                questions.Add(ques);
            }

            Question testQuestion = questions[1];
            Assert.AreEqual(testQuestion.options[0], "a");
        }

        [Test]
        [Category("Game Manager Test")]
        public void GetQuestionsTest_Get_Difficulty()
        {
            string data = Resources.Load<TextAsset>("data").text; //use dummy data

            JSONObject json = JSONObject.Create(data);

            //get the difficulty
            int difficulty = (int)json["game"]["difficulty"].f;

            Assert.AreEqual(difficulty, 1);
        }

        [Test]
        [Category("Game Manager Test")]
        public void GetQuestionsTest_Get_HighScore()
        {
            string data = Resources.Load<TextAsset>("data").text; //use dummy data

            JSONObject json = JSONObject.Create(data);

            //get the highscore
            int highScore = (int)json["game"]["highscore"].f;

            Assert.AreEqual(highScore, 166);
        }

        [Test]
        [Category("Game Manager Test")]
        public void GetNextQuestionTest_Easy_Difficulty()
        {
            string data = Resources.Load<TextAsset>("data").text; //use dummy data

            List<Question> questions = new List<Question>();

            JSONObject json = JSONObject.Create(data);

            //get the questions
            json = json["questions"];

            for (int i = 0; i < json.list.Count; i++)
            {
                Question ques = new Question();

                ques.question = json[i]["question"].str;
                ques.answerIndex = (int)json[i]["answer"].f;
                ques.difficulty = (int)json[i]["difficulty"].f;

                for (int j = 0; j < json[i]["options"].list.Count; j++)
                {
                    ques.options.Add(json[i]["options"].list[j].str);
                }

                questions.Add(ques);
            }

            //get the next question using the new difficulty
            List<Question> questionsForDifficulty = questions.FindAll(n => n.difficulty == -1);
            Question currentQuestion = questionsForDifficulty[0];

            Assert.AreEqual(currentQuestion.question, "How many numbers are in 1111111");
        }

        [Test]
        [Category("Game Manager Test")]
        public void GetNextQuestionTest_Normal_Difficulty()
        {
            string data = Resources.Load<TextAsset>("data").text; //use dummy data

            List<Question> questions = new List<Question>();

            JSONObject json = JSONObject.Create(data);

            //get the questions
            json = json["questions"];

            for (int i = 0; i < json.list.Count; i++)
            {
                Question ques = new Question();

                ques.question = json[i]["question"].str;
                ques.answerIndex = (int)json[i]["answer"].f;
                ques.difficulty = (int)json[i]["difficulty"].f;

                for (int j = 0; j < json[i]["options"].list.Count; j++)
                {
                    ques.options.Add(json[i]["options"].list[j].str);
                }

                questions.Add(ques);
            }

            //get the next question using the new difficulty
            List<Question> questionsForDifficulty = questions.FindAll(n => n.difficulty == 0);
            Question currentQuestion = questionsForDifficulty[0];

            Assert.AreEqual(currentQuestion.question, "What is the game about");
        }

        [Test]
        [Category("Game Manager Test")]
        public void GetNextQuestionTest_Hard_Difficulty()
        {
            string data = Resources.Load<TextAsset>("data").text; //use dummy data

            List<Question> questions = new List<Question>();

            JSONObject json = JSONObject.Create(data);

            //get the questions
            json = json["questions"];

            for (int i = 0; i < json.list.Count; i++)
            {
                Question ques = new Question();

                ques.question = json[i]["question"].str;
                ques.answerIndex = (int)json[i]["answer"].f;
                ques.difficulty = (int)json[i]["difficulty"].f;

                for (int j = 0; j < json[i]["options"].list.Count; j++)
                {
                    ques.options.Add(json[i]["options"].list[j].str);
                }

                questions.Add(ques);
            }

            //get the next question using the new difficulty
            List<Question> questionsForDifficulty = questions.FindAll(n => n.difficulty == 1);
            Question currentQuestion = questionsForDifficulty[0];

            Assert.AreEqual(currentQuestion.question, "what is the first letter of the alphabet");
        }
    }
}
