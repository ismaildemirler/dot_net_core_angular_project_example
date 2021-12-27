using DershaneBul.Entities.ComplexType;
using DershaneBul.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DershaneBul.DataAccess.Concrete.EntityFramework
{
    public class DershaneBulDbContext : DbContext
    {
        public DershaneBulDbContext(DbContextOptions<DershaneBulDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FirmComplexTypeModel>()
                .HasKey(c => new { c.FirmId });
            base.OnModelCreating(modelBuilder);
        }

        #region Tables
        public DbSet<Address> Address { get; set; }
        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommentLike> CommentLike { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<ContactType> ContactType { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<CourseType> CourseType { get; set; }
        public DbSet<CourseAndCourseType> CourseAndCourseType { get; set; }
        public DbSet<FeeRange> FeeRange { get; set; }
        public DbSet<FilterType> FilterType { get; set; }
        public DbSet<Firm> Firm { get; set; }
        public DbSet<FirmProgram> FirmProgram { get; set; }
        public DbSet<FirmProperty> FirmProperty { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<GroupTime> GroupTime { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<MediaType> MediaType { get; set; }
        public DbSet<MemberCampaign> MemberCampaign { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Program> Program { get; set; }
        public DbSet<ProgramRegisterCalender> ProgramRegisterCalender { get; set; }
        public DbSet<ProgramRegisterCalenderGroupTime> ProgramRegisterCalenderGroupTime { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Time> Time { get; set; }
        public DbSet<Town> Town { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<RelatedType> RelatedType { get; set; }
        #endregion

        #region ComplexTypes
        public DbSet<FirmComplexTypeModel> FirmComplexTypeModel { get; set; }
        #endregion
    }
}
