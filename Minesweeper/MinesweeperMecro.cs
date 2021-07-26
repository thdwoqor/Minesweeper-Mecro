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
            //var element = _driver.FindElements(By.XPath("//*[@id='A43']/div"));

            var element = _driver.FindElements(By.XPath("/html/body/div[3]/div[2]/div/div[1]/div[2]/div/div[19]/table/tbody/tr/td[1]/div/div[1]/div[4]/div[2]/div"));
            
            int column = 0;
            int row = 0;
            foreach (var e1 in element)
            {
                string e_value = e1.GetAttribute("class");

                if (e_value == "clear")
                {
                    row++;
                    column = 0;
                    continue;
                }
                m[row, column] = new MinesweeperMecro();
                m[row, column].center_e = e1;
                m[row, column].zero_count = 0;
                


                if (e_value == "cell size24 hd_closed hd_flag")
                {
                    m[row, column].center_n = 7;
                    column++;
                    continue;
                }
                else if (e_value == "cell size24 hd_closed")
                {
                    m[row, column].center_n = 8;
                    column++;
                    continue;
                }
                else if (e_value == "cell size24 hd_opened hd_type0")
                {
                    m[row, column].center_n = 9;
                    column++;
                    continue;
                }
                else if (e_value == "cell size24 hd_opened hd_type1")
                {
                    m[row, column].center_n = 1;
                    column++;
                    continue;
                }
                else if (e_value == "cell size24 hd_opened hd_type2")
                {
                    m[row, column].center_n = 2;
                    column++;
                    continue;
                }
                else if (e_value == "cell size24 hd_opened hd_type3")
                {
                    m[row, column].center_n = 3;
                    column++;
                    continue;
                }
                else if (e_value == "cell size24 hd_opened hd_type4")
                {
                    m[row, column].center_n = 4;
                    column++;
                    continue;
                }
                else if (e_value == "cell size24 hd_opened hd_type5")
                {
                    m[row, column].center_n = 5;
                    column++;
                    continue;
                }
            }
        }

        public void set(MinesweeperMecro[,] m)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 30; j++)
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

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if ( m[i, j].center_n == 0)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                if (isValidPosition(i - 1 + z, j - 1 + k) && m[i-1+z, j-1+k].center_n == 8)
                                {
                                    Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[i - 1 + z, j - 1 + k].center_e).Click().Perform();
                                    m[i, j].zero_count--;
                                    m[i - 1 + z, j - 1 + k].center_n = 6;
                                }
                            }
                        }
                        //return;
                    }

                    if (m[i, j].zero_count == 1 && m[i, j].center_n == 1)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                if (isValidPosition(i - 1 + z, j - 1 + k) && m[i - 1 + z, j - 1 + k].center_n == 8)
                                {
                                    Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[i - 1 + z, j - 1 + k].center_e).ContextClick().Perform();
                                    m[i, j].center_n--;
                                    m[i, j].zero_count--;
                                    m[i - 1 + z, j - 1 + k].center_n = 7;
                                    //return;
                                }
                            }
                        }
                    }

                    if (m[i, j].center_n == 2&& isValidPosition(i, j - 1)&& isValidPosition(i, j + 1) && (m[i, j-1].center_n == 1 && m[i, j+1].center_n == 1))
                    {
                        if (isValidPosition(i - 1, j - 1) && m[i-1, j-1].center_n == 8)
                        {
                            Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[i - 1, j - 1].center_e).ContextClick().Perform();
                        }
                        if (isValidPosition(i - 1, j + 1) && m[i - 1, j + 1].center_n == 8)
                        {
                            Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[i - 1, j + 1].center_e).ContextClick().Perform();
                        }

                        if (isValidPosition(i + 1, j - 1) && m[i + 1, j - 1].center_n == 8)
                        {
                            Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[i + 1, j - 1].center_e).ContextClick().Perform();
                        }
                        if (isValidPosition(i + 1, j + 1) && m[i + 1, j + 1].center_n == 8)
                        {
                            Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[i + 1, j + 1].center_e).ContextClick().Perform();
                        }
                    }
                    else if (m[i, j].center_n == 2 && isValidPosition(i - 1, j) && isValidPosition(i + 1, j) && (m[i - 1, j].center_n == 1 && m[i + 1, j].center_n == 1))
                    {
                        if (isValidPosition(i - 1, j - 1) && m[i - 1, j - 1].center_n == 8)
                        {
                            Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[i - 1, j - 1].center_e).ContextClick().Perform();
                        }
                        if (isValidPosition(i + 1, j - 1) && m[i + 1, j - 1].center_n == 8)
                        {
                            Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[i + 1, j - 1].center_e).ContextClick().Perform();
                        }
                        if (isValidPosition(i - 1, j + 1) && m[i - 1, j + 1].center_n == 8)
                        {
                            Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[i - 1, j + 1].center_e).ContextClick().Perform();
                        }
                        if (isValidPosition(i + 1, j + 1) && m[i + 1, j + 1].center_n == 8)
                        {
                            Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[i + 1, j + 1].center_e).ContextClick().Perform();
                        }
                    }
                }
            }
            /*
            Random rand = new Random();
            int x = rand.Next(0, 16);
            int y = rand.Next(0, 30);

            while (true)
            {
                if (m[x, y].center_n == 8)
                {
                    Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[x, y].center_e).Click().Perform();
                    return;
                }
                x = rand.Next(0, 16);
                y = rand.Next(0, 30);
            }
            */

        }
        private bool isValidPosition(int x, int y)
        {
            if (x < 0 || y < 0 || x >= 16 || y >= 30) return false;
            return true;
        }

        private void bordervalue(int x, int y, int z, int k, MinesweeperMecro[,] m)
        {
            
            if (isValidPosition(x, y))
            {
                //m[z, k].border_n[x - z + 1, y - k + 1] = m[x, y].center_n;
                //m[z, k].border_e[x - z + 1, y - k + 1] = m[x, y].center_e;
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

        //public int[,] border_n = new int[3, 3];
        //□□□
        //□■□
        //□□□
        //public IWebElement[,] border_e = new IWebElement[3, 3];

        public int zero_count = 0;

    }
}
