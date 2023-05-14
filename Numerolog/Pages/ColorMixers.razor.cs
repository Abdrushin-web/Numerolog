using Colorology;
using Colourful;
using System.Globalization;

namespace Numerolog.Pages
{
    public partial class ColorMixers
    {
        string Ratio
        {
            get => ratio.ToString(CultureInfo.InvariantCulture);
            set => ratio = double.Parse(value, CultureInfo.InvariantCulture);
        }

        string?
            color1 = MacbethColorChecker.Red.ToHtml(),
            color2 = MacbethColorChecker.Blue.ToHtml();
        double ratio = 0.5;
    }
}