using System.Threading;
using System.Threading.Tasks;
using GraphQL.Common;
using GraphQL.DataLoader;
using GraphQL.Models;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQL.Sessions
{
    [ExtendObjectType(Constants.SUBSCRIPTION)]
    public class SessionSubscriptions
    {
        [Subscribe]
        [Topic]
        public Task<Session> OnSessionScheduledAsync(
            [EventMessage] int sessionId,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            sessionById.LoadAsync(sessionId, cancellationToken);
    }
}