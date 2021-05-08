using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.Common;
using GraphQL.Data;
using GraphQL.DataLoader;
using GraphQL.Models;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace GraphQL.Attendees
{
    [ExtendObjectType(Constants.QUERY)]
    public class AttendeeQueries
    {
        [UseApplicationDbContext]
        [UsePaging]
        public IQueryable<Attendee> GetAttendees(
            [ScopedService] DataContext context) =>
            context.Attendees;

        public Task<Attendee> GetAttendeeByIdAsync(
            [ID(nameof(Attendee))]int id,
            AttendeeByIdDataLoader attendeeById,
            CancellationToken cancellationToken) =>
            attendeeById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Attendee>> GetAttendeesByIdAsync(
            [ID(nameof(Attendee))]int[] ids,
            AttendeeByIdDataLoader attendeeById,
            CancellationToken cancellationToken) =>
            await attendeeById.LoadAsync(ids, cancellationToken);
    }
}