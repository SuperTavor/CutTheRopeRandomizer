class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Please drag the content/map folder of the game onto the executable.");
            Console.ReadKey();
            Environment.Exit(1);
        }

        string folderPath = args[0];
        Program program = new Program();

        string tempFolderPath = Path.Combine(folderPath, "temp");
        if (!Directory.Exists(tempFolderPath))
        {
            Directory.CreateDirectory(tempFolderPath);

            string[] fullNames = Directory.GetFiles(folderPath);
            string[] fileNames = program.GetFileNames(folderPath);

            Random random = new Random();
            List<string> usedNames = new List<string>();

            foreach (string fileName in fileNames)
            {
                int randomIndex = random.Next(0, fileNames.Length);

                while (usedNames.Contains(fileNames[randomIndex]))
                {
                    randomIndex = random.Next(0, fileNames.Length);
                }

                string oldFilePath = Path.Combine(folderPath, fileName);
                string newFileName = fileNames[randomIndex];
                string newFilePath = Path.Combine(tempFolderPath, newFileName);

                File.Move(oldFilePath, newFilePath);
                usedNames.Add(newFileName);
            }
            foreach(var file in Directory.GetFiles(folderPath))
            {
                File.Delete(file);
            }
            foreach(var file in Directory.GetFiles(Path.Combine(folderPath,"temp")))
            {
                File.Copy(file, folderPath + "\\" +Path.GetFileName(file),true);
            }
            Directory.Delete(Path.Combine(folderPath,"temp"), true);
        }
        else
        {
            Console.WriteLine("The 'temp' folder already exists. Please remove or rename it and run the program again.");
        }
    }

    string[] GetFileNames(string folder)
    {
        string[] files = Directory.GetFiles(folder);
        string[] output = new string[files.Length];

        for (int i = 0; i < files.Length; i++)
        {
            output[i] = Path.GetFileName(files[i]);
        }

        return output;
    }
}
