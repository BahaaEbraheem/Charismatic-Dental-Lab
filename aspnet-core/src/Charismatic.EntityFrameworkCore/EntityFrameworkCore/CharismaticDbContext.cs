using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Charismatic.Authorization.Users;
using Charismatic.Authorization.Roles;
using Charismatic.MultiTenancy;
using Charismatic.Models;
using Charismatic.Models.Address;

namespace Charismatic.EntityFrameworkCore
{
    public class CharismaticDbContext : AbpZeroDbContext<Tenant, Role, User, CharismaticDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Case> Cases { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<MissionMember> MissionMembers { get; set; }
        public DbSet<AdvancedA> AdvancedAs { get; set; }
        public DbSet<AdvancedB> advancedBs { get; set; }
        public DbSet<PatientReferrais> PatientReferrais { get; set; }
        public DbSet<CaseType> CaseTypes { get; set; }
        public DbSet<CaseTypeDepartmentWorkFlow> CaseTypeDepartmentWorkFlows { get; set; }
        public DbSet<CaseDepartmentExcluded> CaseDepartmentExcludeds { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Center> Centers { get; set; }
        public DbSet<Matrial> Matrials { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<DoctorCenter> DoctorCenters { get; set; }
        public DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Teeth> Teeths { get; set; }
        public DbSet<TeethColor> TeethColors { get; set; }
        public DbSet<TeethLength> teethLengths { get; set; }

        public DbSet<TeethCase> TeethCases { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<CaseProduct> CaseProducts { get; set; }
        public DbSet<AttachmentType> AttachmentTypes { get; set; }
        public DbSet<AttachmentExtension> AttachmentExtensions { get; set; }
        public DbSet<Extension> Extensions { get; set; }

        public DbSet<Bill> Bill { get; set; }


        //public DbSet<Country> CountriesList { get; set; }

        public DbSet<State> States { get; set; }

        public CharismaticDbContext(DbContextOptions<CharismaticDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Case>()
           .HasOne(e => e.Doctor)
           .WithMany(d => d.Cases)
           .HasForeignKey(c => c.DoctorId)
           .IsRequired(true)
           .OnDelete(DeleteBehavior.ClientSetNull);



            modelBuilder.Entity<Case>()
           .HasOne(e => e.EvaluationCase)
           .WithMany(ec => ec.Cases)
           .HasForeignKey(c => c.EvaluationCaseId)
           .IsRequired(false)
           .OnDelete(DeleteBehavior.ClientSetNull);





            modelBuilder.Entity<Case>()
           .HasOne(e => e.PatientReferrais)
           .WithMany(pr => pr.Cases)
           .HasForeignKey(c => c.PatientReferraisId)
           .IsRequired(true)
           .OnDelete(DeleteBehavior.ClientSetNull);



            modelBuilder.Entity<Case>()
            .HasOne(e => e.CaseType)
            .WithMany(ct => ct.Cases)
            .HasForeignKey(c => c.TypeId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);




            modelBuilder.Entity<TeethCase>()
            .HasOne(e => e.Case)
            .WithMany(c => c.TeethCases)
            .HasForeignKey(c => c.CaseId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<TeethCase>()
            .HasOne(e => e.Teeth)
            .WithMany(t => t.TeethCases)
            .HasForeignKey(c => c.TeethId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.ClientSetNull);



            modelBuilder.Entity<TeethCase>()
            .HasOne(e => e.TeethColor)
            .WithMany(tc => tc.TeethCases)
            .HasForeignKey(c => c.ColorId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.ClientSetNull);




            modelBuilder.Entity<TeethCase>()
            .HasOne(e => e.Matrial)
            .WithMany(m => m.TeethCases)
            .HasForeignKey(c => c.MatrialId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<CaseProduct>()
            .HasOne(e => e.Case)
            .WithMany(c => c.CaseProducts)
            .HasForeignKey(c => c.CaseId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.ClientSetNull);



            modelBuilder.Entity<CaseProduct>()
            .HasOne(e => e.Product)
            .WithMany(p => p.CaseProducts)
            .HasForeignKey(c => c.ProductId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.ClientSetNull);




            modelBuilder.Entity<DoctorCenter>()
            .HasOne(e => e.Doctor)
            .WithMany(d => d.DoctorCenters)
            .HasForeignKey(c => c.DoctorId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.ClientSetNull);





            modelBuilder.Entity<DoctorCenter>()
            .HasOne(e => e.Center)
            .WithMany(c => c.DoctorCenters)
            .HasForeignKey(c => c.CenterId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.ClientSetNull);



            modelBuilder.Entity<DoctorSpecialty>()
            .HasOne(e => e.Specialty)
            .WithMany()
            .HasForeignKey(c => c.SpecialtyId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.ClientSetNull);



            modelBuilder.Entity<DoctorSpecialty>()
            .HasOne(e => e.Doctor)
            .WithMany(d => d.DoctorSpecialties)
            .HasForeignKey(c => c.DoctorId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.ClientSetNull);





            modelBuilder.Entity<Employee>()
             .HasOne(e => e.Department)
             .WithMany(d => d.Employees)
             .HasForeignKey(c => c.DepartmentId)
             .IsRequired(true)
             .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<Country>()
               .Property(et => et.Id)
               .ValueGeneratedNever();

            modelBuilder.Entity<State>()
               .Property(et => et.Id)
               .ValueGeneratedNever();


            //     modelBuilder.Entity<Transfer>()
            //         .HasOne(e => e.TransferFromUser)
            //        .WithMany()
            //        .HasForeignKey(c => c.TransferFromUserId)
            //        .IsRequired(true)
            //        .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<Transfer>()
            //         .HasOne(e => e.TransferToUser)
            //         .WithMany()
            //         .HasForeignKey(c => c.TransferToUserId)
            //         .IsRequired(true)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<Coaching>()
            //         .HasOne(e => e.ToMonitorUser)
            //        .WithMany()
            //        .HasForeignKey(c => c.ToMonitorUserId)
            //        .IsRequired(true)
            //        .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<Coaching>()
            //         .HasOne(e => e.FromMonitorUser)
            //         .WithMany()
            //         .HasForeignKey(c => c.FromMonitorUserId)
            //         .IsRequired(true)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<Coaching>()
            //         .HasOne(e => e.Session)
            //         .WithMany()
            //         .HasForeignKey(c => c.SessionId)
            //         .IsRequired(true)
            //         .OnDelete(DeleteBehavior.ClientSetNull);
            //     modelBuilder.Entity<Coaching>()
            //         .HasOne(e => e.OrginalSession)
            //        .WithMany()
            //        .HasForeignKey(c => c.OrginalSessionId)
            //        .IsRequired(true)
            //        .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<Delegation>()
            //         .HasOne(e => e.FromUser)
            //        .WithMany(e => e.FromDelegations)
            //        .HasForeignKey(c => c.FromUserId)
            //        .IsRequired(true)
            //        .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<Delegation>()
            //         .HasOne(e => e.ToUser)
            //         .WithMany(e => e.ToDelegations)
            //         .HasForeignKey(c => c.ToUserId)
            //         .IsRequired(true)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<Agent>()
            //         .HasOne(e => e.Unit)
            //         .WithMany()
            //         .HasForeignKey(e => e.UnitId)
            //         .IsRequired(true)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<DiscussionBoard>()
            //         .HasOne(e => e.LastInvitedUser)
            //        .WithMany()
            //        .HasForeignKey(c => c.LastInvitedUserId)
            //        .IsRequired(false)
            //        .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<DiscussionBoard>()
            //         .HasOne(e => e.ClosedByUser)
            //         .WithMany()
            //         .HasForeignKey(c => c.ClosedByUserId)
            //         .IsRequired(false)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<Contact>()
            //        .HasOne(e => e.CategoryIndex)
            //        .WithMany()
            //        .HasForeignKey(c => c.CategoryIndexId)
            //        .IsRequired(false)
            //        .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<Contact>()
            //         .HasOne(e => e.SkillIndex)
            //         .WithMany()
            //         .HasForeignKey(c => c.SkillIndexId)
            //         .IsRequired(false)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<ContactEvaluation>()
            //          .HasOne(e => e.Session)
            //          .WithMany()
            //          .HasForeignKey(c => c.SessionId)
            //          .IsRequired(true)
            //          .OnDelete(DeleteBehavior.NoAction);
            //     modelBuilder.Entity<Session>()
            //         .HasOne(e => e.MonitorUser)
            //         .WithMany()
            //         .HasForeignKey(c => c.MonitorUserId)
            //         .IsRequired(true)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<Session>()
            //         .HasOne(e => e.AgentHOSUser)
            //         .WithMany()
            //         .HasForeignKey(c => c.AgentHOSUserId)
            //         .IsRequired(false)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<EvaluationCriterion>()
            //         .HasOne(e => e.Parent)
            //         .WithMany(e => e.Children)
            //         .HasForeignKey(c => c.ParentId)
            //         .IsRequired(false)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<Comment>()
            //         .HasOne(e => e.ReplyToComment)
            //         .WithMany(e => e.Replies)
            //         .HasForeignKey(c => c.ReplyToCommentId)
            //         .IsRequired(false)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     //modelBuilder.Entity<SelectionItem>()
            //     //   // .HasOne(e => e.Parent)
            //     //    .WithMany(e => e.Children)
            //     //    .HasForeignKey(c => c.ParentId)
            //     //    .IsRequired(false)
            //     //    .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<SessionContact>()
            //         .HasOne(e => e.AddingReason)
            //         .WithMany()
            //         .HasForeignKey(c => c.AddingReasonId)
            //         .IsRequired(false)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<SessionContact>()
            //         .HasOne(e => e.ChangedReason)
            //         .WithMany()
            //         .HasForeignKey(c => c.ChangedReasonId)
            //         .IsRequired(false)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<EvaluationCycle>()
            //         .HasOne(e => e.SelectionCriterion)
            //         .WithMany()
            //         .HasForeignKey(c => c.SelectionCriterionId)
            //         .IsRequired(true)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<DiscussionBoardUser>()
            //         .HasOne(e => e.User)
            //         .WithMany()
            //         .HasForeignKey(c => c.UserId)
            //         .IsRequired(true)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<SessionContact>()
            //         .HasOne(e => e.Session)
            //         .WithMany()
            //         .HasForeignKey(c => c.SessionId)
            //         .IsRequired(true)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<Session>()
            //         .HasOne(e => e.MonitorUser)
            //         .WithMany()
            //         .HasForeignKey(c => c.MonitorUserId)
            //         .IsRequired(true)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<Session>()
            //         .HasOne(e => e.EvaluationCycle)
            //         .WithMany()
            //         .HasForeignKey(c => c.EvaluationCycleId)
            //         .IsRequired(false)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<Session>()
            //         .HasOne(e => e.SelectionCriterion)
            //         .WithMany()
            //         .HasForeignKey(c => c.SelectionCriterionId)
            //         .IsRequired(true)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<MonitorDistribution>()
            //         .HasOne(e => e.EvaluationCycle)
            //         .WithMany()
            //         .HasForeignKey(c => c.EvaluationCycleId)
            //         .IsRequired(true)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //     modelBuilder.Entity<MonitorDistribution>()
            //         .HasOne(e => e.MonitorUser)
            //         .WithMany()
            //         .HasForeignKey(c => c.MonitorUserId)
            //         .IsRequired(true)
            //         .OnDelete(DeleteBehavior.ClientSetNull);


            //     modelBuilder.Entity<FaceBookPost>()
            //      .HasOne(e => e.FaceBookPage)
            //      .WithMany()
            //      .HasForeignKey(c => c.PageId)
            //      .IsRequired(true)
            //      .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
