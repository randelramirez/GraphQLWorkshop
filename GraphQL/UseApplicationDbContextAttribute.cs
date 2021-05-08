using System.Reflection;
using GraphQL.Data;
using GraphQL.Extensions;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace GraphQL
{
    public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            descriptor.UseDbContext<DataContext>();
        }
    }
}