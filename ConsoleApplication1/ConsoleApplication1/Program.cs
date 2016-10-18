using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using NPOI.HSSF.Record.Crypto;
using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;

namespace ConsoleApplication1
{
    class Program
    {
        private static void Main(string[] args)
        {
            var path = @"C:\Users\jueme\Desktop\1.docx";
            // Open word and a docx file
            var wordApplication = new Application() {Visible = true};
            var document = wordApplication.Documents.Open(path, Visible: true);

            foreach (Paragraph p in document.Paragraphs)
            {
                if (p.Range.Text.Contains("{Type}"))
                {
                    var text = p.Range.Text.Replace("{Type}", "ATM");
                    p.Range.Text = text;
                    //object range = p.Range;
                    //document.InlineShapes.AddHorizontalLineStandard(ref range);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                var newP = document.Paragraphs.Add();
                newP.Range.Text = "KKKKKKK"+i;
                newP.Range.InsertParagraphAfter();
                object range = newP.Range;
                document.InlineShapes.AddHorizontalLineStandard(ref range);
            }

            // Some console ascii-UI
            Console.WriteLine("Press any key to save document and close word..");
            Console.ReadLine();

            // Save settings
            document.Save();

            // Close word
            wordApplication.Quit();

        }


        //        private static void AddWordText()
        //        {
        //            object path;//文件路径
        //            string strContent;//文件内容
        //            Application wordApp;//Word应用程序变量
        //            Document wordDoc;//Word文档变量
        //            path = "d:\\myWord.doc";//保存为Word2003文档
        //            // path = "d:\\myWord.doc";//保存为Word2007文档
        //            wordApp = new Application();//初始化
        //            if (File.Exists((string)path))
        //            {
        //                File.Delete((string)path);
        //            }
        //            Object Nothing = Missing.Value;
        //            wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
        //
        //            wordApp.Selection.ParagraphFormat.LineSpacing = 35f;//设置文档的行间距
        //            //写入普通文本
        //            wordApp.Selection.ParagraphFormat.FirstLineIndent = 30;//首行缩进的长度
        //            strContent = "c#向Word写入文本   普通文本:\n";
        //            wordDoc.Paragraphs.Last.Range.Text = strContent;
        //
        //            //将文档的前三个字替换成"asdfasdf"，并将其颜色设为蓝色
        //            object start = 0;
        //            object end = 3;
        //            Microsoft.Office.Interop.Word.Range rang = wordDoc.Range(ref start, ref end);
        //            rang.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorBrightGreen;
        //            rang.Text = "我是替换文字";
        //            wordDoc.Range(ref start, ref end);
        //
        //
        //
        //            //写入黑体文本
        //            object unite = Microsoft.Office.Interop.Word.WdUnits.wdStory;
        //            wordApp.Selection.EndKey(ref unite, ref Nothing);
        //            wordApp.Selection.ParagraphFormat.FirstLineIndent = 0;//取消首行缩进的长度
        //            strContent = "黑体文本\n ";//在文本中使用'\n'换行
        //            wordDoc.Paragraphs.Last.Range.Font.Name = "黑体";
        //            wordDoc.Paragraphs.Last.Range.Text = strContent;
        //            // wordApp.Selection.Text = strContent;
        //            //写入加粗文本
        //            strContent = "加粗文本\n ";
        //            wordApp.Selection.EndKey(ref unite, ref Nothing);
        //            wordDoc.Paragraphs.Last.Range.Font.Bold = 1;//Bold=0为不加粗
        //            wordDoc.Paragraphs.Last.Range.Text = strContent;
        //            //  wordApp.Selection.Text = strContent;
        //            //写入15号字体文本
        //            strContent = "15号字体文本\n ";
        //            wordApp.Selection.EndKey(ref unite, ref Nothing);
        //
        //            wordDoc.Paragraphs.Last.Range.Font.Size = 15;
        //            wordDoc.Paragraphs.Last.Range.Text = strContent;
        //            //写入斜体文本
        //            strContent = "斜体文本\n ";
        //            wordApp.Selection.EndKey(ref unite, ref Nothing);
        //            wordDoc.Paragraphs.Last.Range.Font.Italic = 1;
        //            wordDoc.Paragraphs.Last.Range.Text = strContent;
        //            //写入蓝色文本
        //            strContent = "蓝色文本\n ";
        //            wordApp.Selection.EndKey(ref unite, ref Nothing);
        //            wordDoc.Paragraphs.Last.Range.Font.Color = MSWord.WdColor.wdColorBlue;
        //            wordDoc.Paragraphs.Last.Range.Text = strContent;
        //            //写入下划线文本
        //            strContent = "下划线文本\n ";
        //            wordApp.Selection.EndKey(ref unite, ref Nothing);
        //            wordDoc.Paragraphs.Last.Range.Font.Underline = MSWord.WdUnderline.wdUnderlineThick;
        //            wordDoc.Paragraphs.Last.Range.Text = strContent;
        //
        //            object format = MSWord.WdSaveFormat.wdFormatDocument;
        //            wordDoc.SaveAs(ref path, ref format, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
        //            wordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
        //            wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
        //            Response.Write("<script> alert('" + path + ": Word文档写入文本完毕!');</script>");
        //        }
    }
}
