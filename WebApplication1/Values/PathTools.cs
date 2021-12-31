namespace WebApplication1.Values
{
    using System.IO;

    public class PathTools
    {
        private static string Root => Directory.GetCurrentDirectory();
        public static string PathDefault => "/assets/images/blog/blog1.jpg";

        public static string BlogPath = "/upload/blog";

        public static string BlogPathServer = Path.Combine(Root, $"wwwroot{BlogPath}");
    }
}
