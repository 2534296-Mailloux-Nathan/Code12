using Spectre.Console;

namespace Code12Game
{
    public static class Affichange
    {
        public static Layout GameHUD = new Layout()
            .Visible()
            .SplitColumns(
            new Layout("Left")
            .Size(103)
            .SplitRows(new Layout("view").Size(26),new Layout("desk").Update(new Panel("je suis la"))),
            new Layout("info")
            );
        //test
        public static void UpdateDesk()
        {
            GameHUD.GetLayout("Left").GetLayout("desk").Update(new Panel("je suis la mais pas le meme"));
        }
    }
}