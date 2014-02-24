using CoreApplication.Extensibility;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace CoreApplication
{
    public class PluginStrapper
    {

        [ImportMany]
        public IEnumerable<ITextExtraction> TextExtraction { get; set; }

    }
}
