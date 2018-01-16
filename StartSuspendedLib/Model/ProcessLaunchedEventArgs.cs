namespace StartSuspendedLib.Model
{
    public class ProcessLaunchedEventArgs
    {
        public ProcessLaunchedEventArgs(bool started, string message, string title)
        {
            this.Message = message;
            Started = started;
            this.Title = title;
        }

        public bool Started { get; }

        public string Message { get; }
        public string Title { get; }

    }
}
