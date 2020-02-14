using System;

namespace _01_飞行棋游戏
{
    class Program
    {

        static int[] Maps = new int[100];//静态字段模拟全局变量

        static int[] PlayerPos = new int[2];//声明一个静态数组用来存储玩家A和玩家B的坐标

        static string[] PlayerNames = new string[2];//存储玩家的姓名

        static bool[] Flags = new bool[2];//两个玩家的标记

        static void Main(string[] args)
        {
            GameShow();
            #region 输入玩家姓名
            Console.WriteLine("Please enter the name of player A:");
            PlayerNames[0] = Console.ReadLine();
            while (PlayerNames[0] == "")
            {
                Console.WriteLine("Player name cannot be empty, please re-enter:");
                PlayerNames[0] = Console.ReadLine();
            }
            Console.WriteLine("Please enter the name of player B:");
            PlayerNames[1] = Console.ReadLine();
            while (PlayerNames[1] == "" || PlayerNames[1] == PlayerNames[0])
            {
                if (PlayerNames[1] == "")
                {
                    Console.WriteLine("Player name cannot be empty, please re-enter:");
                    PlayerNames[1] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Player B connot have the same name as Player A," +
                        " please re-enter:");
                    PlayerNames[1] = Console.ReadLine();
                }
            }
            #endregion
            //玩家姓名输入完成后，首先应该清屏
            Console.Clear();//清屏
            GameShow();
            Console.WriteLine("{0}的飞机用A表示", PlayerNames[0]);
            Console.WriteLine("{0}的飞机用B表示", PlayerNames[1]);

            //画地图之前必须初始化地图
            InitailMap();
            DrawMap();

            //玩家A和玩家B都不在终点时，可以一直玩游戏
            while (PlayerPos[0] < 99 && PlayerPos[1] < 99)
            {
                if (Flags[0] == false)
                {
                    PlayGame(0);
                }
                else
                {
                    Flags[0] = false;
                }
                if (PlayerPos[0] >= 99)
                {
                    Console.WriteLine("玩家{0}赢了玩家{1}", PlayerNames[0], PlayerNames[1]);
                    break;
                }
                if (Flags[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    Flags[1] = false;
                }
                if (PlayerPos[1] >= 99)
                {
                    Console.WriteLine("玩家{0}赢了玩家{1}", PlayerNames[1], PlayerNames[0]);
                    break;
                }
            }//while
            Console.WriteLine("Game Over!!!");
            Console.ReadKey();
        }

        /// <summary>
        /// 设置游戏头
        /// </summary>
        public static void GameShow()
        {
            Console.ForegroundColor = ConsoleColor.Green;//设置前景颜色（改变文本颜色 ConsoleColor--枚举类型）
            Console.WriteLine("*****************************");
            Console.WriteLine("*****************************");
            Console.WriteLine("*****************************");
            Console.WriteLine("******Flying Chess Game******");
            Console.WriteLine("*****************************");
            Console.WriteLine("*****************************");
            Console.WriteLine("*****************************");
        }

