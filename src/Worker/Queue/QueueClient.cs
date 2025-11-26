using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Worker.Queue
{
    /// <summary>
    /// Example in-memory implementation of IQueueClient for local development/testing.
    /// Replace with SQS or other queue in production.
    /// </summary>
    public class QueueClient : IQueueClient
    {
        private readonly Queue<Guid> _jobQueue = new();

        public void EnqueueJob(Guid jobId)
        {
            _jobQueue.Enqueue(jobId);
        }

        public async IAsyncEnumerable<Guid> ReceiveJobIdsAsync([System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_jobQueue.Count > 0)
                {
                    yield return _jobQueue.Dequeue();
                }
                else
                {
                    await Task.Delay(500, cancellationToken);
                }
            }
        }
    }
}
