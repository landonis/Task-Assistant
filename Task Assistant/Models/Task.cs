namespace Task_Assistant.Models
{
    //data parameter needs synced
    internal class Task
    {
        private string data;
        public string Data
        {
            get
            {
                data = "Title%#$#" + Title + "%#$#Description%#$#" + Description + "%#$#DueTime%#$#" + DueTime.ToString() + "%#$#DueDate%#$#" + DueDate.ToString();
                return data;
            }
            set //called when assigning new data, will need to always repopulate task object parameters
            {
                data = value;
                string[] values = data.Split("%#$#");
                for (int itr = 0; itr < values.Length; itr++)
                {
                    switch (values[itr])
                    {
                        case "Title":
                            itr++; Title = values[itr]; break;
                        case "Description":
                            itr++; Description = values[itr]; break;
                        case "DueTime":
                            itr++; DueTime = TimeSpan.Parse(values[itr]); break;
                        case "DueDate":
                            itr++; DueDate = DateTime.Parse(values[itr]); break;
                    }
                }
                DueDate += DueTime;
            }
        }

        
        public string Filepath { get; set; }
        public string ID { get; set; }
        private string descriptive_id;
        public string Descriptive_ID
        {
            get { return descriptive_id; }
            set { descriptive_id = "File Name: " + value; } 
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan DueTime { get; set; }
        public DateTime DueDate { get; set; }
    }
}
