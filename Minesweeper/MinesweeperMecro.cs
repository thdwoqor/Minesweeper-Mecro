using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Mecro
    {
        public void Init(ChromeDriver _driver, MinesweeperMecro[,] m)
        {
            var element = _driver.FindElements(By.XPath("//*[@id='A43']/div"));
            int column = 0;
            int row = 0;
            foreach (var e1 in element)
            {
                
                if (e1.GetAttribute("class") == "clear")
                {
                    row++;
                    column = 0;
                    continue;
                }
                m[row, column] = new MinesweeperMecro();
                m[row, column].center_e = e1;
                m[row, column].zero_count = 0;
                


                if (e1.GetAttribute("class") == "cell size24 hd_closed hd_flag")
                {
                    m[row, column].center_n = 7;
                    column++;
                }
                else if (e1.GetAttribute("class") == "cell size24 hd_closed")
                {
                    m[row, column].center_n = 8;
                    column++;
                }
                else if (e1.GetAttribute("class") == "cell size24 hd_opened hd_type0")
                {
                    m[row, column].center_n = 9;
                    column++;
                }
                else if (e1.GetAttribute("class") == "cell size24 hd_opened hd_type1")
                {
                    m[row, column].center_n = 1;
                    column++;
                }
                else if (e1.GetAttribute("class") == "cell size24 hd_opened hd_type2")
                {
                    m[row, column].center_n = 2;
                    column++;
                }
                else if (e1.GetAttribute("class") == "cell size24 hd_opened hd_type3")
                {
                    m[row, column].center_n = 3;
                    column++;
                }

            }
        }

        public void set(MinesweeperMecro[,] m)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            if (z == 1 && k == 1)
                                continue;
                            bordervalue(i - 1 + z, j - 1 + k, i, j,m);
                        }
                    }
                }
            }
        }

        public void click_event(ChromeDriver _driver, MinesweeperMecro[,] m)
        {

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (m[i, j].zero_count == 1 && m[i, j].center_n == 0)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                if (m[i, j].border_n[z, k] == 8)
                                {
                                    Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[i, j].border_e[z, k]).Click().Perform();
                                }
                            }
                        }
                        return;
                    }

                    if (m[i, j].zero_count == 1 && m[i, j].center_n == 1)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                if (m[i, j].border_n[z, k] == 8)
                                {
                                    Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[i, j].border_e[z, k]).ContextClick().Perform();

                                    /*
                                    for (int x = 0; x < 4; x++)
                                    {
                                        if (isValidPosition_3(z + xdir[x], k + ydir[x])&& m[i, j].border_n[z + xdir[x], k + ydir[x]] != 7)
                                        {
                                            actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[i, j].border_e[z + xdir[x], k + ydir[x]]).Click().Perform();
                                        }
                                    }
                                    */
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            Random rand = new Random();
            int x = rand.Next(0, 9);
            int y = rand.Next(0, 9);

            while (true)
            {
                if (m[x, y].center_n == 8)
                {
                    Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[x, y].center_e).Click().Perform();
                    return;
                }
                x = rand.Next(0, 9);
                y = rand.Next(0, 9);
            }

        }
        private bool isValidPosition(int x, int y)
        {
            if (x < 0 || y < 0 || x >= 9 || y >= 9) return false;
            return true;
        }

        private void bordervalue(int x, int y, int z, int k, MinesweeperMecro[,] m)
        {
            if (isValidPosition(x, y))
            {
                m[z, k].border_n[x - z + 1, y - k + 1] = m[x, y].center_n;
                m[z, k].border_e[x - z + 1, y - k + 1] = m[x, y].center_e;
                if (m[x, y].center_n == 8)
                    m[z, k].zero_count++;
                if (m[x, y].center_n == 7)
                {
                    if(m[z, k].center_n>=1&& m[z, k].center_n <= 3|| m[z, k].center_n == 9)
                        m[z, k].center_n--;
                }
            }
        }

        private bool isValidPosition_3(int x, int y)
        {
            if (x < 0 || y < 0 || x >= 3 || y >= 3) return false;
            return true;
        }

        int[] xdir = { -1, 1, 0, 0 };
        int[] ydir = { 0, 0, -1, 1 };

    }
    public class MinesweeperMecro
    {
        public int center_n;
        public IWebElement center_e;

        public int[,] border_n = new int[3, 3];
        //□□□
        //□■□
        //□□□
        public IWebElement[,] border_e = new IWebElement[3, 3];

        public int zero_count = 0;

    }
}
