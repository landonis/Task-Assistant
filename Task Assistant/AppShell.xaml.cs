namespace Task_Assistant;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		//Register the TaskPage
		Routing.RegisterRoute(nameof(Views.TaskPage), typeof(Views.TaskPage));
	}
}
