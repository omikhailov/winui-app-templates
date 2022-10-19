using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Services.Lifecycle.Messages
{
    public abstract class OptionallyAsynchronousMessage
    {
        private List<Task> _taskList;

        public Task CompletionTask 
        { 
            get
            {
                if (_taskList == null)
                {
                    return Task.CompletedTask;
                }
                else
                {
                    return Task.WhenAll(_taskList);
                }
            }
        }

        public void Await(Task me)
        {
            if (_taskList == null) _taskList = new List<Task>();

            _taskList.Add(me);
        }
    }
}
