using LaktiBg.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaktiBg.Infrastructure.Data.SeedDb
{
    public class UserFriendsConfiguration : IEntityTypeConfiguration<UserFriends>
    {
        public void Configure(EntityTypeBuilder<UserFriends> builder)
        {
            var data = new SeedData();

            builder.HasData(new UserFriends[] { data.UserFriends, data.FriendUserFriends });
        }
    }
}
