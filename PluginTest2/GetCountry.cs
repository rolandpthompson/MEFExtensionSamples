using CoreApplication.Extensibility;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace PluginTest2
{
    [Export(typeof(ITextExtraction))]
    public class GetCountry : ITextExtraction
    {
        public string Version
        {
            get { return "1.0.0"; }
        }

        public string Author
        {
            get { return "Roland Thompson"; }
        }

        public string Description
        {
            get { return "Extracts the Country Name from specific Html formatted Text."; }
        }

        public string ExtractedText(string sourceText)
        {
            // We want to extract the address...
            return Country(sourceText);
        }


        /// <summary>
        /// Get the address
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private string[] GetAddress(string text)
        {

            List<string> address = new List<string>();

            // get the sections only..
            string startText = "<div id=\"htmldelivery\"";
            int startPos = text.IndexOf(startText);
            string remainingText = text.Substring(startPos + startText.Length + 1, text.Length - (startPos + startText.Length + 1));

            int endPos = remainingText.IndexOf("</div>");

            // get the text
            remainingText = text.Substring(startPos + startText.Length + 1, endPos);

            // get the last break
            int lastBrRef = remainingText.LastIndexOf("<br>");

            // only use text to the last break
            remainingText = remainingText.Substring(0, lastBrRef);

            // remove Delivery Address text 
            remainingText = remainingText.Replace("<legend>Delivery Address</legend>", string.Empty);

            // Convert the Html to Text
            HtmlToText htmlConvert = new HtmlToText();

            // split down
            string[] stringSeparators = new string[] { "<br>" };
            string[] split = remainingText.Split(stringSeparators, StringSplitOptions.None);

            for (int i = 0; i < split.Length; i++)
                split[i] = htmlConvert.ConvertHtml(split[i]).Trim();

            // store the sections text
            return split;

        }

        /// <summary>
        /// Return the last part of the address (which will be the country!)
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private string Country(string text)
        {
            string[] address = GetAddress(text);
            return address[address.Length - 1];
        }
    }
}
