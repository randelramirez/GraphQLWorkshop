using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.Data;
using GraphQL.Models;
using GreenDonut;
using HotChocolate.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.DataLoader
{
    public class AttendeeByIdDataLoader : BatchDataLoader<int, Attendee>
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;

        public AttendeeByIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<DataContext> dbContextFactory)
            : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory ?? 
                                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Attendee>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using DataContext dbContext = 
                _dbContextFactory.CreateDbContext();

            return await dbContext.Attendees
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}