        /// <summary>
        /// 初始化地图
        /// </summary>
        public static void InitailMap()
        {
            int[] luckyturn = { 6, 23, 40, 55, 69, 83 };//幸运轮盘◎
            for (int i = 0; i < luckyturn.Length; i++)
            {
                //int index = luckyturn[i];
                Maps[luckyturn[i]] = 1;
            }

            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//地雷☆
            for (int i = 0; i < landMine.Length; i++)
            {
                Maps[landMine[i]] = 2;
            }

            int[] pause = { 9, 27, 60, 93 };//暂停▲
            for (int i = 0; i < pause.Length; i++)
            {
                Maps[pause[i]] = 3;
            }

            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };//时空隧道卍
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                Maps[timeTunnel[i]] = 4;
            }
        }

        /// <summary>
        /// 画地图
        /// </summary>
        public static void DrawMap()
        {
            Console.WriteLine("图例：幸运轮盘：◎  地雷：☆  暂停：▲  时空隧道：卍");
            #region 第一横行
            for (int i = 0; i < 30; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            #endregion
            //画完第一横行之后应该换行
            Console.WriteLine();

            #region 第一竖行
            for (int i = 30; i < 35; i++)
            {
                for (int j = 0; j < 29; j++)
                {
                    Console.Write("  ");
                }

                Console.Write(DrawStringMap(i));
                Console.WriteLine();
            }
            #endregion

            #region 第二横行
            for (int i = 64; i >= 35; i--)
            {
                Console.Write(DrawStringMap(i));
            }
            #endregion
            //画完第二横行之后应该换行
            Console.WriteLine();

            #region 第二竖行
            for (int i = 65; i <= 69; i++)
            {
                Console.WriteLine(DrawStringMap(i));
            }
            #endregion

            #region 第三横行
            for (int i = 70; i <= 99; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            #endregion
            //画完最后一行之后应该换行
            Console.WriteLine();

        }//DrawMap结尾

        /// <summary>
        /// 从画地图的方法中抽象出来的一个方法--画关卡
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string DrawStringMap(int i)
        {
            string str = "";
            //如果玩家A和玩家B的坐标相同，并且都在地图上，画一个<>
            if (PlayerPos[0] == PlayerPos[1] && PlayerPos[0] == i)
            {
                //Console.Write("<>");
                str = "<>";
            }
            else if (PlayerPos[0] == i)
            {
                //Console.Write("A");
                str = "A";
            }
            else if (PlayerPos[1] == i)
            {
                //Console.Write("B");
                str = "B";
            }
            else
            {
                switch (Maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        //Console.Write("□");
                        str = "□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Green;
                        //Console.Write("◎");
                        str = "◎";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        //Console.Write("☆");
                        str = "☆";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.White;
                        //Console.Write("▲");
                        str = "▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        //Console.Write("卍");
                        str = "卍";
                        break;
                }//switch
            }//else
            return str;
        }

        /// <summary>
        /// 玩游戏
        /// </summary>
        public static void PlayGame(int playerNumber)
        {
            Random r = new Random();
            int rNumber = r.Next(1, 7);
            Console.WriteLine("{0}按任意键开始掷骰子", PlayerNames[playerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("{0}掷出了{1}", PlayerNames[playerNumber], rNumber);
            PlayerPos[playerNumber] += rNumber;
            ChangePos();
            Console.ReadKey(true);
            Console.WriteLine("{0}按任意键开始行动", PlayerNames[playerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("{0}行动结束", PlayerNames[playerNumber]);
            Console.ReadKey(true);
            //玩家A有可能踩到了玩家B 方块 幸运轮盘 地雷 暂停 时空隧道
            if (PlayerPos[playerNumber] == PlayerPos[1 - playerNumber])
            {
                Console.WriteLine("玩家{0}踩到了玩家{1}，玩家{2}退6格", PlayerNames[playerNumber], PlayerNames[1 - playerNumber], PlayerNames[1 - playerNumber]);
                PlayerPos[1 - playerNumber] -= 6;
                ChangePos();
                Console.ReadKey(true);
            }
            else // 踩到了关卡
            {
                //玩家的坐标
                switch (Maps[PlayerPos[playerNumber]])
                {
                    case 0:
                        Console.WriteLine("玩家{0}踩到了方块，安全", PlayerNames[playerNumber]);
                        Console.ReadKey(true);
                        break;
                    case 1:
                        Console.WriteLine("玩家{0}踩到了幸运轮盘：1---交换位置 2---轰炸对方", PlayerNames[playerNumber]);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine("玩家{0}选择和玩家{1}交换位置", PlayerNames[playerNumber], PlayerNames[1 - playerNumber]);
                                Console.ReadKey(true);
                                int temp = PlayerPos[playerNumber];
                                PlayerPos[playerNumber] = PlayerPos[1 - playerNumber];
                                PlayerPos[1 - playerNumber] = temp;
                                Console.WriteLine("交换完成，按任意键继续游戏！");
                                Console.ReadKey(true);
                                break;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("玩家{0}选择轰炸玩家{1}，玩家{2}退6格", PlayerNames[playerNumber], PlayerNames[1 - playerNumber], PlayerNames[1 - playerNumber]);
                                Console.ReadKey(true);
                                PlayerPos[1 - playerNumber] -= 6;
                                ChangePos();
                                Console.WriteLine("玩家{0}退6格完成", PlayerNames[1 - playerNumber]);
                                Console.ReadKey(true);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("只能输入1或2：1---交换位置 2---轰炸对方");
                                input = Console.ReadLine();
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("玩家{0}踩到了地雷，退6格", PlayerNames[playerNumber]);
                        Console.ReadKey(true);
                        PlayerPos[playerNumber] -= 6;
                        ChangePos();
                        break;
                    case 3:
                        Console.WriteLine("玩家{0}踩到了暂停，暂停一回合", PlayerNames[playerNumber]);
                        Flags[playerNumber] = true;
                        Console.ReadKey(true);
                        break;
                    case 4:
                        Console.WriteLine("玩家{0}踩到了时空隧道，前进10格", PlayerNames[playerNumber]);
                        PlayerPos[playerNumber] += 10;
                        ChangePos();
                        Console.ReadKey(true);
                        break;
                }//switch
            }//else
            ChangePos();
            Console.Clear();
            DrawMap();
        }

        /// <summary>
        /// 当玩家坐标发生改变的时候
        /// </summary>
        public static void ChangePos()
        {
            if (PlayerPos[0] < 0)
            {
                PlayerPos[0] = 0;
            }
            if (PlayerPos[0] >= 99)
            {
                PlayerPos[0] = 99;
            }
            if (PlayerPos[1] < 0)
            {
                PlayerPos[1] = 0;
            }
            if (PlayerPos[1] >= 99)
            {
                PlayerPos[1] = 99;
            }
        }
    }
}
