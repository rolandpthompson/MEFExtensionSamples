using CoreApplication.Extensibility;
using System.ComponentModel.Composition;

namespace PluginTest1
{
    [Export(typeof(ITextExtraction))]
    public class Test1 : ITextExtraction
    {


        public string Version
        {
            get { return "1.0.0"; }
        }

        public string Author
        {
            get { return "Roland Paul Thompson"; }
        }
           
        public string Description
        {
            get { return "A test class to demonstrate how to create plugins..."; }
        }

        public string ExtractedText(string sourceText)
        {
            return "Some work done here to extract some text!";
        }


    }

}
