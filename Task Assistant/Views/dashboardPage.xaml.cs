namespace Task_Assistant.Views;

public partial class dashboardPage : ContentPage
{
	public dashboardPage()
	{
		InitializeComponent();

		BindingContext = new Models.AllTasks();
	}

    protected override void OnAppearing()
    {
        ((Models.AllTasks)BindingContext).LoadTasks();
    }

    private async void AddTask_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(TaskPage));
	}

	private async void tasksCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count != 0)
		{
			var task = (Models.Task)e.CurrentSelection[0];
			await Shell.Current.GoToAsync($"{nameof(TaskPage)}?{nameof(TaskPage.ItemId)}={task.Filepath}");

			tasksCollection.SelectedItem = null;
		}
	}
}