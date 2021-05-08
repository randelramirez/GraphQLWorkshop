using System.Threading;
using System.Threading.Tasks;
using GraphQL.Common;
using GraphQL.DataLoader;
using GraphQL.Models;

namespace GraphQL.Attendees
{
    public class CheckInAttendeePayload : AttendeePayloadBase
    {
        private int? sessionId;

        public CheckInAttendeePayload(Attendee attendee, int sessionId)
            : base(attendee)
        {
            this.sessionId = sessionId;
        }

        public CheckInAttendeePayload(UserError error)
            : base(new[] { error })
        {
        }

        public async Task<Session?> GetSessionAsync(
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken)
        {
            if (sessionId.HasValue)
            {
                return await sessionById.LoadAsync(sessionId.Value, cancellationToken);
            }

            return null;
        }
    }
}