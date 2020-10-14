using System;
using System.Collections.Generic;
using System.Text;

namespace CsetAnalytics.ViewModels.Dashboard
{
    public class TreeView
    {
        public string Name { get; set; }
        public List<TreeView> Children { get; set; }
    }
}
