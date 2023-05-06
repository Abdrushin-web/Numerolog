using System.Globalization;

namespace Numerology
{
    /// <summary>
    /// <see cref="Alphabet"/>s
    /// </summary>
    public static class Alphabets
    {
        /// <summary>
        /// German spiritualy received alphabet by Lucien Siffrid and Hermann Wenng from Lord Abdrushin`s circle
        /// </summary>
        public static readonly Alphabet German = new(
            CultureInfo.GetCultureInfo("de-DE"),
            new TextNumber[]
            {
                ("A Ä", 100),
                ("E", 10),
                ("S", 1),
                ("C", 200),
                ("D", 20),
                ("Q ß", 2),
                ("H", 300),
                ("R", 30),
                ("F", 400),
                ("T", 40),
                ("G", 500),
                ("M", 50),
                ("U Ü", 5),
                ("I J", 600),
                ("L", 60),
                ("W", 6),
                ("K", 700),
                ("V", 70),
                ("Z", 7),
                ("P", 800),
                ("X", 80),
                ("Y", 8),
                ("B", 900),
                ("N", 90),
                ("O Ö", 9),
            });
    }
}
