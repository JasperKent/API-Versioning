using Microsoft.EntityFrameworkCore;
using Swagger.DataAccess.Entities;

namespace Swagger.DataAccess.Context
{
    public class ReviewContext : DbContext
    {
        public ReviewContext(DbContextOptions<ReviewContext> options)
            :base(options)
        {            
        }

        public DbSet<BookReview> BookReviews { get; set; }
    }
}
