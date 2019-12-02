using System.Collections.Generic;
using System.IO;

namespace RicercaLocalizzata.Data
{
    public class MyItemSerializer
    {
        public async void Save(IEnumerable<MyItem> items, string path)
        {
            using(TextWriter tw = File.CreateText(path))
            {
                foreach (var i in items)
                { 
                    await tw.WriteLineAsync(i.ToString()); 
                }
            }
        }
    }
}
