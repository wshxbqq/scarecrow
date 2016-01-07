using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Drawing;

namespace SeleniumHelper
{
    public class MyWebDriver
    {
        public static string BASE_IMG_PATH = "D:/scarecrow/";

        public static string SHOOT_IMG_PATH = "D:/scarecrow/shoot/";
        public static string TPL_IMG_PATH = "D:/scarecrow/tpl/";
        public static string ERROR_IMG_PATH = "D:/scarecrow/error/";

 
        public static void getShootImg(Entity.TbTasks task)
        {
            if (!Directory.Exists(BASE_IMG_PATH))
            {
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

            switch (task.TaskType.Trim())
            {
                case "WebKit": SeleniumHelper.MyWebDriver.handelPhantomjs(task); break;
                case "Gecko": SeleniumHelper.MyWebDriver.handelFireFox(task) ; break;
                    //case "IE": driver = new InternetExplorerDriver(); break;
            }
        }

        public static void handelFireFox(Entity.TbTasks task) {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl(task.TaskUrl);
            driver.Manage().Window.Size = new System.Drawing.Size(task.TaskWidth, task.TaskHeight);

            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            if (task.TaskBeforeScript.Trim() != "")
            {
                jse.ExecuteScript(task.TaskBeforeScript);
            }
            jse.ExecuteScript("setInterval(function () { window.scrollBy(0, 20)}, 10);");



            System.Threading.Thread.Sleep(task.TaskDelayTime == 0 ? 1000 : task.TaskDelayTime);




            string targetPng = SHOOT_IMG_PATH + task.TaskId + ".png";
            if (File.Exists(targetPng))
            {
                File.Delete(targetPng);
            }


            Stream stream = new MemoryStream(((ITakesScreenshot)driver).GetScreenshot().AsByteArray);

            driver.Close();
            driver.Dispose();



            Bitmap bm = new Bitmap(stream);
            Bitmap bm1 = Cut(bm, 0, 0, task.TaskWidth, task.TaskHeight);
            bm1.Save(targetPng);
            bm.Dispose();
            bm1.Dispose();

        }

        public static void handelPhantomjs(Entity.TbTasks task) {
            IWebDriver driver = new PhantomJSDriver();
            driver.Navigate().GoToUrl(task.TaskUrl);
            driver.Manage().Window.Size = new System.Drawing.Size(task.TaskWidth, task.TaskHeight);

            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            if (task.TaskBeforeScript.Trim() != "")
            {
                jse.ExecuteScript(task.TaskBeforeScript);
            }
            jse.ExecuteScript("setInterval(function () { window.scrollBy(0, 20)}, 10);");



            System.Threading.Thread.Sleep(task.TaskDelayTime == 0 ? 10000 : task.TaskDelayTime);




            string targetPng = SHOOT_IMG_PATH + task.TaskId + ".png";
            if (File.Exists(targetPng))
            {
                File.Delete(targetPng);
            }


            Stream stream = new MemoryStream(((ITakesScreenshot)driver).GetScreenshot().AsByteArray);

            driver.Close();
            driver.Dispose();



            Bitmap bm = new Bitmap(stream);
            Bitmap bm1 = Cut(bm, 0, 0, task.TaskWidth, task.TaskHeight);
            bm1.Save(targetPng);
            
            bm.Dispose();
            bm1.Dispose();
            stream.Close();
            stream.Dispose();


        }



        //public static Bitmap GetEntereScreenshot()
        //{

        //    Bitmap stitchedImage = null;
        //    try
        //    {
        //        long totalwidth1 = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.offsetWidth");//documentElement.scrollWidth");

        //        long totalHeight1 = (long)((IJavaScriptExecutor)driver).ExecuteScript("return  document.body.parentNode.scrollHeight");

        //        int totalWidth = (int)totalwidth1;
        //        int totalHeight = (int)totalHeight1;

        //        // Get the Size of the Viewport
        //        long viewportWidth1 = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.clientWidth");//documentElement.scrollWidth");
        //        long viewportHeight1 = (long)((IJavaScriptExecutor)driver).ExecuteScript("return window.innerHeight");//documentElement.scrollWidth");

        //        int viewportWidth = (int)viewportWidth1;
        //        int viewportHeight = (int)viewportHeight1;


        //        // Split the Screen in multiple Rectangles
        //        List<Rectangle> rectangles = new List<Rectangle>();
        //        // Loop until the Total Height is reached
        //        for (int i = 0; i < totalHeight; i += viewportHeight)
        //        {
        //            int newHeight = viewportHeight;
        //            // Fix if the Height of the Element is too big
        //            if (i + viewportHeight > totalHeight)
        //            {
        //                newHeight = totalHeight - i;
        //            }
        //            // Loop until the Total Width is reached
        //            for (int ii = 0; ii < totalWidth; ii += viewportWidth)
        //            {
        //                int newWidth = viewportWidth;
        //                // Fix if the Width of the Element is too big
        //                if (ii + viewportWidth > totalWidth)
        //                {
        //                    newWidth = totalWidth - ii;
        //                }

        //                // Create and add the Rectangle
        //                Rectangle currRect = new Rectangle(ii, i, newWidth, newHeight);
        //                rectangles.Add(currRect);
        //            }
        //        }

        //        // Build the Image
        //        stitchedImage = new Bitmap(totalWidth, totalHeight);
        //        // Get all Screenshots and stitch them together
        //        Rectangle previous = Rectangle.Empty;
        //        foreach (var rectangle in rectangles)
        //        {
        //            // Calculate the Scrolling (if needed)
        //            if (previous != Rectangle.Empty)
        //            {
        //                int xDiff = rectangle.Right - previous.Right;
        //                int yDiff = rectangle.Bottom - previous.Bottom;
        //                // Scroll
        //                //selenium.RunScript(String.Format("window.scrollBy({0}, {1})", xDiff, yDiff));
        //                ((IJavaScriptExecutor)driver).ExecuteScript(String.Format("window.scrollBy({0}, {1})", xDiff, yDiff));
        //                System.Threading.Thread.Sleep(200);
        //            }

        //            // Take Screenshot
        //            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

        //            // Build an Image out of the Screenshot
        //            Image screenshotImage;
        //            using (MemoryStream memStream = new MemoryStream(screenshot.AsByteArray))
        //            {
        //                screenshotImage = Image.FromStream(memStream);
        //            }

        //            // Calculate the Source Rectangle
        //            Rectangle sourceRectangle = new Rectangle(viewportWidth - rectangle.Width, viewportHeight - rectangle.Height, rectangle.Width, rectangle.Height);

        //            // Copy the Image
        //            using (Graphics g = Graphics.FromImage(stitchedImage))
        //            {
        //                g.DrawImage(screenshotImage, rectangle, sourceRectangle, GraphicsUnit.Pixel);
        //            }

        //            // Set the Previous Rectangle
        //            previous = rectangle;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // handle
        //    }
        //    return stitchedImage;
        //}




        static bool isApproximate(int a,int b) {
            if (Math.Abs(a-b)<20) {
                return true;
            }

            return false;

        }


        public static Bitmap Cut(Bitmap b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }
            int w = b.Width;
            int h = b.Height;
 
            
                Bitmap bmpOut = new Bitmap(iWidth, iHeight,System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmpOut);
                g.Clear(Color.White);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
           
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

            int x = 0;
            int y = 0;
            for (int _w = 0; _w < bmTpl.Width; _w++)
            {
                for (int _h = 0; _h < bmTpl.Height; _h++)
                {
                    var shootPx = bmShoot.GetPixel(_w, _h);
                    var tplPx = bmTpl.GetPixel(_w, _h);
                    if (tplPx.A > 0)
                    {
                        if (!isApproximate(shootPx.R, tplPx.R) || !isApproximate(shootPx.G, tplPx.G)   || !isApproximate(shootPx.B, tplPx.B))
                        {
                            result = false;
                            x = _w;
                            y = _h;
                            break;
                        }
                    }

                }
            }
            Dao.TbTasks dao_task = new Dao.TbTasks();

            if (!result)
            {
                if (!Directory.Exists(ERROR_IMG_PATH + task.TaskId))
                {
                    Directory.CreateDirectory(ERROR_IMG_PATH + task.TaskId);
                }
                string errorImg = ERROR_IMG_PATH + task.TaskId + "/" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png";

                shootFi.CopyTo(errorImg);
                task.TaskErrorCount++;

                Dao.TbError dao_error = new Dao.TbError();

                Entity.TbError entity_error = new Entity.TbError();

                entity_error.ErrorTime = DateTime.Now;
                entity_error.ErrorX = x;
                entity_error.ErrorY = y;
                entity_error.ErrorTaskId = task.TaskId;
                entity_error.ErrorImg = errorImg;

                dao_error.Add(entity_error);


                var errorToday= dao_error.GetListToday("error_time");
                int errorCount = 0;
                foreach (Entity.TbError err in errorToday) {
                    if (err.ErrorTaskId== task.TaskId) {
                        errorCount++;
                    }
                }
                if (errorCount==1) {
                    

                }








                Console.WriteLine("error! " + task.TaskUrl);

            }
            else {
                task.TaskMonitoringCount++;
                Console.WriteLine("done! "+ task.TaskUrl);

            }
            dao_task.AddOrUpdate(task);


            bmShoot.Dispose();
            bmTpl.Dispose();

        }
    }
}
