using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UNO
{
    class Program
    {
        static Cards[] player1card = new Cards[8];
        static Cards[] player2card = new Cards[8];
        static Cards[] player3card = new Cards[8];
        static Cards[] player4card = new Cards[8];

        static int CardsPlayer1Counter = 8;
        static int CardsPlayer2Counter = 8;
        static int CardsPlayer3Counter = 8;
        static int CardsPlayer4Counter = 8;
        static string floorColor = string.Empty;
        static string floorType = string.Empty;


        static void shuffleCards()

        {
            List<string> cardTypeList = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "+2", "+4", "Change Color", "Change Order" };
            List<string> cardColorList = new List<string> { "R", "B", "G", "Y" };

            Random t = new Random();
            floorType = cardTypeList[t.Next(cardTypeList.Count - 4)];
            floorColor = cardColorList[t.Next(cardColorList.Count)];

            for (int i = 0; i <= CardsPlayer1Counter - 1; i++)
            {
                player1card[i] = new Cards(cardTypeList[t.Next(cardTypeList.Count)], cardColorList[t.Next(cardColorList.Count)]);
                player2card[i] = new Cards(cardTypeList[t.Next(cardTypeList.Count)], cardColorList[t.Next(cardColorList.Count)]);
                player3card[i] = new Cards(cardTypeList[t.Next(cardTypeList.Count)], cardColorList[t.Next(cardColorList.Count)]);
                player4card[i] = new Cards(cardTypeList[t.Next(cardTypeList.Count)], cardColorList[t.Next(cardColorList.Count)]);

            }
            Console.WriteLine("Shuffling Cards");
            using (var progress = new ProgressBar())
            {
                for (int i = 0; i <= 100; i++)
                {
                    progress.Report((double)i / 100);
                    Thread.Sleep(20);
                }
            }
            Console.Clear();



        }
        static void wildCard(string type)
        {
            switch (type)
            {
                case "+4":
                    break;
                case "+2":
                    break;
                case "Change Color":
                    break;
                case "Change Order":
                    break;

            }
        }
        static void showcards(int counter)
        {
            string output = string.Empty;

            Console.WriteLine();
            Console.WriteLine("{0,-20} {1,80} {2,119} {3,119} {4,119}", "Your Cards are:", string.Format("Player 1 Cards:{0}", CardsPlayer1Counter), string.Format("Player 2 Cards:{0}", CardsPlayer2Counter), string.Format("Player 3 Cards:{0}", CardsPlayer3Counter), string.Format("Player 4 Cards:{0}", CardsPlayer4Counter));
            int count = 0;
            for (int i = 0; i <= counter; i++)

            {
                if (player1card[i].CardTypes.Contains("+4") || player1card[i].CardTypes.Contains("Change Color"))
                {
                    output += string.Format("{0}({1})|", player1card[i].CardTypes, count);
                }
                else

                    output += string.Format("{0}{1}({2})|", player1card[i].CardTypes, player1card[i].CardColors, count);
                count++;


            }
            Console.WriteLine(output);
        }
        static void dearCard()
        {
            List<string> cardTypeList = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "+2", "+4", "Change Color", "Change Order" };
            List<string> cardColorList = new List<string> { "R", "B", "G", "Y" };

            Random t = new Random();
            Random c = new Random();

            Cards[] stageCards = new Cards[CardsPlayer1Counter + 1];
            for (int i = 0; i <= CardsPlayer1Counter - 1; i++)
                stageCards[i] = new Cards(player1card[i].CardTypes, player1card[i].CardColors);
            stageCards[CardsPlayer1Counter] = new Cards(cardTypeList[t.Next(cardTypeList.Count - 1)], cardColorList[c.Next(cardColorList.Count - 1)]);
            player1card = stageCards;
            CardsPlayer1Counter++;
        }
        static bool cardPicked()
        {
            Console.WriteLine("Current Card: " + floorType + floorColor);
            string keyOutput = string.Empty;
            Console.WriteLine("Pic a card (enter key d to deal card):");

            int cardpicked = -1;

            while (keyOutput == string.Empty)
            {

                string stageOutput = Console.ReadLine();

                {
                    bool isNumeric = int.TryParse(stageOutput, out cardpicked);
                    if (isNumeric || stageOutput == "d")
                        if (stageOutput == "d")
                        {
                            dearCard();

                            keyOutput = "Deal";

                        }
                        else
                        {
                            if (cardpicked >= 0 && cardpicked <= CardsPlayer1Counter)

                            {
                                Cards currentCard = player1card[cardpicked];

                                if (currentCard.CardColors != string.Empty)
                                    if (floorColor == string.Empty && floorType == string.Empty)
                                    {
                                        floorColor = currentCard.CardColors;
                                        floorType = currentCard.CardTypes;
                                        keyOutput = "Go";

                                    }
                                    else
                                        wildCard(currentCard.CardTypes);
                                else
                                {
                                    if (currentCard.CardColors == floorColor || currentCard.CardTypes == floorType)
                                    {
                                        floorColor = currentCard.CardColors;
                                        floorType = currentCard.CardTypes;
                                        keyOutput = "Go";
                                    }
                                    else
                                    {
                                        keyOutput = string.Empty;
                                        Console.WriteLine("Invalid selection, please try again..");
                                    }
                                }
                            }

                            else
                            {
                                keyOutput = string.Empty;
                                Console.WriteLine("Invalid selection, please try again..");
                            }
                        }


                }
                Console.Clear();
                if (keyOutput == "Go")
                {
                    player1card[cardpicked] = new Cards("", "");
                    for (int i = cardpicked; i < 7; i++)
                    {

                        Cards nextCard = player1card[i + 1];
                        player1card[i] = new Cards(nextCard.CardTypes, nextCard.CardColors);

                    }
                    if (cardpicked != 7)
                    {
                        player1card[7] = new Cards("", "");

                    }
                    CardsPlayer1Counter--;



                }
            }
            return false;

        }
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome To UNO CMD to Start the game press Enter...");
            Console.ReadLine();
            Console.Clear();
            shuffleCards();
            bool gameEnd = false;
            string floorColor = string.Empty;
            string floorType = string.Empty;

            while (!gameEnd)
            {
                showcards(CardsPlayer1Counter - 1);
                gameEnd = cardPicked();



            }

        }
    }
}
