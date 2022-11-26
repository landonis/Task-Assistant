using System.Collections.ObjectModel;

namespace Task_Assistant.Models
{
    internal class AllTasks
    {
        public ObservableCollection<Task> Tasks { get; set; } = new ObservableCollection<Task>();

        public AllTasks() =>
            LoadTasks();

        public void LoadTasks()
        {
            Tasks.Clear();
            string appDataPath = FileSystem.AppDataDirectory;

            IEnumerable<Task> tasks = Directory
                .EnumerateFiles(appDataPath, "*.tasks.txt")
                .Select(filepath => new Task()
                {
                    Filepath = filepath,
                    ID = Path.GetFileName(filepath),
                    Data = File.ReadAllText(filepath),
                }) ;
            foreach (Task task in tasks)
            {
                Tasks.Add(task);
            }
            if (Tasks.Count > 0)
            {
                tasks.OrderBy(task => task.DueDate);
            }
            else
            {

            }
        }
    }
}
