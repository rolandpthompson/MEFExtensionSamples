using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;

namespace CoreApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            var plugins = new PluginStrapper();
            var catalog = new AggregateCatalog();
            var extensionPath = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(Program)).Location), "Plugins");

            // check if directory exists...
            DirectoryInfo dInfo = new DirectoryInfo(extensionPath);
            if (!dInfo.Exists)
                dInfo.Create();
            
            catalog.Catalogs.Add(new DirectoryCatalog(extensionPath));

            var container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(plugins);
            }
            catch (CompositionException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            var i = 0;

            dInfo = new DirectoryInfo(@"c:\working");
            FileInfo[] files = dInfo.GetFiles("*.htm");

            foreach (var file in files)
            {
                StreamReader sr = new StreamReader(file.FullName);

                var text = sr.ReadToEnd();
                foreach (var extraction in plugins.TextExtraction)
                {
                    //Console.WriteLine(extraction.Author);
                    Console.WriteLine(extraction.ExtractedText(text));
                    i++;
                }
            }

            Console.WriteLine("Finished");
            Console.ReadLine();

        }
    }
}
