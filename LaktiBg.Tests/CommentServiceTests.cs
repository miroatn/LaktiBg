using LaktiBg.Infrastructure.Data.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LaktiBg.Tests
{
    public class CommentServiceTests
    {
        [Test]
        public async Task AddCommentAsync_SuccessfullyAddsComment()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Comment>>();
            var mockUserService = new Mock<IUserService>();
            var mockEventService = new Mock<IEventService>();

            var commentService = new CommentService(mockRepository.Object, mockUserService.Object, mockEventService.Object);

            var model = new CommentFormModel
            {
                Text = "Test comment",
                AuthorId = "author123"
            };
            var eventId = 1;

            // Act
            await commentService.AddCommentAsync(model, eventId);

            // Assert
            mockRepository.Verify(r => r.AddAsync(It.IsAny<Comment>()), Times.Once);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
