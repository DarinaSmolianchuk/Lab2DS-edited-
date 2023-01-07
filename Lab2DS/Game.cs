using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2DS
{
    public abstract class Game
    {
        public abstract string GameType { get; }
        public int Rating { get; set; }

        //Гра
        public void play(GameAccount winner, GameAccount loser)
        {
            //Не можна грати в гру на все, якщо різниця в рейтигу більша ніж в 2 рази
            if (GameType == "ALL-IN-GAME")
            {
                try
                {
                    if (winner.CurrentRating == 0 || loser.CurrentRating == 0 ||
                        winner.CurrentRating / loser.CurrentRating > 2 ||
                        loser.CurrentRating / winner.CurrentRating > 2)
                        throw new Exception("Can't play All-in game!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            //В будь-які інші ігри можна грати при будь-яких умовах
            int gameID = GameRecord.gamesCount++;
            //Запис в історію отримують оба гравця
            winner.winGame(gameID, loser, this);
            loser.loseGame(gameID, winner, this);
        }
    }

    //Стандартна гра. Гра на 26 рейтингу
    public class StandartGame : Game
    {
        public StandartGame()
        {
            Rating = 26;
        }
        public override string GameType
        {
            get { return "Standart"; }
        }
    }

    //Тренувальна гра. Без рейтингу
    public class TrainingGame : Game
    {
        public TrainingGame()
        {
            Rating = 0;
        }
        public override string GameType
        {
            get { return "Training"; }
        }
    }

    //Гра на все. Рейтинг береться з поточних рейтингів гравців, значення -1 символічне
    public class AllInGame : Game
    {
        public AllInGame()
        {
            Rating = -1;
        }
        public override string GameType
        {
            get { return "ALL-IN-GAME"; }
        }
    }
}
