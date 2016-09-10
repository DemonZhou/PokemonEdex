using System;
using System.Collections.Generic;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using Entity;

namespace ExcelClass
{
    static class ExcelApi
    {
        static string filename;
        static int finishcount;
        static List<Entity.Pokemon> PokemonList = new List<Pokemon>();
        static Excel.Application excelapp;
        static Excel.Workbook exwk;
        static Excel.Worksheet exws;
        public static List<Pokemon> GetPokemonList()
        {
            return PokemonList;
        }
        public static void Init(string filename)
        {
            
            ExcelApi.filename = filename;
            if (filename == String.Empty)
                return;
            excelapp = new Excel.Application();
            excelapp.Visible = false;
            exwk = excelapp.Workbooks.Open(filename);
            exws = (Excel.Worksheet)exwk.Worksheets[1];
            
        }
        public static int GetRows()
        {
            return exws.UsedRange.Rows.Count;
        }
        
        public static int readone(int i)
        {

            Excel.Range ran = null;
            string url = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Picture";
            int rowscount = exws.UsedRange.Rows.Count;
            int colscount = exws.UsedRange.Columns.Count;
            string id = String.Empty;
            string name = String.Empty;
            string imagepath = String.Empty;
            string attr = String.Empty;
            int phattack;
            int phdefense;
            int spattack;
            int spdefense;
            int hp;
            int speed;
            int sum;
            id = ((Excel.Range)exws.Cells[i, 1]).Text.ToString();
            name = ((Excel.Range)exws.Cells[i, 2]).Text.ToString();
            imagepath = url + "\\" + name + ".png";
            if (!File.Exists(imagepath))
            {
                ran = exws.Cells[i, 3];
                ran.Select();
                ran.CopyPicture(Excel.XlPictureAppearance.xlScreen, Excel.XlCopyPictureFormat.xlBitmap);
                ParameterizedThreadStart tstart = new ParameterizedThreadStart(Saveimg);
                Thread thread = new Thread(tstart);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start(imagepath);
               
            }
            attr = ((Excel.Range)exws.Cells[i, 4]).Text.ToString();
            hp = Convert.ToInt32(((Excel.Range)exws.Cells[i, 5]).Text.ToString());
            phattack = Convert.ToInt32(((Excel.Range)exws.Cells[i, 6]).Text.ToString());
            phdefense = Convert.ToInt32(((Excel.Range)exws.Cells[i, 7]).Text.ToString());
            spattack = Convert.ToInt32(((Excel.Range)exws.Cells[i, 8]).Text.ToString());
            spdefense = Convert.ToInt32(((Excel.Range)exws.Cells[i, 9]).Text.ToString());
            speed = Convert.ToInt32(((Excel.Range)exws.Cells[i, 10]).Text.ToString());
            sum = Convert.ToInt32(((Excel.Range)exws.Cells[i, 11]).Text.ToString());
            PokemonList.Add(new Pokemon(id, name, imagepath,
                attr, phattack, phdefense, spattack,
                spdefense, hp, sum, speed));
            finishcount++;
            if (finishcount % 100 == 0 && finishcount / 100 != 0)
            {
                ExcelApi.Dispose();
                ExcelApi.Init(filename);
            }
            return finishcount;

        }
        public static void Dispose()
        {
            exwk.Close(false);
            exwk = null;
            excelapp.Quit();
            excelapp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        
        private static void Saveimg(object imgpath)
        {
            string imagepath = imgpath.ToString();
            Image img = null;
            if (Clipboard.ContainsImage())
            {
                img = Clipboard.GetImage();
                img.Save(imagepath, System.Drawing.Imaging.ImageFormat.Png);
                img.Dispose();
            }
            
        }

    }

}
