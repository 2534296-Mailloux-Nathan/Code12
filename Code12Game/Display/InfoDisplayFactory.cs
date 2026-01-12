using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code12Game.Display
{
    public static class InfoDisplayFactory
    {
        public static Layout CreatInfoLayout()
        {
            var infoPanel = new Panel("Info Panel Content")
            {
                Border = BoxBorder.Rounded,
                Header = new PanelHeader("Information", Justify.Center)
            };
            var layout = new Layout("info")
                .Update(infoPanel)
                .Ratio(1)
                .Visible();
            return layout;
        }
    }
}
