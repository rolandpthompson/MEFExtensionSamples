using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            foreach (var extraction in bootStrapper.TextExtraction)
            {
                Console.WriteLine(extraction.Author);
                Console.WriteLine(extraction.ExtractedText()
                i++;
            }

        }
    }
}
