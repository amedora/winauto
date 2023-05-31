using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinAuto.Infrastructure.Automa;

namespace NotepadDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var targetWindow = "無題 - メモ帳";
            var window = AutomaWindowFinder.FindWindowByName(targetWindow);

            if (window == null)
            {
                Console.WriteLine("対象のウィンドウが見つかりませんでした。");
                Console.ReadLine();
                return;
            }
            else
            {
                Console.WriteLine("対象のウィンドウを見つけました。");
            }

            var prop = new MyProperty
            {
                Text = "12345678",
            };

            var maps = window.Map<MyProperty>();
            maps.AddMapping("15", p => p.Text);

            maps.Update(prop);

            //var button = window.Button("Item 1");
            //button.Click();

            Console.WriteLine("Enterキーで終了します。");
            Console.ReadLine();
        }
    }

    class MyProperty
    {
        public string Text { get; set; }
    }
}
