using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2DS
{
    class Program
    {
        static void Main(string[] args)
        {
            GameAccount player1 = new GameAccount("player1");
            PremiumAccount player2 = new PremiumAccount("player2");
            WinStreakerAccount player3 = new WinStreakerAccount("player3");
            GameSelector selector = new GameSelector();

            //Спроба грати на все у гравців, де у одного з них рейтинг == 0
            selector.getAllInGame().play(player1, player2);

            //Тестування гри преміумного гравця
            Console.WriteLine("Premium test: player2");
            selector.getStandartGame().play(player1, player2);
            selector.getStandartGame().play(player2, player1);
            selector.getStandartGame().play(player2, player1);
            selector.getStandartGame().play(player2, player1);
            player1.getStats();
            player2.getStats();

            //Тестування гри любителя вінстріків
            Console.WriteLine("Winstreak test: player3");
            selector.getStandartGame().play(player3, player1);
            selector.getStandartGame().play(player3, player1);
            selector.getStandartGame().play(player3, player1);
            selector.getStandartGame().play(player3, player1);
            selector.getStandartGame().play(player3, player1);
            player3.getStats();
            player1.getStats();

            //Тренувальна гра
            Console.WriteLine("Training test");
            selector.getTrainingGame().play(player3, player2);
            selector.getTrainingGame().play(player3, player2);
            player3.getStats();
            player2.getStats();

            //Гра на все
            Console.WriteLine("All-in test");
            selector.getAllInGame().play(player2, player3);
            player3.getStats();
            player2.getStats();

        }
    }
}