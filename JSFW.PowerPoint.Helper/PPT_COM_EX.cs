using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace JSFW.PowerPoint.Helper
{
    internal class PPT_COM_EX
    {
        /// <summary>
        /// D:\ 이지만 없으면 첫번째 drive명 C:\
        /// </summary>
        public static string DRV = GetDrive(@"D:\");

        private static string GetDrive(string drive)
        {
            if (!Directory.GetLogicalDrives().Contains(drive))
            {
                drive = Directory.GetLogicalDrives()[0];
            }
            return drive;
        }

        /// <summary>
        /// JSFW\PPT_HELP\Category
        /// </summary>
        public static string ROOT_CATEGORY_DIR = DRV + @"JSFW\PPT_HELP\Category";

        /// <summary>
        /// 96 dpi로 구현됨...
        /// </summary>
        /// <param name="pixels"></param>
        /// <returns></returns>
        internal static int PixelsToPoints(int pixels)
        {
            return pixels * 72 / 96;
        }

        internal static void ReleaseComObject(object comObject)
        {
            try
            {
                if (comObject == null) return;

                //https://www.add-in-express.com/creating-addins-blog/2013/11/05/release-excel-com-objects/
                System.Runtime.InteropServices.Marshal.ReleaseComObject(comObject);
            }
            //catch (Exception ex)
            //{

            //}
            finally
            {
                comObject = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        #region shape를 드래그 앤 드랍!!! 드랍후 해당 shape를 위치변경해줌.
        // https://stackoverflow.com/questions/23271372/how-to-dragdrop-multiple-shapes-with-a-powerpoint-2010-or-2013-add-in
         
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        internal class Win32API
        {
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetCursorPos(out POINT lpPoint);
        }

        internal static POINT GetCursorPosition(int ptr)
        {
            POINT point = new POINT();

            Win32API.GetCursorPos(out point);

            Win32API.ScreenToClient(new IntPtr(ptr), ref point);

            return point;
        }

        internal static POINT ConvertScreenPointToSlideCoordinates(POINT point, Microsoft.Office.Interop.PowerPoint.Application app)
        {
            app.ActiveWindow.Panes[2].Activate();
            // Get the screen coordinates of the upper-left hand corner of the slide.
            POINT refPointUpperLeft = new POINT(
                app.ActiveWindow.PointsToScreenPixelsX(0),
                app.ActiveWindow.PointsToScreenPixelsY(0));

            // Get the size of the slide (in points of the slide's coordinate system).
            var slide = app.ActiveWindow.View.Slide;
            var slideWidth = slide.CustomLayout.Width;
            var slideHeight = slide.CustomLayout.Height;

            // Get the screen coordinates of the bottom-right hand corner of the slide.
            POINT refPointBottomRight = new POINT(
                app.ActiveWindow.PointsToScreenPixelsX(slideWidth),
                app.ActiveWindow.PointsToScreenPixelsY(slideHeight));

            // Both reference points have to be converted to the PowerPoint window's coordinate system.
            Win32API.ScreenToClient(new IntPtr(app.ActiveWindow.HWND), ref refPointUpperLeft);
            Win32API.ScreenToClient(new IntPtr(app.ActiveWindow.HWND), ref refPointBottomRight);

            // Convert the point to the slide's coordinate system (convert the pixel coordinate inside the slide into a 0..1 interval, then scale it up by the slide's point size).
            return new POINT(
                (int)Math.Round((double)(point.X - refPointUpperLeft.X) / (refPointBottomRight.X - refPointUpperLeft.X) * slideWidth),
                (int)Math.Round((double)(point.Y - refPointUpperLeft.Y) / (refPointBottomRight.Y - refPointUpperLeft.Y) * slideHeight));
        }

        #endregion shape를 드래그 앤 드랍!!! 드랍후 해당 shape를 위치변경해줌.


        /// <summary>
        /// 실행중인 ppt에서 해당 슬라이드 객체를 얻어 넘겨준다.
        /// 넘겨받는 쪽에서 shape 생성 및 드래그처리를 한다.
        /// </summary>        
        /// <param name="linkedSlideHandlingDelegate">현재 슬라이드 처리 대리자</param>
        internal static void PassTheCreatedSlide(out bool hasException, Action<Microsoft.Office.Interop.PowerPoint.Application, _Slide> linkedSlideHandlingDelegate = null)
        {
            hasException = false;
            Microsoft.Office.Interop.PowerPoint.Application app = null;
            _Slide slide = null;
            try
            {
                app = new Microsoft.Office.Interop.PowerPoint.Application();

                float slideHeight = -1f;
                try
                {
                    slideHeight = app.ActivePresentation.PageSetup.SlideHeight;
                }
                catch (Exception ex)
                {
                    throw new Exception("PPT를 실행하여 주십시오.", ex);
                }

                app.Activate(); // 창이 숨어 있으면 켜줌.
                app.ActiveWindow.Activate();
                app.ActiveWindow.Panes[2].Activate();
                slide = app.ActiveWindow.View.Slide;// 현재 슬라이드 

                //?? 가끔 슬라이드를 제대로 못찾는 것인지 에러가 발생한다!!
                 
                linkedSlideHandlingDelegate?.Invoke(app, slide);
            }
            catch (Exception ex)
            {
                hasException = true;
                System.Diagnostics.Debug.WriteLine($"{ex}");
                System.Windows.Forms.MessageBox.Show($"{ex.Message}");
            }
            finally
            { 
                PPT_COM_EX.ReleaseComObject(slide);
                PPT_COM_EX.ReleaseComObject(app);
                 
                slide = null;
                app = null; 
            }
        }

    }

    public static class Ux
    {
        public static void SaveFile<T>(T obj, string filePath, Encoding encoding = null)
        {
            if (obj == null) return;

            if (string.IsNullOrWhiteSpace(filePath)) return;

            string dir = Path.GetDirectoryName(filePath);
            if (string.IsNullOrWhiteSpace(dir) == false && Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            File.WriteAllText(filePath, json, encoding ?? Encoding.UTF8);
        }

        public static T LoadFile<T>(string filePath, Encoding encoding = null)
        {
            T obj = default(T);

            if (string.IsNullOrWhiteSpace(filePath) == false && File.Exists(filePath) == false)
            {
                return obj;
            }

            try
            {
                string json = File.ReadAllText(filePath, encoding ?? Encoding.UTF8);
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                obj = default(T);
                System.Diagnostics.Debug.WriteLine($"{ex}");
            }
            return obj;
        }    
    }
}
