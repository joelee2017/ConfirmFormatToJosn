using Newtonsoft.Json;
class Program
{
    static void Main(string[] args)
    {    
        do
        {
            Console.WriteLine("程式開始執行\n");
            Console.Write("請輸入路徑：");
            string directoryPath = Console.ReadLine();
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("請輸入正確的路徑!");
            }
            // 遍歷目錄及其子目錄，查找所有 JSON 檔案 
            foreach (var filePath in Directory.EnumerateFiles(directoryPath, "*.json", SearchOption.AllDirectories))
            {
                try
                {
                    // 讀取檔案內容 
                    string json = File.ReadAllText(filePath);
                    // 建立 JsonSerializerSettings 物件，設定 Error 事件處理常式 
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.Error += (sender, args) =>
                    {
                        // 在控制台上印出轉換失敗的錯誤訊息 
                        Console.WriteLine("轉換 JSON 資料失敗: {0}", args.ErrorContext.Error.Message);
                        // 將處理設定為已處理 
                        args.ErrorContext.Handled = true;
                    };
                    // 將 JSON 資料轉換成物件 
                    object obj = JsonConvert.DeserializeObject(json, settings);
                    // 在控制台上印出來源路徑及檔名，以及轉換後的物件內容 
                    Console.WriteLine("檔案來源: {0}", filePath);
                    Console.WriteLine("JSON 資料內容: {0}\n", obj.ToString());
                }
                catch (JsonException ex)
                {
                    // 在控制台上印出轉換失敗的錯誤訊息 
                    Console.WriteLine("轉換 JSON 資料失敗: {0}", ex.Message);
                }
            }
            Console.WriteLine("程式執行完畢");
            Console.WriteLine("請按下 Enter 鍵再次執行程式，或輸入其他字串離開");
            // 讀取使用者輸入的文字 
            string input = Console.ReadLine();
            // 如果使用者輸入的文字不是空白，則離開程式 
            if (!string.IsNullOrWhiteSpace(input))
            {
                break;
            }
            Console.WriteLine("程式重新開始執行");
        } while (true);
    }
}