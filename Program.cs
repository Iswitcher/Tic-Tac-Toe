using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_tac_toe
{
    class Program
    {
        static void Main(string[] args)
        {
            bool retry = true;

            Console.WriteLine("================================================\nGreetings! You have launched a tic-tac-toe game!\n================================================");

            Player.gameModeSelect();
            Player.playerSelect();

            while (retry)
            {
                char reply;
                Game.startNewGame();
                while (Game.isGameOver() == false)
                {
                    Game.turn();
                }

                Console.WriteLine("Do you want to play again? Y/N");
                retry = char.TryParse(Console.ReadLine(),out reply);
                if (reply == 'y' || reply == 'Y') 
                {
                    retry = true;
                    Console.WriteLine("You have selected a retry option. Game will restart now.");
                }
                else if (reply == 'n' || reply == 'N') 
                {
                    retry = false;
                    Console.WriteLine("You have skipped the retrying part. Game will end soon.");
                }
                else 
                {
                    retry = false;
                    Console.WriteLine("Your response was not recognized. Game will end soon.");
                }
                
            }

            Console.WriteLine("Double-press the Escape (ESC) key to quit:");
            do
            {
                Console.ReadKey();
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }

    public class GameField
    {
        public static char[,] gameField = new char[3, 3];

        public static void PrepareGameField()
        {
            Console.WriteLine("==================================\n=======ULTIMATE TIC-TAC-TOE=======\n==================================\nHere is your cleared up game field:");

            //search for the every cell in the game field
            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    //set the default field value
                    gameField[i, j] = '_';
                }
            }
        }

        public static void PrintGameField()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   | 1 | 2 | 3 |");
            char a = 'a';
            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    if (j == 0) { Console.Write(" " + a.ToString() + " | " + gameField[i, j].ToString() + " | "); }
                    else if (j == 2) { Console.WriteLine(gameField[i, j].ToString() + " | "); }
                    else { Console.Write(gameField[i, j].ToString() + " | "); }
                }
                a++;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void WriteNewTurn(char symbol, int x,int y) 
        {
            if (gameField[x, y] == '_') { gameField[x, y] = symbol; }
            else 
            { 
                Console.WriteLine("This cell is already occupied. You cannot make your turn here. Enter another coordinates: ");
                Game.activePlayer = !Game.activePlayer;
            }
        }
    }

    public class Player
    {
        public static int gameMode = new int();
        public static bool isPlayer1AI, isPlayer2AI = new bool();
        //public static string player1Name, player2Name = "";

        public static void gameModeSelect()
        {
            bool result = new bool();

            Console.WriteLine("Please select the desired game mode:\n    for the SINGLE player vs AI mode press: 1\n    for the CO-OP game with 2 players press: 2");
            while (!result)
            {
                result = int.TryParse(Console.ReadLine(), out gameMode);
                if (gameMode != 1 && gameMode != 2)
                {
                    result = false;
                    Console.WriteLine("You have selected an invalid or non-existent game mode as of yet. Please, select a correct one instead:\n    for the SINGLE player vs AI mode press: 1\n    for the CO-OP game with 2 players press: 2");
                }

            }
            Console.WriteLine("You have selected a game mode with a number " + gameMode.ToString());
        }

        public static void playerSelect()
        {
            bool result = new bool();
            int playerNum = new int();

            while (!result)
            {
                if (gameMode == 1)
                {
                    Console.WriteLine("Please type the number of the player (1 or 2) you wish to play. The opposite player would be set up as an AI:");
                    result = int.TryParse(Console.ReadLine(), out playerNum);
                    if (playerNum == 1)
                    {
                        Console.WriteLine("You have chosen player #1. Your moves would be marked at the board as 'X'");
                        isPlayer2AI = true;
                    }
                    else if (playerNum == 2)
                    {
                        Console.WriteLine("You have chosen player #2. Your moves would be marked at the board as 'O'");
                        isPlayer1AI = true;
                    }
                    else
                    {
                        result = false;
                        Console.WriteLine("The player number you've entered is invalid! Please, enter the correct one below:");
                    }

                }
                if (gameMode == 2)
                {
                    Console.Write("The selected game mode requires two players.\nPlayer1 moves first and his moves are marked as '");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("X");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("'\nPlayer2 moves second and his moves would be marked as '");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("O");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("'");
                    Console.WriteLine();
                    result = true;
                }
            }
        }
    }

    public class Game
    {
        //active player variable. true for player1, false for player2
        public static bool activePlayer = new bool();
        //public static bool isGameOver = new bool();

        public static void startNewGame()
        {
            GameField.PrepareGameField();
            GameField.PrintGameField();

            //sets the player1 move as first 
            activePlayer = true;

            Console.WriteLine("The game field is set. Player1 can make his first turn:");
            //Console.WriteLine("For selecing a cell to make a move for - enter its coordinates like this: 'a1'.");
        }

        public static void turn()
        {
            string unparsedCoordinates,coordinates;
            char[] coordinatesList; //parsed coordinates to write into a game field
            bool result = new bool();
            string[] unparsedCoordArray = { "a1", "a2", "a3", "b1", "b2", "b3", "c1", "c2", "c3" }; //used to check input coordinates


            if (activePlayer == true) 
            { 
                Console.Write("Player 1 makes the turn: "); 
                //Console.Write(activePlayer.ToString()); 
                if (Player.isPlayer1AI == true) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(AI.AIOutput());
                    Console.ForegroundColor = ConsoleColor.Gray;
                    //Game.logging();
                    GameField.PrintGameField();
                    activePlayer = !activePlayer;
                    return; 
                }
            }
            else 
            {
                Console.Write("Player 2 makes the turn: ");
                //Console.Write(activePlayer.ToString());
                if (Player.isPlayer2AI == true) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(AI.AIOutput());
                    Console.ForegroundColor = ConsoleColor.Gray;
                    //Game.logging();
                    GameField.PrintGameField();
                    activePlayer = !activePlayer;
                    return; 
                }
            }

            //check input coordinates for eligibility (dumb)
            unparsedCoordinates = Console.ReadLine();
            while (!result)
            {
                
                if (!unparsedCoordArray.Contains(unparsedCoordinates))
                {
                    result = false;
                    Console.WriteLine("Wrong coordinates entered, please enter the valid ones:");
                    unparsedCoordinates = Console.ReadLine();
                }
                else { result = true; }
            }

            //TODO: rewrite this shit 
            coordinates = unparsedCoordinates.Replace('1', '0').Replace('2', '1').Replace('3', '2').Replace('a','0').Replace('b','1').Replace('c','2');
            coordinatesList = coordinates.ToCharArray(0, 2);
            //Console.WriteLine(coordinatesList.GetValue(0).ToString() + coordinatesList.GetValue(1).ToString()); //to be removed, printing for diagnostics

            GameField.WriteNewTurn(activePlayer? 'X':'O', int.Parse(coordinatesList[0].ToString()), int.Parse(coordinatesList[1].ToString()));

            GameField.PrintGameField();

            //sets the active turn for opposite player
            activePlayer = !activePlayer;
            //isGameOver = false;
        }

        public static bool isGameOver()
        {
            int freeCells = 0;
            string winStreakX = "",winStreakY = "", winStreakDiag = "",winStreakReverseDiag = "";
            
            //loop for empty cells
            for (int i = 0; i < GameField.gameField.GetLength(0); i++)
            {
                for (int j = 0; j < GameField.gameField.GetLength(1); j++)
                {
                    if (GameField.gameField[i,j] == '_') 
                    { 
                        freeCells++;
                        break; //exit if any cell is free
                    }
                }
            }

            //loop for winning HORISONTAL rows
            for (int i = 0; i < GameField.gameField.GetLength(0); i++)
            {
                winStreakX = "";
                for (int j = 0; j < GameField.gameField.GetLength(1); j++)
                {
                    winStreakX = winStreakX + GameField.gameField[i, j];
                    if (winStreakX == "XXX" || winStreakX == "OOO") { break; }
                }
                if (winStreakX == "XXX" || winStreakX == "OOO") { break; }
            }

            //loop for winning VERTICAL rows
            for (int j = 0; j < GameField.gameField.GetLength(0); j++)
            {
                winStreakY = "";
                for (int i = 0; i < GameField.gameField.GetLength(1); i++)
                {
                    winStreakY = winStreakY + GameField.gameField[i, j];
                    if (winStreakY == "XXX" || winStreakY == "OOO") { break; }
                }
                if (winStreakY == "XXX" || winStreakY == "OOO") { break; }
            }

            //loop for diagonal winning streaks 
            for (int i = 0, j = GameField.gameField.GetLength(1)-1 ; i < GameField.gameField.GetLength(0); i++, j--)
            {
                winStreakDiag = winStreakDiag + GameField.gameField[i, i];
                winStreakReverseDiag = winStreakReverseDiag + GameField.gameField[j, i];
            }



            //return if the winning streak is achieved
            if (winStreakX == "XXX" || winStreakY == "XXX" || winStreakDiag == "XXX" || winStreakReverseDiag == "XXX")
            {
                Console.WriteLine("Player 1 WINS!");
                return true;
            }
            else if (winStreakX == "OOO" || winStreakY == "OOO" || winStreakDiag == "OOO" || winStreakReverseDiag == "OOO")
            {
                Console.WriteLine("Player 2 WINS!");
                return true;
            }
            else if (freeCells == 0)
            {
                Console.WriteLine("GAME OVER! No free cells left!");
                return true;
                }
                else return false;
        }

        public static void logging()
        {
            for (int i = 0; i < AI.potentialCells.Count; i++) { Console.WriteLine(AI.potentialCells[i].ToString()); }
            for (int i = 0; i < AI.prioritizedRows.Count; i++) { Console.WriteLine(AI.prioritizedRows[i].ToString()); }
            for (int i = 0; i < AI.flaggedRows.Count; i++) { Console.WriteLine(AI.flaggedRows[i].ToString()); }
            for (int i = 0; i < AI.cellFrequencies.Count; i++) { Console.WriteLine(AI.cellFrequencies[i].ToString()); }
            for (int i = 0; i < AI.AIGameFieldWinningRows.Count; i++) { Console.WriteLine(AI.AIGameFieldWinningRows[i].ToString()); }
        }
    }

    public class AI
    {
        public static List<AIGameFieldRow> AIGameFieldWinningRows = new List<AIGameFieldRow>();
        static bool isPlayerChecked, isAIChecked;
        public static List<FrequencyRate> cellFrequencies = new List<FrequencyRate>();
        public static List<FlaggedAIGameFieldWinningRows> flaggedRows = new List<FlaggedAIGameFieldWinningRows>();

        public static List<int> prioritizedRows = new List<int>();
        public static List<AIPotentiaMovelCells> potentialCells = new List<AIPotentiaMovelCells>();

        public static void AIGetWinRows()
        {
            AIGameFieldWinningRows.Clear();
            
            //add to AIGameFieldWinningRows columns of possible winning rows
            for (int j = 0; j < GameField.gameField.GetLength(1) ; j++ ) //loop for columns
            {
                AIGameFieldWinningRows.Add(new AIGameFieldRow(){});
                for (int i = 0; i < GameField.gameField.GetLength(0); i++) //loop for rows
                {
                    if      (Player.isPlayer1AI == true && GameField.gameField[i,j] == 'X') {isAIChecked = true;}
                    else if (Player.isPlayer2AI == true && GameField.gameField[i,j] == 'O') {isAIChecked = true;}
                    else isAIChecked = false;

                    if      (Player.isPlayer1AI == false && GameField.gameField[i, j] == 'X') { isPlayerChecked = true; }
                    else if (Player.isPlayer2AI == false && GameField.gameField[i, j] == 'O') { isPlayerChecked = true; }
                    else isPlayerChecked = false;

                    AIGameFieldWinningRows[j].AIGameFieldRowArray[i] = new AIGameFieldCell(i, j, isPlayerChecked, isAIChecked);
                }

            }

            //add to AIGameFieldWinningRows rows of possible winning rows
            for (int i = 0 ; i < GameField.gameField.GetLength(0) ; i++ )
            {
                AIGameFieldWinningRows.Add(new AIGameFieldRow(){});
                for (int j = 0 ; j < GameField.gameField.GetLength(1); j++)
                {
                    if      (Player.isPlayer1AI == true && GameField.gameField[i, j] == 'X') { isAIChecked = true; }
                    else if (Player.isPlayer2AI == true && GameField.gameField[i, j] == 'O') { isAIChecked = true; }
                    else isAIChecked = false;

                    if      (Player.isPlayer1AI == false && GameField.gameField[i, j] == 'X') { isPlayerChecked = true; }
                    else if (Player.isPlayer2AI == false && GameField.gameField[i, j] == 'O') { isPlayerChecked = true; }
                    else isPlayerChecked = false;

                    AIGameFieldWinningRows[i + GameField.gameField.GetLength(0)].AIGameFieldRowArray[j] = new AIGameFieldCell(i, j, isPlayerChecked, isAIChecked);
                }
            }

            //add to AIGameFieldWinningRows winning diagonal row
            AIGameFieldWinningRows.Add(new AIGameFieldRow(){});
            for (int i = 0 ; i < GameField.gameField.GetLength(0) ; i++)
            {
                if (Player.isPlayer1AI == true && GameField.gameField[i, i] == 'X') { isAIChecked = true; }
                else if (Player.isPlayer2AI == true && GameField.gameField[i, i] == 'O') { isAIChecked = true; }
                else isAIChecked = false;

                if (Player.isPlayer1AI == false && GameField.gameField[i, i] == 'X') { isPlayerChecked = true; }
                else if (Player.isPlayer2AI == false && GameField.gameField[i, i] == 'O') { isPlayerChecked = true; }
                else isPlayerChecked = false;

                AIGameFieldWinningRows[AIGameFieldWinningRows.Count()-1].AIGameFieldRowArray[i] = new AIGameFieldCell(i, i, isPlayerChecked, isAIChecked);
            }

            //add to AIGameFieldWinningRows winning reverse diagonal row
            AIGameFieldWinningRows.Add(new AIGameFieldRow() { });
            for (int i = GameField.gameField.GetLength(0)-1, j=0; i >= 0; i--, j++)
            {
                if (Player.isPlayer1AI == true && GameField.gameField[i, j] == 'X') { isAIChecked = true; }
                else if (Player.isPlayer2AI == true && GameField.gameField[i, j] == 'O') { isAIChecked = true; }
                else isAIChecked = false;

                if (Player.isPlayer1AI == false && GameField.gameField[i, j] == 'X') { isPlayerChecked = true; }
                else if (Player.isPlayer2AI == false && GameField.gameField[i, j] == 'O') { isPlayerChecked = true; }
                else isPlayerChecked = false;

                AIGameFieldWinningRows[AIGameFieldWinningRows.Count() - 1].AIGameFieldRowArray[i] = new AIGameFieldCell(i, j, isPlayerChecked, isAIChecked);
            }


        }

        public static void AIGetBaseRates()
        { 
            int frequency;
            cellFrequencies.Clear();
                        
            for (int i = 0 ; i< GameField.gameField.GetLength(0) ; i++)
            {
                for (int j = 0 ; j< GameField.gameField.GetLength(1) ; j++)
                {
                    frequency = 0;

                    for ( int l = 0 ; l < AIGameFieldWinningRows.Count ; l++)
                    {
                        for (int r = 0 ; r < AIGameFieldWinningRows[l].AIGameFieldRowArray.Length ; r++ )
                        {
                            if (AIGameFieldWinningRows[l].AIGameFieldRowArray[r].x == i && AIGameFieldWinningRows[l].AIGameFieldRowArray[r].y == j)
                            {
                                frequency++;
                            }
                        }
                    }

                    cellFrequencies.Add(new FrequencyRate() {coordinateX = i, coordinateY = j, frequency = frequency});
                }
            }

        }

        public static void AIModifyRates()
        { 
            int RowPlayerOccupied, RowAIOccupied;
            flaggedRows.Clear();
            
            for (int i = 0; i < AIGameFieldWinningRows.Count; i++)
            {
                RowPlayerOccupied = 0;
                RowAIOccupied = 0;
                int AIPriority = 0;

                for (int a = 0; a < AIGameFieldWinningRows[i].AIGameFieldRowArray.Length; a++)
                { 
                    if (AIGameFieldWinningRows[i].AIGameFieldRowArray[a].isAITagged == true) 
                    { 
                        RowAIOccupied += 1;
                    }
                    if (AIGameFieldWinningRows[i].AIGameFieldRowArray[a].isPlayerTagged == true)
                    {
                        RowPlayerOccupied += 1;
                    }
                }
                if (RowAIOccupied == 2          && RowPlayerOccupied == 0) {AIPriority = 1;}
                else if (RowPlayerOccupied == 2 && RowAIOccupied == 0)     {AIPriority = 2;}
                else if (RowAIOccupied == 1     && RowPlayerOccupied == 0) {AIPriority = 3;}
                else if (RowPlayerOccupied == 1 && RowAIOccupied == 0)     {AIPriority = 4;}
                else if (RowAIOccupied == 0     && RowPlayerOccupied == 0) {AIPriority = 5;}
                else if (RowAIOccupied == 1     && RowPlayerOccupied == 1) {AIPriority = 6;}
                else AIPriority = 7;

                flaggedRows.Add(new FlaggedAIGameFieldWinningRows() {winningRowNubmer = i, playerOccipiedCells = RowPlayerOccupied, AIOccupiedCells = RowAIOccupied, AIPriority = AIPriority});
            }
        }

        public static void AIMakeMove()
        { 
            int selectedPriority = 7; //had to make this so, priority goes from 6 to 0
            int priority; 
            int movePriority = 0;
            int moveX = 0, moveY = 0;

            //clearing up the mess from the previous move
            prioritizedRows.Clear();
            potentialCells.Clear();

            for (int i = 0; i < flaggedRows.Count; i++)
            {
                //if the priority of the looped row is higher - select this priority instead
                if (selectedPriority > flaggedRows[i].AIPriority) { selectedPriority = flaggedRows[i].AIPriority; }
                else continue;
            }
            //now we have a priority we could use as a search for:
            for (int i = 0; i < flaggedRows.Count; i++)
            {
                if (flaggedRows[i].AIPriority == selectedPriority) 
                {
                    prioritizedRows.Add(i);
                }
            }
            //now we have only winning row numbers which are interesting to us
            for (int i = 0; i < prioritizedRows.Count; i++)
            {
                
                
                for (int j = 0; j < AIGameFieldWinningRows[i].AIGameFieldRowArray.Length; j++)
                {
                    if (AIGameFieldWinningRows[prioritizedRows[i]].AIGameFieldRowArray[j].isAITagged == false &&
                        AIGameFieldWinningRows[prioritizedRows[i]].AIGameFieldRowArray[j].isPlayerTagged == false)
                    {
                        priority = 0;

                        for (int a = 0 ; a < cellFrequencies.Count; a++)
                        {
                            if (AIGameFieldWinningRows[prioritizedRows[i]].AIGameFieldRowArray[j].x == cellFrequencies[a].coordinateX &&
                                AIGameFieldWinningRows[prioritizedRows[i]].AIGameFieldRowArray[j].y == cellFrequencies[a].coordinateY)
                            {
                                priority = cellFrequencies[a].frequency;
                            }
                            else continue;
                        }

                        potentialCells.Add(new AIPotentiaMovelCells()
                        {
                            cellCoordX = AIGameFieldWinningRows[prioritizedRows[i]].AIGameFieldRowArray[j].x,
                            cellCoordY = AIGameFieldWinningRows[prioritizedRows[i]].AIGameFieldRowArray[j].y,
                            basePriority = priority});
                    }
                }
            }
            //now we have a list of cells for current possible move with added base priority

            for (int i = 0; i < potentialCells.Count; i++)
            {
                if (movePriority < potentialCells[i].basePriority) { movePriority = potentialCells[i].basePriority; }
            }

            for (int i = 0; i < potentialCells.Count; i++)
            {
                if (potentialCells[i].basePriority == movePriority) 
                {
                    moveX = potentialCells[i].cellCoordX;
                    moveY = potentialCells[i].cellCoordY;
                }
            }

            //finally, let's make a move
            if (Player.isPlayer1AI == true) {GameField.gameField[moveX,moveY] = 'X';}
            else if (Player.isPlayer2AI == true) { GameField.gameField[moveX, moveY] = 'O'; }

        }

        public static string AIOutput()
        {
            string AIResponse = "AI making turn...";
            AIGetWinRows();
            AIGetBaseRates(); 
            AIModifyRates();
            AIMakeMove();
            return AIResponse;
        }
    }

    public class AIGameFieldCell
    {
        public int x, y; //coordinates for the gameField
        public bool isPlayerTagged, isAITagged;
        public AIGameFieldCell(int x, int y, bool isPlayerTagged, bool isAITagged)
        {
            this.x = x;
            this.y = y;
            this.isPlayerTagged = isPlayerTagged;
            this.isAITagged = isAITagged;
        }

        public override string ToString()
        {
            return " GameCell:"+ x+ y + isPlayerTagged + isAITagged;
        }
    }

    public class AIGameFieldRow
    {
        public AIGameFieldCell[] AIGameFieldRowArray = new AIGameFieldCell[3];

        public override string ToString()
        {
            return "GameRow: " + AIGameFieldRowArray[0] + AIGameFieldRowArray[1] + AIGameFieldRowArray[2];
        }
    }

    public class FrequencyRate
    {
        public int coordinateX { get; set; }
        public int coordinateY { get; set; }
        public int frequency   { get; set; }

        public override string ToString()
        {
            return "X:" + coordinateX + " Y:" + coordinateY +" Frequency: "+frequency;
        }
    }

    public class FlaggedAIGameFieldWinningRows
    {
        public int winningRowNubmer { get; set; }
        public int playerOccipiedCells { get; set; }
        public int AIOccupiedCells { get; set; }
        public int AIPriority { get; set; }

        public override string ToString()
        {
            return "winningRowNubmer:" + winningRowNubmer + " playerOccipiedCells:" + playerOccipiedCells + " AIOccupiedCells:" + AIOccupiedCells + " AIPriority:" + AIPriority;
        }
    }

    public class AIPotentiaMovelCells
    {
        public int cellCoordX;
        public int cellCoordY;
        public int basePriority;

        public override string ToString()
        {
            return "Cell_X:" + cellCoordX +" Cell_Y:"+ cellCoordY + " Set_priority: " + basePriority;
        }
    }
}


