using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2DS
{
    public class GameRecord
    {
        //Змінні гри
        public static int gamesCount = 0;
        public int ID { get; }
        public GameAccount FirstPlayer { get; }
        public GameAccount SecondPlayer { get; }
        public string GameType { get; }
        public int Rating { get; }
        public bool IsFirstWin { get; }

        //Конструктор
        public GameRecord(int id, GameAccount firstPlayer, GameAccount secondPlayer, string gameType, int rating, bool isFirstWin)
        {
            ID = id;
            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
            GameType = gameType;
            Rating = rating;
            IsFirstWin = isFirstWin;
        }
    }
}
