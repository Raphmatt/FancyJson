namespace FancyJson
{
    class Program
    {
        static void Main(string[] args)
        {


            //string filePath = Utility.GetPathJson() + "database.json";
            //for (int i = 0; i < 2; i++)
            //{
            //    if (!File.Exists(filePath))
            //    {
            //        using (StreamWriter sw = File.CreateText(filePath))
            //        {
            //            sw.WriteLine(json);
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        File.Delete(filePath);
            //    }
            //}
            while (UI.Menu()) ;
        }
    }
}