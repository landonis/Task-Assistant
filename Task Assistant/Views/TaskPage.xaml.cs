namespace Task_Assistant.Views;
[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class TaskPage : ContentPage
{
	private readonly string _Archive_Directory = Path.Combine(FileSystem.AppDataDirectory, "Archive");
    private readonly string _Completed_Directory = Path.Combine(FileSystem.AppDataDirectory, "Completed");

    private string file_id;
    public string ItemId
	{
		set { LoadTask(value); }
	}
	public TaskPage()
	{
		InitializeComponent();

		string appDataPath = FileSystem.AppDataDirectory;
		string randomFileName = $"{Path.GetRandomFileName()}.tasks.txt";

        LoadTask(Path.Combine(appDataPath, randomFileName));
	}


	//takes a path as a parameter
	//will create a task object bounded to the xaml task page
	private void LoadTask(string filePath)
	{
        file_id = Path.GetFileName(filePath);
        Models.Task taskModel = new Models.Task();

		taskModel.Filepath = filePath;
        taskModel.DueDate= DateTime.Now;
        taskModel.ID = file_id;
        if (File.Exists(filePath))
		{
            taskModel.Data = File.ReadAllText(taskModel.Filepath);
        }
		
		BindingContext=taskModel;
	}
	
	
	//will return and handle creating new archive paths in the format of year-month
	private string GetDatedArchivePath()
	{
		DateTime currentDate = DateTime.Now;
		
		//Check for Archive Directory Presence
		if (Directory.Exists(_Archive_Directory)!=true)
		{
			Directory.CreateDirectory(_Archive_Directory);
		}


		//generate an archive path to format
        string DatedArchivePath = Path.Combine(_Archive_Directory, currentDate.Year.ToString() + "-" + currentDate.Month.ToString());

        //Check for datedArchivePath directory inside of Archive Path
        if (Directory.Exists(DatedArchivePath)!=true) 
		{
			Directory.CreateDirectory(DatedArchivePath);
		}


		//returns a validated archive path
		return DatedArchivePath;
	}


    //will return and handle creating new completed paths in the format of year-month
    private string GetDatedCompletedPath()
    {
        DateTime currentDate = DateTime.Now;

        //Check for Archive Directory Presence
        if (Directory.Exists(_Completed_Directory) != true)
        {
            Directory.CreateDirectory(_Completed_Directory);
        }


        //generate a Completed path to format
        string DatedCompletedPath = Path.Combine(_Completed_Directory, currentDate.Year.ToString() + "-" + currentDate.Month.ToString());

        //Check for datedCompletedPath directory inside of Completed Path
        if (Directory.Exists(DatedCompletedPath) != true)
        {
            Directory.CreateDirectory(DatedCompletedPath);
        }


        //returns a validated Completed path
        return DatedCompletedPath;
    }


    //binded to the save button on the task configuration page
    private async void SaveTask_Clicked(object sender, EventArgs e)
	{
		//will check if object bounded to referencing object is a task object
		//will assign that task object's data attribute the text pulled from the file
		//will return to the previous page

		if (BindingContext is Models.Task task)
		{
			File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, file_id).ToString(), task.Data);
		}

		await Shell.Current.GoToAsync("..");
	}


	//binded to the delete button on task configuration page
    private async void DeleteTask_Clicked(object sender, EventArgs e)
    {
        //will check if object bounded to referencing object is a task object
        //will check if a file exists in relation to that tasks File ID to the app data directory
		//if it exists it will remove the file
        //will return to the previous page
        if (BindingContext is Models.Task task)
		{
			if (File.Exists(Path.Combine(FileSystem.AppDataDirectory, task.ID)))
			{
				File.Delete(Path.Combine(FileSystem.AppDataDirectory, task.ID));
			}
		}

		await Shell.Current.GoToAsync("..");
    }
    
	
	//binded to the archive button on task configuration page
	private async void ArchiveTask_Clicked(object sender, EventArgs e)
    {
        //will check if object bounded to referencing object is a task object
        //will check if a file exists in relation to that tasks File ID to the app data directory
        //if it exists it will move the file to the archive hierarchy
        //will return to the previous page

        if (BindingContext is Models.Task task)
		{
			if (File.Exists(Path.Combine(FileSystem.AppDataDirectory, task.ID)))
			{
				string Archive = GetDatedArchivePath();
				string ArchivePath = Path.Combine(Path.Combine(FileSystem.AppDataDirectory, task.ID), Archive);
				File.Move(Path.Combine(FileSystem.AppDataDirectory, task.ID), Path.Combine(ArchivePath, task.ID));
			}
		}	
        await Shell.Current.GoToAsync("..");
    }
    

	private async void CompletedTask_Clicked(object sender, EventArgs e)
    {
        //will check if object bounded to referencing object is a task object
        //will check if a file exists in relation to that tasks File ID to the app data directory
        //if it exists it will move the file to the completed hierarchy
        //will return to the previous page

        if (BindingContext is Models.Task task)
        {
            if (File.Exists(Path.Combine(FileSystem.AppDataDirectory, task.ID)))
            {
                string Completed = GetDatedCompletedPath();
                string CompletedPath = Path.Combine(Path.Combine(FileSystem.AppDataDirectory, task.ID), Completed);
                File.Move(Path.Combine(FileSystem.AppDataDirectory, task.ID), Path.Combine(CompletedPath, task.ID));
            }
        }

        await Shell.Current.GoToAsync("..");
    }
}