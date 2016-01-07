using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Drawing;

namespace scarecrow
{
   
    class Warden
    {

        public static string BASE_IMG_PATH = "D:/scarecrow/";

        public static string SHOOT_IMG_PATH = "D:/scarecrow/shoot/";
        public static string TPL_IMG_PATH = "D:/scarecrow/tpl/";
        public static string ERROR_IMG_PATH = "D:/scarecrow/error/";


        public static void  getShootImg(Entity.TbTasks task) {
            if (!Directory.Exists(BASE_IMG_PATH)) {
                Directory.CreateDirectory(BASE_IMG_PATH);
            }
            if (!Directory.Exists(SHOOT_IMG_PATH))
            {
                Directory.CreateDirectory(SHOOT_IMG_PATH);
            }
            if (!Directory.Exists(TPL_IMG_PATH))
            {
                Directory.CreateDirectory(TPL_IMG_PATH);
            }
            if (!Directory.Exists(ERROR_IMG_PATH))
            {
                Directory.CreateDirectory(ERROR_IMG_PATH);
            }
            IWebDriver driver = new InternetExplorerDriver();

            switch (task.TaskType) {
                case "Firefox": driver = new FirefoxDriver() ; break;
                case "Chrome": driver = new ChromeDriver(); break;
                case "IE": driver = new InternetExplorerDriver(); break;
           

            }
            


            driver.Navigate().GoToUrl(task.TaskUrl);
            driver.Manage().Window.Size = new System.Drawing.Size(task.TaskWidth, task.TaskHeight);
            if (task.TaskBeforeScript.Trim()!="") {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript(task.TaskBeforeScript);
            }

            if (task.TaskDelayTime>0) {
                System.Threading.Thread.Sleep(task.TaskDelayTime);
            }


            string targetPng = SHOOT_IMG_PATH + task.TaskId + ".png";
            if (File.Exists(targetPng)) {
                File.Delete(targetPng);
            }

            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(targetPng, System.Drawing.Imaging.ImageFormat.Png);

            driver.Close();
 

        }

       

        public static void handleImg(Entity.TbTasks task)
        {
            string targetPng = SHOOT_IMG_PATH + task.TaskId + ".png";
            string tplPng = TPL_IMG_PATH + task.TaskId + ".png";

            FileInfo shootFi = new FileInfo(targetPng);
            FileInfo tplFi = new FileInfo(tplPng);

            Bitmap bmShoot = new Bitmap(shootFi.FullName);
            Bitmap bmTpl = new Bitmap(tplFi.FullName);



            bool result = true;
            for (int _w=0;_w< bmTpl.Width;_w++) {
                for (int _h=0;_h< bmTpl.Height;_h++) {
                    var shootPx = bmShoot.GetPixel(_w,_h);
                    var tplPx = bmTpl.GetPixel(_w, _h);
                    if (tplPx.A>=0) {
                        if (shootPx.R != tplPx.R || shootPx.G != tplPx.G || shootPx.B != tplPx.B)
                        {
                            result = false;
                            break;
                        }
                    }
                    
                }
            }

            if (!result) {
                if (!Directory.Exists(ERROR_IMG_PATH + task.TaskId)) {
                    Directory.CreateDirectory(ERROR_IMG_PATH + task.TaskId);
                }
                string errorImg = ERROR_IMG_PATH + task.TaskId + "/" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png";
                
                shootFi.CopyTo(errorImg);
                task.TaskErrorCount++;
                Dao.TbTasks dao_task = new Dao.TbTasks();
                dao_task.AddOrUpdate(task);
            }


        }


    }
 
}
