using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ScriptRunner
{
    class Program
    {
        private static readonly string connectionString = "Server=localhost;Database=Experiment;User Id=sa;Password=yourStrong(!)Password;";

        static void Main(string[] args)
        {
            var configFile = System.IO.File.ReadAllText("config.json");
            var parsedConfig = JsonConvert.DeserializeObject<Config.Config>(configFile);
            var selection = string.Empty;

            while (selection.ToLower() != "q")
            {
                Console.WriteLine("Scripts: ");
                foreach (var script in parsedConfig.Scripts)
                {
                    Console.WriteLine($"{script.Name} ({script.Command})");
                }

                Console.Write("\nScript to run: ");
                selection = Console.ReadLine().ToLower();

                var selectedScript = parsedConfig.Scripts.SingleOrDefault(s => s.Command == selection);

                if (selectedScript != null)
                {
                    var sql = System.IO.File.ReadAllText(selectedScript.Path);

                    foreach (var param in selectedScript.Params)
                    {
                        Console.Write($"{param.Name} ({param.DefaultValue}): ");
                        var enteredValue = Console.ReadLine();

                        var paramValue = string.IsNullOrEmpty(enteredValue) ? param.DefaultValue : enteredValue;

                        sql = sql.Replace($"@{param.Name}", $"'{paramValue}'");
                    }

                    using SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();

                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write($"{reader[i]} ");
                        }

                        Console.WriteLine();
                    }
                }

                Console.WriteLine("\n");
            }
        }
    }
}
