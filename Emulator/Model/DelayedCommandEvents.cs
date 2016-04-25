using System;

namespace Emulator.Model
{
    public static class DelayedCommandEvents
    {
        public static readonly Guid MouseDown = new Guid("BBADC2B5-F994-4178-B006-F4BDEFED9E62");
        public static readonly Guid MouseUp = new Guid("56396AA1-BB6B-4CF1-AB98-7C7AF6F738F5");
        public static readonly Guid Click = new Guid("924FF373-C394-461E-B74C-9E81C81B2EE9");
        public static readonly Guid DoubleClick = new Guid("6C50F05D-05FA-4979-B8E3-F0FB7E1378CF");
    }
}