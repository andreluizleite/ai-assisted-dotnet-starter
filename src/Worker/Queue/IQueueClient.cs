using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Worker.Queue
{
    /// <summary>
    /// Abstraction for a queue client that receives PostGenerationJob IDs.
    /// </summary>
    public interface IQueueClient
    {
        /// <summary>
        /// Asynchronously receives JobIds from the queue.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Async stream of JobIds.</returns>
        IAsyncEnumerable<Guid> ReceiveJobIdsAsync(CancellationToken cancellationToken);
    }
}




