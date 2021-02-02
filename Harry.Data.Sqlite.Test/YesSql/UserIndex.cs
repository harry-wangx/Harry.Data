using Harry.Data.Samples.Models;
using System;
using System.Collections.Generic;
using System.Text;
using YesSql.Indexes;

namespace Harry.Data.Sqlite.Test
{
    public class UserIndex : MapIndex
    {
        public string Name { get; set; }
    }

    public class UserIndexProvider : IndexProvider<UserModel>
    {
        public override void Describe(DescribeContext<UserModel> context)
        {
            context.For<UserIndex>()
                .Map(user =>
                {
                    return new UserIndex
                    {
                        Name = user.Name
                    };
                });
        }
    }
}
