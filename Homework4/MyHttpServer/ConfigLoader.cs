using System.Text.Json;

namespace MyHTTPServer;

public class ConfigLoader
{
    const string configFilePath = @".\appsettings.json";
    
    public AppSettingConfig Config { get; private set; }
    
    private static void CheckConfig()
    {
        if (!File.Exists(configFilePath))
        {
            Console.WriteLine($"File {configFilePath} not found");
            throw new Exception();
        }
    }

    public ConfigLoader()
    {
        CheckConfig();
        var config = new AppSettingConfig();
        using (StreamReader jsonStream = new StreamReader(configFilePath))
        {
            config = JsonSerializer.Deserialize<AppSettingConfig>(jsonStream.BaseStream);
        }
        if (!Directory.Exists(config.StaticFilesPath))
        {
            Directory.CreateDirectory(config.StaticFilesPath);
        }
        
        if (!File.Exists(config.StaticFilesPath + "\\" + "index.html"))
        {
            Console.WriteLine("Файл index.html не найден");
        }

        Config = config;
    }
}