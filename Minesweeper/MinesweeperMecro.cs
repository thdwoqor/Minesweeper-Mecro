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
        public MinesweeperMecro[,] m;
        public ChromeDriver _driver;

        public Mecro(MinesweeperMecro[,] m, ChromeDriver _driver)
        {
            this.m = m;
            this._driver = _driver;
        }


        public void Init()
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
                else if (e_value == "cell size24 hd_opened hd_type6")
                {
                    m[row, column].center_n = 6;
                    column++;
                    continue;
                }
            }
        }

        public void set()
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

        public void click_event()
        {
            bool not_find = true;

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if(centerzero(i, j))
                        not_find = false;

                    if(bombfinded(i,j))
                        not_find = false;

                    if (onetwoone(i, j)) 
                        not_find = false;

                }
            }

            if (not_find)
            {
                int n = 0;
                int x = 0, y = 0;
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        if (m[i, j].center_n > 0 && m[i, j].center_n <= 6 && Math.Abs(m[i, j].center_n - m[i, j].zero_count) > n)
                        {
                            n = Math.Abs(m[i, j].center_n - m[i, j].zero_count);
                            x = i; y = j;
                        }
                    }
                }
                Debug.WriteLine($"{x} {y} {m[x, y].center_n} {m[x, y].zero_count} *");
                
                while (true)
                {
                    
                    Random rand = new Random();
                    int xx = x - 1 + rand.Next(0, 3);
                    int yy = y - 1 + rand.Next(0, 3);

                    if (isValidPosition(xx, yy)&& m[xx, yy].center_n==8)
                    {
                        Debug.WriteLine($"{xx} {yy} **");
                        Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[xx, yy].center_e).Click().Perform();
                        break;
                    }

                }
            }
        }

        private bool bombfinded(int x, int y)
        {
            bool change = false;
            if (m[x, y].zero_count == m[x, y].center_n && m[x, y].center_n <= 6 && m[x, y].center_n > 0)
            {
                for (int a = 0; a < 3; a++)
                {
                    for (int b = 0; b < 3; b++)
                    {
                        if (isValidPosition(x - 1 + a, y - 1 + b) && m[x - 1 + a, y - 1 + b].center_n == 8)
                        {
                            Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[x - 1 + a, y - 1 + b].center_e).ContextClick().Perform();

                            m[x - 1 + a, y - 1 + b].center_n = 7;

                            Refresh(x - 1 + a, y - 1 + b, 7);

                            change = true;
                        }
                    }
                }
            }
            if(change)
                return true;
            else
                return false;
        }

        private bool centerzero(int x, int y)
        {
            bool change = false;
            if (m[x, y].center_n == 0)
            {
                for (int a = 0; a < 3; a++)
                {
                    for (int b = 0; b < 3; b++)
                    {
                        if (isValidPosition(x - 1 + a, y - 1 + b) && m[x - 1 + a, y - 1 + b].center_n == 8)
                        {
                            Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[x - 1 + a, y - 1 + b].center_e).Click().Perform();

                            m[x - 1 + a, y - 1 + b].center_n = 9;
                            Refresh(x - 1 + a, y - 1 + b, 8);
                            change = true;
                        }
                    }
                }
            }
            if (change)
                return true;
            else
                return false;
        }

        private bool onetwoone(int x, int y)
        {
            if (x < 0 || y < 0 || x + 2 >= 16 || y + 2 >= 30) return false;
            bool change = false;
            if ((m[x + 1, y].center_n == 1 && m[x + 1, y + 1].center_n == 2 && m[x + 1, y + 2].center_n == 1))
            {
                //■□■
                // 1 2 1
                //□□□
                if (m[x + 2, y].center_n != 8  && m[x + 2, y + 2].center_n != 8)
                {
                    if (m[x, y].center_n == 8)
                    {
                        Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[x, y].center_e).ContextClick().Perform();
                        m[x, y].center_n = 7;
                        Refresh(x, y, 7);
                        change = true;
                    }
                    if (m[x, y+2].center_n == 8)
                    {
                        Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[x, y+2].center_e).ContextClick().Perform();
                        m[x, y + 2].center_n = 7;
                        Refresh(x, y + 2, 7);
                        change = true;
                    }
                }
                //□□□
                // 1 2 1
                //■□■
                if (m[x , y].center_n != 8  && m[x , y + 2].center_n != 8)
                {
                    if (m[x+2, y].center_n == 8)
                    {
                        Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[x + 2, y].center_e).ContextClick().Perform();
                        m[x + 2, y].center_n = 7;
                        Refresh(x + 2, y, 7);
                        change = true;
                    }
                    if (m[x+2, y + 2].center_n == 8)
                    {
                        Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[x + 2, y + 2].center_e).ContextClick().Perform();
                        m[x + 2, y + 2].center_n = 7;
                        Refresh(x + 2, y + 2, 7);
                        change = true;
                    }
                }
            }

            if ((m[x , y+1].center_n == 1 && m[x + 1, y+1 ].center_n == 2 && m[x + 2, y+1].center_n == 1))
            {
                //□ 1 ■
                //□ 2 □
                //□ 1 ■
                if (m[x , y].center_n != 8  && m[x + 2, y].center_n != 8)
                {
                    if (m[x, y+2].center_n == 8)
                    {
                        Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[x, y + 2].center_e).ContextClick().Perform();
                        m[x, y + 2].center_n = 7;
                        Refresh(x, y + 2, 7);
                        change = true;
                    }
                    if (m[x+2, y + 2].center_n == 8)
                    {
                        Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[x + 2, y + 2].center_e).ContextClick().Perform();
                        m[x + 2, y + 2].center_n = 7;
                        Refresh(x + 2, y + 2, 7);
                        change = true;
                    }
                }
                //■ 1 □
                //□ 2 □
                //■ 1 □
                if (m[x, y+2].center_n != 8  && m[x+2, y + 2].center_n != 8)
                {
                    if (m[x , y].center_n == 8)
                    {
                        Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[x, y].center_e).ContextClick().Perform();
                        m[x, y].center_n = 7;
                        Refresh(x, y, 7);
                        change = true;
                    }
                    if (m[x + 2, y].center_n == 8)
                    {
                        Actions actionProvider = new Actions(_driver); actionProvider.MoveToElement(m[x + 2, y].center_e).ContextClick().Perform();
                        m[x + 2, y].center_n = 7;
                        Refresh(x + 2, y,7);
                        change = true;
                    }
                }
            }
            if(change)
                return true;
            else
                return false ;
        }

        public void Refresh(int x,int y,int num)
        {
            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    if (a == 1 && b == 1)
                        continue;
                    if (isValidPosition(x - 1 + a, y - 1 + b))
                    {
                        m[x - 1 + a, y - 1 + b].zero_count--;
                        if (m[x - 1 + a, y - 1 + b].center_n >= 1 && m[x - 1 + a, y - 1 + b].center_n <= 6&& num==7)
                            m[x - 1 + a, y - 1 + b].center_n--;
                    }
                }
            }
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
                if (m[x, y].center_n == 8)
                    m[z, k].zero_count++;
                if (m[x, y].center_n == 7)
                {
                    if(m[z, k].center_n>=1&& m[z, k].center_n <= 6)
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
