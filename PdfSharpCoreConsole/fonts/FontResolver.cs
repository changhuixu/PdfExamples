using System;
using System.IO;
using PdfSharpCore.Fonts;

namespace PdfSharpCoreConsole.fonts
{
    public static class FontNames
    {
        public const string Arial = nameof(Arial);
        public const string MrvCode39S = nameof(MrvCode39S);
        public const string SegoeWP = nameof(SegoeWP);
    }
    public class FontResolver : IFontResolver
    {
        public string DefaultFontName => FontNames.Arial;

        public byte[] GetFont(string faceName)
        {
            using (var ms = new MemoryStream())
            {
                using (var fs = File.Open(faceName, FileMode.Open))
                {
                    fs.CopyTo(ms);
                    ms.Position = 0;
                    return ms.ToArray();
                }
            }
        }
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals(FontNames.SegoeWP, StringComparison.CurrentCultureIgnoreCase))
            {
                if (isBold && isItalic)
                {
                    return new FontResolverInfo("./fonts/SegoeWP-Light.ttf", true, true);
                }
                else if (isBold)
                {
                    return new FontResolverInfo("./fonts/SegoeWP-Bold.ttf");
                }
                else if (isItalic)
                {
                    return new FontResolverInfo("./fonts/SegoeWP-Light.ttf", false, true);
                }
                else
                {
                    return new FontResolverInfo("./fonts/SegoeWP-Light.ttf");
                }
            }
            if (familyName.Equals(FontNames.Arial, StringComparison.CurrentCultureIgnoreCase))
            {
                return new FontResolverInfo("./fonts/arial.ttf");
            }
            if (familyName.Equals(FontNames.MrvCode39S, StringComparison.CurrentCultureIgnoreCase))
            {
                return new FontResolverInfo("./fonts/mrvcode39s.ttf");
            }
            return null;
        }
    }
}
