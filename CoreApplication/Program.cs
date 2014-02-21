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

            var bootStrapper = new BootStrapper();

            var catalog = new AggregateCatalog();

            var extensionPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(Program)).Location) ?? "./Extensions";

            catalog.Catalogs.Add(new DirectoryCatalog(extensionPath));

            var container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(bootStrapper);
            }
            catch (CompositionException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            var i = 0;

            DirectoryInfo dInfo = new DirectoryInfo(@"c:\working");
            FileInfo[] files = dInfo.GetFiles("*.htm");

            foreach (var file in files)
            {
                StreamReader sr = new StreamReader(file.FullName);

                var text = sr.ReadToEnd();
                foreach (var extraction in bootStrapper.TextExtraction)
                {
                    Console.WriteLine(extraction.Author);
                    Console.WriteLine(extraction.ExtractedText(text));
                    i++;
                }
            }

            Console.WriteLine("Finished");
            Console.ReadLine();

        }
    }
}
