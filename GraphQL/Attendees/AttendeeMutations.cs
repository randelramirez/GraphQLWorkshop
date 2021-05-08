using System.Threading;
using System.Threading.Tasks;
using GraphQL.Common;
using GraphQL.Data;
using GraphQL.Models;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Attendees
{
    [ExtendObjectType(Constants.MUTATION)]
    public class AttendeeMutations
    {
        [UseApplicationDbContext]
        public async Task<RegisterAttendeePayload> RegisterAttendeeAsync(
            RegisterAttendeeInput input,
            [ScopedService] DataContext context,
            CancellationToken cancellationToken)
        {
            var attendee = new Attendee
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                UserName = input.UserName,
                EmailAddress = input.EmailAddress
            };

            context.Attendees.Add(attendee);

            await context.SaveChangesAsync(cancellationToken);

            return new RegisterAttendeePayload(attendee);
        }
        
        [UseApplicationDbContext]
        public async Task<CheckInAttendeePayload> CheckInAttendeeAsync(
            CheckInAttendeeInput input,
            [ScopedService] DataContext context,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            Attendee attendee = await context.Attendees.FirstOrDefaultAsync(
                t => t.Id == input.AttendeeId, cancellationToken);

            if (attendee is null)
            {
                return new CheckInAttendeePayload(
                    new UserError("Attendee not found.", "ATTENDEE_NOT_FOUND"));
            }

            attendee.SessionsAttendees.Add(
                new SessionAttendee
                {
                    SessionId = input.SessionId
                });

            await context.SaveChangesAsync(cancellationToken);

            // creating a string topic combined with parts of the input input.SessionId and a string describing the event OnAttendeeCheckedIn_
            await eventSender.SendAsync(
                "OnAttendeeCheckedIn_" + input.SessionId,
                input.AttendeeId,
                cancellationToken);

            return new CheckInAttendeePayload(attendee, input.SessionId);
        }
    }
}