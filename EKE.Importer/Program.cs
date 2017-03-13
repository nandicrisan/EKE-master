namespace EKE.Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            //var importer = new Importer();
            //importer.Start();
            var formater = new HtmlFormatter();
            formater.ReplaceBr();
        }
    }
}
