using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{

    public partial class Form1 : Form
    {
        private ChromeDriverService _driverService = null;
        private ChromeOptions _options = null;

        private ChromeDriver _driver = null;

        private MinesweeperMecro[,] m;

        Mecro Mecro1;

        public Form1()
        {
            InitializeComponent();

            _driverService = ChromeDriverService.CreateDefaultService();
            _driverService.HideCommandPromptWindow = true;

            _options = new ChromeOptions();
            //_options.AddArgument("--headless");
            //_options.AddArgument("disable-gpu");
            //_options.AddArgument("user-agent=Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6)" + "AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36");
            _options.AddArgument("disable-infobars");
            _options.AddArgument("--disable-extensions");
            

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            m = new MinesweeperMecro[16, 30];
            _driver = new ChromeDriver(@"chrom", _options);
            _driver.Navigate().GoToUrl("https://minesweeper.online/ko/start/3");  // 웹 사이트에 접속합니다.
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            try
            {
                while (_driver.FindElement(By.XPath("//*[@id='init_loading']")).Enabled)
                {
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Mecro1 = new Mecro(m, _driver);
            while (true)
            {
                Mecro1.Init();

                Mecro1.set();

                Mecro1.click_event();

            }
        }
    }
}

/*
 if(isValidPosition(i-1, j - 1))
                    {
                        m[i, j].border_n[0, 0] = m[i - 1, j - 1].center_n;
                        m[i, j].border_e[0, 0] = m[i - 1, j - 1].center_e;
                        if (m[i - 1, j - 1].center_n == 0)
                            m[i - 1, j - 1].zero_count++;
                    }
                    //■□□
                    //□□□
                    //□□□
                    if (isValidPosition(i - 1, j))
                    {
                        m[i, j].border_n[0, 1] = m[i - 1, j].center_n;
                        m[i, j].border_e[0, 1] = m[i - 1, j].center_e;
                        if (m[i - 1, j].center_n == 0)
                            m[i - 1, j].zero_count++;
                    }
                    //□■□
                    //□□□
                    //□□□
                    if (isValidPosition(i - 1, j+1))
                    {
                        m[i, j].border_n[0, 2] = m[i - 1, j + 1].center_n;
                        m[i, j].border_e[0, 2] = m[i - 1, j + 1].center_e;
                        if (m[i - 1, j + 1].center_n == 0)
                            m[i - 1, j + 1].zero_count++;
                    }
                    //□□■
                    //□□□
                    //□□□
                    if (isValidPosition(i , j - 1))
                    {
                        m[i, j].border_n[1, 0] = m[i , j - 1].center_n;
                        m[i, j].border_e[1, 0] = m[i , j - 1].center_e;
                        if (m[i, j - 1].center_n == 0)
                            m[i, j - 1].zero_count++;
                    }
                    //□□□
                    //■□□
                    //□□□
                    if (isValidPosition(i, j + 1))
                    {
                        m[i, j].border_n[1, 2] = m[i, j + 1].center_n;
                        m[i, j].border_e[1, 2] = m[i, j + 1].center_e;
                        if (m[i, j + 1].center_n == 0)
                            m[i, j + 1].zero_count++;
                    }
                    //□□□
                    //□□■
                    //□□□
                    if (isValidPosition(i+1, j - 1))
                    {
                        m[i, j].border_n[2, 0] = m[i + 1, j - 1].center_n;
                        m[i, j].border_e[2, 0] = m[i + 1, j - 1].center_e;
                        if (m[i + 1, j - 1].center_n == 0)
                            m[i + 1, j - 1].zero_count++;
                    }
                    //□□□
                    //□□□
                    //■□□
                    if (isValidPosition(i + 1, j ))
                    {
                        m[i, j].border_n[2, 1] = m[i + 1, j].center_n;
                        m[i, j].border_e[2, 1] = m[i + 1, j].center_e;
                        if (m[i + 1, j].center_n == 0)
                            m[i + 1, j].zero_count++;
                    }
                    //□□□
                    //□□□
                    //□■□
                    if (isValidPosition(i + 1, j+1))
                    {
                        m[i, j].border_n[2, 2] = m[i + 1, j+1].center_n;
                        m[i, j].border_e[2, 2] = m[i + 1, j+1].center_e;
                        if (m[i + 1, j + 1].center_n == 0)
                            m[i + 1, j + 1].zero_count++;
                    }
                    //□□□
                    //□□□
                    //□□■
 */