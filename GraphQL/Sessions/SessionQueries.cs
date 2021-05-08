using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.Common;
using GraphQL.Data;
using GraphQL.DataLoader;
using GraphQL.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace GraphQL.Sessions
{
    [ExtendObjectType(Constants.QUERY)]
    public class SessionQueries
    {
        // [UseApplicationDbContext]
        // public async Task<IEnumerable<Session>> GetSessionsAsync(
        //     [ScopedService] DataContext context,
        //     CancellationToken cancellationToken) =>
        //     await context.Sessions.ToListAsync(cancellationToken);
        
        [UseApplicationDbContext]
        [UsePaging(typeof(NonNullType<SessionType>))]
        /*
            By default the filter middleware would infer a filter type that exposes all the fields of the entity. 
            In our case it would be better to remove filtering for ids and internal fields and focus on fields that the user really can use.
         */
        [UseFiltering(typeof(SessionFilterInputType))]
        [UseSorting]
        public IQueryable<Session> GetSessions(
            [ScopedService] DataContext context) =>
            context.Sessions;

        public Task<Session> GetSessionByIdAsync(
            [ID(nameof(Session))] int id,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            sessionById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Session>> GetSessionsByIdAsync(
            [ID(nameof(Session))] int[] ids,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            await sessionById.LoadAsync(ids, cancellationToken);
    }
}