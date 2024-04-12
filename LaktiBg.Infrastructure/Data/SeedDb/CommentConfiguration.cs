using LaktiBg.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Infrastructure.Data.SeedDb
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            var data = new SeedData();

            builder.HasData(new Comment[] { data.HappyEventFirstComment, data.HappyEventSecondComment, data.HappyEventThirdComment,
                data.VilaPetraEventFirstComment, data.VilaPetraEventSecondComment,
                data.CinemaCityEventFirstComment});
        }

    }
}
