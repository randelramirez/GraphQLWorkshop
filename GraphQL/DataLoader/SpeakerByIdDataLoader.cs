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
    public class SpeakerByIdDataLoader : BatchDataLoader<int, Speaker>
    {
        private readonly IDbContextFactory<DataContext> dbContextFactory;

        public SpeakerByIdDataLoader(
            IBatchScheduler batchScheduler, 
            IDbContextFactory<DataContext> dbContextFactory)
            : base(batchScheduler)
        {
            this.dbContextFactory = dbContextFactory ?? 
                                    throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Speaker>> LoadBatchAsync(
            IReadOnlyList<int> keys, 
            CancellationToken cancellationToken)
        {
            await using DataContext dbContext = 
                dbContextFactory.CreateDbContext();

            return await dbContext.Speakers
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}