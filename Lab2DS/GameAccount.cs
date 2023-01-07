using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2DS
{
    public class GameAccount
    {
        //Ім'я
        public string UserName { get; set; }
        //Рейтинг. Стандартно -- нульовий
        protected int currentRating = 0;
        public int CurrentRating
        {
            get { return currentRating; }
            set
            {
                //Рейтинг не може бути менше 0
                //Якщо отримане значення менше 0, то встановлюємо 0
                if (value < 0)
                    currentRating = 0;
                else
                    currentRating = value;
            }
        }

        //Кількість зіграних ігр. Стандартно -- нуль
        protected int gamesCount = 0;
        public int GamesCount
        {
            get { return gamesCount; }
            set
            {
                //Кількість зіграних ігр не може бути менше 0
                //Якщо отримане значення менше 0, то встановлюємо 0
                if (value < 0)
                    gamesCount = 0;
                else
                    gamesCount = value;
            }
        }
        //Список історії зіграних ігр
        protected List<GameRecord> history = new List<GameRecord>();
        public List<GameRecord> History
        {
            get { return history; }
        }
        //Тип аккаунту
        protected string accountType = "Standart";
        public string AccountType
        {
            get { return accountType; }
        }

        //Конструктор користувача
        public GameAccount(string userName)
        {
            UserName = userName;
        }

        //Виграш
        public virtual void winGame(int gameID, GameAccount opponent, Game game)
        {
            //Якщо гра на весь рейтинг, то буде своя логіка для нарахування рейтингу
            if (game.GameType == "ALL-IN-GAME")
            {
                //Зміна рейтингу і кількості ігор
                CurrentRating = CurrentRating + opponent.CurrentRating;
                gamesCount++;

                //Запис в історію
                history.Add(new GameRecord(gameID, this, opponent, game.GameType, opponent.currentRating, true));
                return;
            }

            //В інших випадках логіка однакова
            //Зміна рейтингу і кількості ігор
            CurrentRating = CurrentRating + game.Rating;
            gamesCount++;

            //Запис в історію
            history.Add(new GameRecord(gameID, this, opponent, game.GameType, game.Rating, true));
        }

        //Програш
        public virtual void loseGame(int gameID, GameAccount opponent, Game game)
        {
            //Якщо гра на весь рейтинг, то буде своя логіка для нарахування рейтингу
            if (game.GameType == "ALL-IN-GAME")
            {
                //Запис в історію
                history.Add(new GameRecord(gameID, this, opponent, game.GameType, -currentRating, false));

                //Зміна рейтингу і кількості ігор
                CurrentRating = 0;
                gamesCount++;

                return;
            }


            //Якщо гра стандартна чи тренувальна, то логіка на оба варіанти одна
            //Зміна рейтингу і кількості ігор
            CurrentRating = CurrentRating - game.Rating;
            gamesCount++;

            //Запис в історію
            history.Add(new GameRecord(gameID, this, opponent, game.GameType, -game.Rating, false));
        }

        //Виведення інфо про ігри
        public void getStats()
        {
            //Якщо не грав -- виводимо відповідне повідомлення
            if (history == null)
            {
                Console.WriteLine("Player " + UserName + " not played yet!");
                return;
            }

            //Отримуємо дані щодо ігр
            Console.WriteLine("\nPlayer " + UserName + ".\nRating: " + CurrentRating + ".\n" +
                              "Account type: " + accountType + "\nPlayed " + gamesCount + " games:");

            Console.WriteLine("\tID   \tOpponent   \tGame type   \tR. change   \tResult");
            foreach (GameRecord game in history)
            {
                //Виводимо тип гри, з ким, який рейтинг гравець отримав/втратив (рейтинг не падає нижче 0)
                Console.Write("\t" + game.ID + "\t" + game.SecondPlayer.UserName + "\t\t" + game.GameType + "\t" + game.Rating);
                if (game.IsFirstWin)
                    Console.WriteLine("\t\tWin");
                else
                    Console.WriteLine("\t\tLose");
            }
            Console.WriteLine();
        }
    }

    //Преміум акаунт. На 50% більша нагорода за перемогу, на 30% менші втрати при програші
    public class PremiumAccount : GameAccount
    {
        //Початковий рейтинг -- зразу 130. Люблю 13
        public PremiumAccount(string userName) : base(userName)
        {
            currentRating = 130;
            accountType = "Premium";
        }

        //Виграш
        public override void winGame(int gameID, GameAccount opponent, Game game)
        {
            //Якщо гра на весь рейтинг, то буде своя логіка для нарахування рейтингу
            if (game.GameType == "ALL-IN-GAME")
            {
                //Зміна рейтингу і кількості ігор з коефіцієнтом преміуму
                CurrentRating = CurrentRating + (int)(opponent.CurrentRating * 1.5);
                gamesCount++;

                //Запис в історію
                history.Add(new GameRecord(gameID, this, opponent, game.GameType, (int)(opponent.CurrentRating * 1.5), true));
                return;
            }

            //В інших випадках логіка однакова
            //Зміна рейтингу і кількості ігор з врахуванням преміуму
            CurrentRating = CurrentRating + (int)(game.Rating * 1.5);
            gamesCount++;

            //Запис в історію
            history.Add(new GameRecord(gameID, this, opponent, game.GameType, (int)(game.Rating * 1.5), true));
        }

        //Програш
        public override void loseGame(int gameID, GameAccount opponent, Game game)
        {
            //Якщо гра на весь рейтинг, то буде своя логіка для нарахування рейтингу
            //Преміумні гравці також втрачають весь рейтинг при грі на все. Бо я так хочу
            if (game.GameType == "ALL-IN-GAME")
            {
                //Запис в історію
                history.Add(new GameRecord(gameID, this, opponent, game.GameType, -currentRating, false));

                //Зміна рейтингу і кількості ігор
                CurrentRating = 0;
                gamesCount++;

                return;
            }

            //Якщо гра стандартна чи тренувальна, то логіка на оба варіанти одна
            //Зміна рейтингу і кількості ігор з урахування преміуму
            CurrentRating = CurrentRating - (int)(game.Rating * 0.7);
            gamesCount++;

            //Запис в історію
            history.Add(new GameRecord(gameID, this, opponent, game.GameType, -(int)(game.Rating * 0.7), false));
        }
    }

    //Акаунт любителя вінстріків. За кожну перемогу підряд нагорода збільшується на 10%
    public class WinStreakerAccount : GameAccount
    {
        //Початковий рейтинг -- стандартний, 0
        public WinStreakerAccount(string userName) : base(userName)
        {
            accountType = "WinStreaker";
        }

        //Виграш
        public override void winGame(int gameID, GameAccount opponent, Game game)
        {
            //Рахуємо вінстрік (кількість перемог підряд з кінця)
            int winStreak = 0;
            for (int i = history.Count - 1; i >= 0; i--)
            {
                if (history[i].FirstPlayer.UserName == UserName && history[i].IsFirstWin ||
                    history[i].SecondPlayer.UserName == UserName && !history[i].IsFirstWin)
                {
                    winStreak++;
                }
                else
                {
                    break;
                }
            }

            //Якщо гра на весь рейтинг, то буде своя логіка для нарахування рейтингу
            if (game.GameType == "ALL-IN-GAME")
            {
                //Зміна рейтингу і кількості ігор
                CurrentRating = CurrentRating + (int)(opponent.CurrentRating * (1 + 0.1 * winStreak));
                gamesCount++;

                //Запис в історію + обрахунок рейтингу в залежності від вінстріку
                history.Add(new GameRecord(gameID, this, opponent, game.GameType, (int)(opponent.CurrentRating * (1 + 0.1 * winStreak)), true));
                return;
            }

            //Для інших видів ігр
            //Зміна рейтингу і кількості ігор
            CurrentRating = CurrentRating + (int)(game.Rating * (1 + 0.1 * winStreak));
            gamesCount++;

            //Запис в історію + обрахунок рейтингу в залежності від вінстірку
            history.Add(new GameRecord(gameID, this, opponent, game.GameType, (int)(game.Rating * (1 + 0.1 * winStreak)), true));
        }

        //Програш. Тут вінстрік ніяк не впливає, тому не перевантажуємо метод
        //
    }
}