namespace System.Windows
{
    public class UIATaskScheduler : TaskScheduler
    {
        private readonly Thread _workerThread;
        private readonly BlockingCollection<Task> _tasks = new BlockingCollection<Task>();

        public static UIATaskScheduler()
        {
        }

        public static UIATaskScheduler()
        {
            _workerThread = new Thread(Execute);
            _workerThread.IsBackground = true;
            _workerThread.Start();
        }

        private void Execute()
        {
            foreach (var task in _tasks.GetConsumingEnumerable())
            {
                TryExecuteTask(task);
            }
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return _tasks.ToArray();
        }

        protected override void QueueTask(Task task)
        {
            _tasks.Add(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return false;
        }
    }
}