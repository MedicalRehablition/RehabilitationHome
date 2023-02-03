using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace prjRehabilitation.Models
{
    public partial class dbClassContext : DbContext
    {
        public dbClassContext()
        {
        }

        public dbClassContext(DbContextOptions<dbClassContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Consultation> Consultations { get; set; } = null!;
        public virtual DbSet<CounsultTypeRecord> CounsultTypeRecords { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<DiseaseDiagnosis> DiseaseDiagnoses { get; set; } = null!;
        public virtual DbSet<DiseaseList> DiseaseLists { get; set; } = null!;
        public virtual DbSet<EmergenceCaller> EmergenceCallers { get; set; } = null!;
        public virtual DbSet<MedDosageUnit> MedDosageUnits { get; set; } = null!;
        public virtual DbSet<MedList> MedLists { get; set; } = null!;
        public virtual DbSet<MedTakeTime> MedTakeTimes { get; set; } = null!;
        public virtual DbSet<MedUsage> MedUsages { get; set; } = null!;
        public virtual DbSet<PatientGetMedDate> PatientGetMedDates { get; set; } = null!;
        public virtual DbSet<PatientInfo> PatientInfos { get; set; } = null!;
        public virtual DbSet<PatientMedInfo> PatientMedInfos { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<TCalendar> TCalendars { get; set; } = null!;
        public virtual DbSet<TClassThemeList> TClassThemeLists { get; set; } = null!;
        public virtual DbSet<TForumArtical> TForumArticals { get; set; } = null!;
        public virtual DbSet<TGroupActivity> TGroupActivities { get; set; } = null!;
        public virtual DbSet<TGroupActivityClassTheme> TGroupActivityClassThemes { get; set; } = null!;
        public virtual DbSet<TGroupActivityPicAndFile> TGroupActivityPicAndFiles { get; set; } = null!;
        public virtual DbSet<TOfficialPost> TOfficialPosts { get; set; } = null!;
        public virtual DbSet<TPersonalPerformance> TPersonalPerformances { get; set; } = null!;
        public virtual DbSet<TPostComment> TPostComments { get; set; } = null!;
        public virtual DbSet<TScheduleDetail> TScheduleDetails { get; set; } = null!;
        public virtual DbSet<TypeName> TypeNames { get; set; } = null!;
        public virtual DbSet<功能評估> 功能評估s { get; set; } = null!;
        public virtual DbSet<功能評估個表> 功能評估個表s { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=medicate.database.windows.net;Initial Catalog=dbClass;User ID=medical;Password=Uxul6yj3");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Fid);

                entity.ToTable("Admin");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.FBirth)
                    .HasMaxLength(50)
                    .HasColumnName("fBirth");

                entity.Property(e => e.FEmail)
                    .HasMaxLength(50)
                    .HasColumnName("fEmail");

                entity.Property(e => e.FName)
                    .HasMaxLength(50)
                    .HasColumnName("fName");

                entity.Property(e => e.FPassword)
                    .HasMaxLength(50)
                    .HasColumnName("fPassword");

                entity.Property(e => e.FRank)
                    .HasMaxLength(50)
                    .HasColumnName("fRank");

                entity.Property(e => e.FSex)
                    .HasMaxLength(50)
                    .HasColumnName("fSex");

                entity.Property(e => e.Fphoto)
                    .HasMaxLength(50)
                    .HasColumnName("fphoto");
            });

            modelBuilder.Entity<Consultation>(entity =>
            {
                entity.HasKey(e => e.FConsultId);

                entity.ToTable("Consultation");

                entity.Property(e => e.FConsultId).HasColumnName("fConsultId");

                entity.Property(e => e.Assessment).HasMaxLength(250);

                entity.Property(e => e.Date).HasMaxLength(50);

                entity.Property(e => e.Result).HasMaxLength(500);

                entity.Property(e => e.Summary).HasMaxLength(200);

                entity.HasOne(d => d.Patinet)
                    .WithMany(p => p.Consultations)
                    .HasForeignKey(d => d.PatinetId)
                    .HasConstraintName("FK_Consultation_PatientInfo");
            });

            modelBuilder.Entity<CounsultTypeRecord>(entity =>
            {
                entity.HasKey(e => e.Fid);

                entity.ToTable("CounsultTypeRecord");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.FConsultId).HasColumnName("fConsultId");

                entity.HasOne(d => d.FConsult)
                    .WithMany(p => p.CounsultTypeRecords)
                    .HasForeignKey(d => d.FConsultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CounsultTypeRecord_Consultation");

                entity.HasOne(d => d.TypeName)
                    .WithMany(p => p.CounsultTypeRecords)
                    .HasForeignKey(d => d.TypeNameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CounsultTypeRecord_TypeNames");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Fid);

                entity.ToTable("Customer");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.FAddress)
                    .HasMaxLength(50)
                    .HasColumnName("fAddress");

                entity.Property(e => e.FEmail)
                    .HasMaxLength(50)
                    .HasColumnName("fEmail");

                entity.Property(e => e.FName)
                    .HasMaxLength(50)
                    .HasColumnName("fName");

                entity.Property(e => e.FPassword)
                    .HasMaxLength(50)
                    .HasColumnName("fPassword");

                entity.Property(e => e.FPhone)
                    .HasMaxLength(50)
                    .HasColumnName("fPhone");

                entity.Property(e => e.FPicture)
                    .HasMaxLength(50)
                    .HasColumnName("fPicture");
            });

            modelBuilder.Entity<DiseaseDiagnosis>(entity =>
            {
                entity.HasKey(e => e.IdPatientDisease)
                    .HasName("PK_疾病診斷");

                entity.ToTable("DiseaseDiagnosis");

                entity.Property(e => e.IdPatientDisease).HasColumnName("ID_Patient_Disease");

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DiseaseChineseName).HasMaxLength(50);

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.IdDisease)
                    .HasMaxLength(50)
                    .HasColumnName("ID_Disease");

                entity.Property(e => e.Remark).HasMaxLength(50);

                entity.Property(e => e.Section).HasMaxLength(50);

                entity.HasOne(d => d.FidNavigation)
                    .WithMany(p => p.DiseaseDiagnoses)
                    .HasForeignKey(d => d.Fid)
                    .HasConstraintName("FK_疾病診斷_PatientInfo");
            });

            modelBuilder.Entity<DiseaseList>(entity =>
            {
                entity.HasKey(e => e.IdDisease)
                    .HasName("PK_'2021中文版_cm$'");

                entity.ToTable("DiseaseList");

                entity.Property(e => e.IdDisease)
                    .HasMaxLength(255)
                    .HasColumnName("ID_Disease");

                entity.Property(e => e.DiseaseChineseName).HasMaxLength(255);

                entity.Property(e => e.DiseaseEnglishName).HasMaxLength(255);

                entity.Property(e => e.F5).HasMaxLength(255);
            });

            modelBuilder.Entity<EmergenceCaller>(entity =>
            {
                entity.HasKey(e => e.Fid);

                entity.ToTable("EmergenceCaller");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.FEmergencyName)
                    .HasMaxLength(50)
                    .HasColumnName("fEmergencyName");

                entity.Property(e => e.FPatientId).HasColumnName("fPatientId");

                entity.Property(e => e.FPhone)
                    .HasMaxLength(50)
                    .HasColumnName("fPhone");

                entity.Property(e => e.Frelation)
                    .HasMaxLength(50)
                    .HasColumnName("frelation");

                entity.HasOne(d => d.FPatient)
                    .WithMany(p => p.EmergenceCallers)
                    .HasForeignKey(d => d.FPatientId)
                    .HasConstraintName("FK_EmergenceCaller_PatientInfo");
            });

            modelBuilder.Entity<MedDosageUnit>(entity =>
            {
                entity.HasKey(e => e.IdUnit)
                    .HasName("PK_藥物計量單位");

                entity.ToTable("MedDosageUnit");

                entity.Property(e => e.IdUnit).HasColumnName("ID_Unit");

                entity.Property(e => e.MedicineUnit)
                    .HasMaxLength(50)
                    .HasColumnName("Medicine_Unit");
            });

            modelBuilder.Entity<MedList>(entity =>
            {
                entity.HasKey(e => e.IdMedicine)
                    .HasName("PK_藥物資訊清單");

                entity.ToTable("MedList");

                entity.Property(e => e.IdMedicine).HasColumnName("ID_Medicine");

                entity.Property(e => e.MedicineChineseName).HasMaxLength(50);

                entity.Property(e => e.MedicineInformation).HasMaxLength(50);
            });

            modelBuilder.Entity<MedTakeTime>(entity =>
            {
                entity.HasKey(e => e.IdMedicineTakeTime)
                    .HasName("PK_藥物服用時間");

                entity.ToTable("MedTakeTime");

                entity.Property(e => e.IdMedicineTakeTime).HasColumnName("ID_MedicineTakeTime");

                entity.Property(e => e.MedicineTakeTime).HasMaxLength(50);
            });

            modelBuilder.Entity<MedUsage>(entity =>
            {
                entity.HasKey(e => e.IdMedicineUsage)
                    .HasName("PK_藥物使用方法");

                entity.ToTable("MedUsage");

                entity.Property(e => e.IdMedicineUsage).HasColumnName("ID_MedicineUsage");

                entity.Property(e => e.MedicineUsage).HasMaxLength(50);
            });

            modelBuilder.Entity<PatientGetMedDate>(entity =>
            {
                entity.HasKey(e => e.IdGetMedicineDate)
                    .HasName("PK_病人開藥日期");

                entity.ToTable("PatientGetMedDate");

                entity.Property(e => e.IdGetMedicineDate).HasColumnName("ID_GetMedicineDate");

                entity.Property(e => e.DateGetMedicine)
                    .HasMaxLength(50)
                    .HasColumnName("Date_GetMedicine");

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.HasOne(d => d.FidNavigation)
                    .WithMany(p => p.PatientGetMedDates)
                    .HasForeignKey(d => d.Fid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_病人開藥日期_PatientInfo");
            });

            modelBuilder.Entity<PatientInfo>(entity =>
            {
                entity.HasKey(e => e.Fid);

                entity.ToTable("PatientInfo");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.FAddressPermanent)
                    .HasMaxLength(50)
                    .HasColumnName("fAddressPermanent");

                entity.Property(e => e.FAddressResidential)
                    .HasMaxLength(50)
                    .HasColumnName("fAddressResidential");

                entity.Property(e => e.FBednum)
                    .HasMaxLength(50)
                    .HasColumnName("fBednum");

                entity.Property(e => e.FBirthday)
                    .HasMaxLength(50)
                    .HasColumnName("fBirthday");

                entity.Property(e => e.FCheckin)
                    .HasMaxLength(50)
                    .HasColumnName("fCheckin");

                entity.Property(e => e.FCountry)
                    .HasMaxLength(10)
                    .HasColumnName("fCountry");

                entity.Property(e => e.FCustomerid)
                    .HasMaxLength(50)
                    .HasColumnName("fCustomerid");

                entity.Property(e => e.FEdu)
                    .HasMaxLength(50)
                    .HasColumnName("fEdu");

                entity.Property(e => e.FExpireDate)
                    .HasMaxLength(50)
                    .HasColumnName("fExpireDate");

                entity.Property(e => e.FGrant)
                    .HasMaxLength(50)
                    .HasColumnName("fGrant");

                entity.Property(e => e.FHomeNum)
                    .HasMaxLength(50)
                    .HasColumnName("fHomeNum");

                entity.Property(e => e.FHos)
                    .HasMaxLength(50)
                    .HasColumnName("fHos");

                entity.Property(e => e.FIdnum)
                    .HasMaxLength(50)
                    .HasColumnName("fIdnum");

                entity.Property(e => e.FIdy)
                    .HasMaxLength(50)
                    .HasColumnName("fIDY");

                entity.Property(e => e.FMarried)
                    .HasMaxLength(50)
                    .HasColumnName("fMarried");

                entity.Property(e => e.FName)
                    .HasMaxLength(50)
                    .HasColumnName("fName");

                entity.Property(e => e.FPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fPhone");

                entity.Property(e => e.FPhotoFile).HasColumnName("fPhotoFile");

                entity.Property(e => e.FPicture)
                    .HasMaxLength(80)
                    .HasColumnName("fPicture");

                entity.Property(e => e.FSex)
                    .HasMaxLength(50)
                    .HasColumnName("fSex");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<PatientMedInfo>(entity =>
            {
                entity.HasKey(e => e.IdPatientMedicineInfo)
                    .HasName("PK_病人用藥資訊");

                entity.ToTable("PatientMedInfo");

                entity.Property(e => e.IdPatientMedicineInfo).HasColumnName("ID_Patient_Medicine_Info");

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdGetMedicineDate).HasColumnName("ID_GetMedicineDate");

                entity.Property(e => e.MedicineAddinfo)
                    .HasMaxLength(50)
                    .HasColumnName("MedicineADDInfo");

                entity.Property(e => e.MedicineChineseName).HasMaxLength(50);

                entity.Property(e => e.MedicineDosage).HasMaxLength(50);

                entity.Property(e => e.MedicineTakeTime).HasMaxLength(50);

                entity.Property(e => e.MedicineUsage).HasMaxLength(50);

                entity.HasOne(d => d.IdGetMedicineDateNavigation)
                    .WithMany(p => p.PatientMedInfos)
                    .HasForeignKey(d => d.IdGetMedicineDate)
                    .HasConstraintName("FK_病人用藥資訊_病人開藥日期");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Fid);

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.FName)
                    .HasMaxLength(50)
                    .HasColumnName("fName");

                entity.Property(e => e.FPhoto)
                    .HasMaxLength(50)
                    .HasColumnName("fPhoto");

                entity.Property(e => e.FPrice)
                    .HasColumnType("money")
                    .HasColumnName("fPrice");

                entity.Property(e => e.FQty).HasColumnName("fQty");
            });

            modelBuilder.Entity<TCalendar>(entity =>
            {
                entity.HasKey(e => e.FId);

                entity.ToTable("tCalendar");

                entity.Property(e => e.FId).HasColumnName("fID");

                entity.Property(e => e.FClassName)
                    .HasMaxLength(100)
                    .HasColumnName("fClassName");

                entity.Property(e => e.FContent)
                    .HasMaxLength(100)
                    .HasColumnName("fContent");

                entity.Property(e => e.FDate)
                    .HasMaxLength(50)
                    .HasColumnName("fDate");

                entity.Property(e => e.FDateColor)
                    .HasMaxLength(100)
                    .HasColumnName("fDateColor");

                entity.Property(e => e.FDeleteBool).HasColumnName("fDeleteBool");

                entity.Property(e => e.FEventName)
                    .HasMaxLength(100)
                    .HasColumnName("fEventName");

                entity.Property(e => e.FRecorder)
                    .HasMaxLength(50)
                    .HasColumnName("fRecorder");

                entity.Property(e => e.FRecorderDate)
                    .HasMaxLength(50)
                    .HasColumnName("fRecorderDate");

                entity.Property(e => e.FTitle)
                    .HasMaxLength(100)
                    .HasColumnName("fTitle");
            });

            modelBuilder.Entity<TClassThemeList>(entity =>
            {
                entity.HasKey(e => e.FClassThemeId);

                entity.ToTable("tClassThemeList");

                entity.Property(e => e.FClassThemeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("fClassThemeID");

                entity.Property(e => e.FClassThemeName)
                    .HasMaxLength(20)
                    .HasColumnName("fClassThemeName");

                entity.Property(e => e.FDeleteBool).HasColumnName("fDeleteBool");
            });

            modelBuilder.Entity<TForumArtical>(entity =>
            {
                entity.HasKey(e => e.FArticalId);

                entity.ToTable("tForumArtical");

                entity.Property(e => e.FArticalId).HasColumnName("fArtical_Id");

                entity.Property(e => e.FBad).HasColumnName("fBad");

                entity.Property(e => e.FBelongto).HasColumnName("fBelongto");

                entity.Property(e => e.FContent)
                    .HasMaxLength(300)
                    .HasColumnName("fContent");

                entity.Property(e => e.FGood).HasColumnName("fGood");

                entity.Property(e => e.FStatus)
                    .IsRequired()
                    .HasColumnName("fStatus")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FTime)
                    .HasMaxLength(30)
                    .HasColumnName("fTime");

                entity.Property(e => e.FTitle)
                    .HasMaxLength(50)
                    .HasColumnName("fTitle");

                entity.Property(e => e.FUserId).HasColumnName("fUserId");

                entity.Property(e => e.FisAnonymous).HasColumnName("fisAnonymous");
            });

            modelBuilder.Entity<TGroupActivity>(entity =>
            {
                entity.HasKey(e => e.FGroupActivityId);

                entity.ToTable("tGroupActivity");

                entity.Property(e => e.FGroupActivityId).HasColumnName("fGroupActivityID");

                entity.Property(e => e.FAchievement)
                    .HasMaxLength(100)
                    .HasColumnName("fAchievement");

                entity.Property(e => e.FClassName)
                    .HasMaxLength(50)
                    .HasColumnName("fClassName");

                entity.Property(e => e.FDate)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fDate");

                entity.Property(e => e.FDeleteBool).HasColumnName("fDeleteBool");

                entity.Property(e => e.FEndTime)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fEndTime");

                entity.Property(e => e.FEventType)
                    .HasMaxLength(20)
                    .HasColumnName("fEventType");

                entity.Property(e => e.FFillFormDate)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fFillFormDate");

                entity.Property(e => e.FFillFormStaff)
                    .HasMaxLength(50)
                    .HasColumnName("fFillFormStaff");

                entity.Property(e => e.FGoal)
                    .HasMaxLength(50)
                    .HasColumnName("fGoal");

                entity.Property(e => e.FGroupName)
                    .HasMaxLength(50)
                    .HasColumnName("fGroupName");

                entity.Property(e => e.FId).HasColumnName("fID");

                entity.Property(e => e.FLeader)
                    .HasMaxLength(50)
                    .HasColumnName("fLeader");

                entity.Property(e => e.FLocation)
                    .HasMaxLength(50)
                    .HasColumnName("fLocation");

                entity.Property(e => e.FPeopleCount).HasColumnName("fPeopleCount");

                entity.Property(e => e.FProcess)
                    .HasMaxLength(100)
                    .HasColumnName("fProcess");

                entity.Property(e => e.FRecorder)
                    .HasMaxLength(50)
                    .HasColumnName("fRecorder");

                entity.Property(e => e.FStartTime)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fStartTime");
            });

            modelBuilder.Entity<TGroupActivityClassTheme>(entity =>
            {
                entity.HasKey(e => e.FId)
                    .HasName("PK_tGroupActivityClassThemeBridging");

                entity.ToTable("tGroupActivityClassTheme");

                entity.Property(e => e.FId).HasColumnName("fID");

                entity.Property(e => e.FClassThemeId).HasColumnName("fClassThemeID");

                entity.Property(e => e.FDeleteBool).HasColumnName("fDeleteBool");

                entity.Property(e => e.FGroupActivityId).HasColumnName("fGroupActivityID");

                entity.HasOne(d => d.FClassTheme)
                    .WithMany(p => p.TGroupActivityClassThemes)
                    .HasForeignKey(d => d.FClassThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tGroupActivityClassTheme_TypeNames1");

                entity.HasOne(d => d.FGroupActivity)
                    .WithMany(p => p.TGroupActivityClassThemes)
                    .HasForeignKey(d => d.FGroupActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tGroupActivityClassThemeBridging_tGroupActivity");
            });

            modelBuilder.Entity<TGroupActivityPicAndFile>(entity =>
            {
                entity.HasKey(e => e.FId)
                    .HasName("PK_tGroupActivityPictures");

                entity.ToTable("tGroupActivityPicAndFiles");

                entity.Property(e => e.FId).HasColumnName("fID");

                entity.Property(e => e.FDeleteBool).HasColumnName("fDeleteBool");

                entity.Property(e => e.FFile1).HasColumnName("fFile1");

                entity.Property(e => e.FFile1Path)
                    .HasMaxLength(200)
                    .HasColumnName("fFile1Path");

                entity.Property(e => e.FGroupActivityId).HasColumnName("fGroupActivityID");

                entity.Property(e => e.FPicture1).HasColumnName("fPicture1");

                entity.Property(e => e.FPicture1Path)
                    .HasMaxLength(200)
                    .HasColumnName("fPicture1Path");

                entity.Property(e => e.FPicture2).HasColumnName("fPicture2");

                entity.Property(e => e.FPicture2Path)
                    .HasMaxLength(200)
                    .HasColumnName("fPicture2Path");

                entity.Property(e => e.FPicture3).HasColumnName("fPicture3");

                entity.Property(e => e.FPicture3Path)
                    .HasMaxLength(200)
                    .HasColumnName("fPicture3Path");

                entity.Property(e => e.FPicture4).HasColumnName("fPicture4");

                entity.Property(e => e.FPicture4Path)
                    .HasMaxLength(200)
                    .HasColumnName("fPicture4Path");

                entity.HasOne(d => d.FGroupActivity)
                    .WithMany(p => p.TGroupActivityPicAndFiles)
                    .HasForeignKey(d => d.FGroupActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tGroupActivityPictures_tGroupActivity");
            });

            modelBuilder.Entity<TOfficialPost>(entity =>
            {
                entity.HasKey(e => e.FPostId);

                entity.ToTable("tOfficialPost");

                entity.Property(e => e.FPostId).HasColumnName("fPostId");

                entity.Property(e => e.FContent).HasColumnName("fContent");

                entity.Property(e => e.FDate)
                    .HasMaxLength(50)
                    .HasColumnName("fDate");

                entity.Property(e => e.FMain)
                    .HasMaxLength(100)
                    .HasColumnName("fMain");

                entity.Property(e => e.FStatus)
                    .HasColumnName("fStatus")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FTag)
                    .HasMaxLength(50)
                    .HasColumnName("fTag");

                entity.Property(e => e.FTitle)
                    .HasMaxLength(50)
                    .HasColumnName("fTitle");
            });

            modelBuilder.Entity<TPersonalPerformance>(entity =>
            {
                entity.HasKey(e => e.FId);

                entity.ToTable("tPersonalPerformance");

                entity.Property(e => e.FId).HasColumnName("fID");

                entity.Property(e => e.FAttention)
                    .HasMaxLength(20)
                    .HasColumnName("fAttention");

                entity.Property(e => e.FCooperate)
                    .HasMaxLength(20)
                    .HasColumnName("fCooperate");

                entity.Property(e => e.FDeleteBool)
                    .HasMaxLength(10)
                    .HasColumnName("fDeleteBool")
                    .IsFixedLength();

                entity.Property(e => e.FDepiction)
                    .HasMaxLength(50)
                    .HasColumnName("fDepiction");

                entity.Property(e => e.FEmotions)
                    .HasMaxLength(20)
                    .HasColumnName("fEmotions");

                entity.Property(e => e.FGroupActivityId).HasColumnName("fGroupActivityID");

                entity.Property(e => e.FHumanInteraction)
                    .HasMaxLength(20)
                    .HasColumnName("fHumanInteraction");

                entity.Property(e => e.FParticipatePerformance)
                    .HasMaxLength(20)
                    .HasColumnName("fParticipatePerformance");

                entity.Property(e => e.FParticipatePersistence)
                    .HasMaxLength(20)
                    .HasColumnName("fParticipatePersistence");

                entity.Property(e => e.FResidentId).HasColumnName("fResidentID");

                entity.HasOne(d => d.FGroupActivity)
                    .WithMany(p => p.TPersonalPerformances)
                    .HasForeignKey(d => d.FGroupActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tPersonalPerformance_tGroupActivity");

                entity.HasOne(d => d.FResident)
                    .WithMany(p => p.TPersonalPerformances)
                    .HasForeignKey(d => d.FResidentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tPersonalPerformance_PatientInfo");
            });

            modelBuilder.Entity<TPostComment>(entity =>
            {
                entity.HasKey(e => e.Fid);

                entity.ToTable("tPostComment");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.FContent)
                    .HasMaxLength(300)
                    .HasColumnName("fContent");

                entity.Property(e => e.FDate)
                    .HasMaxLength(50)
                    .HasColumnName("fDate");

                entity.Property(e => e.FEmail)
                    .HasMaxLength(50)
                    .HasColumnName("fEmail");

                entity.Property(e => e.FName)
                    .HasMaxLength(50)
                    .HasColumnName("fName");

                entity.Property(e => e.FPostId).HasColumnName("fPostID");

                entity.Property(e => e.FStatus)
                    .IsRequired()
                    .HasColumnName("fStatus")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TScheduleDetail>(entity =>
            {
                entity.HasKey(e => e.FId);

                entity.ToTable("tScheduleDetails");

                entity.Property(e => e.FId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("fID");

                entity.Property(e => e.FDeleteBool).HasColumnName("fDeleteBool");

                entity.Property(e => e.FDepiction)
                    .HasMaxLength(50)
                    .HasColumnName("fDepiction");

                entity.Property(e => e.FEndTime)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fEndTime");

                entity.Property(e => e.FGroupActivityId).HasColumnName("fGroupActivityID");

                entity.Property(e => e.FStartTime)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fStartTime");

                entity.HasOne(d => d.FGroupActivity)
                    .WithMany(p => p.TScheduleDetails)
                    .HasForeignKey(d => d.FGroupActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tScheduleDetails_tGroupActivity");
            });

            modelBuilder.Entity<TypeName>(entity =>
            {
                entity.HasKey(e => e.Fid);

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.TypeName1)
                    .HasMaxLength(6)
                    .HasColumnName("TypeName")
                    .IsFixedLength();
            });

            modelBuilder.Entity<功能評估>(entity =>
            {
                entity.HasKey(e => e.F功能評估Id);

                entity.ToTable("功能評估");

                entity.Property(e => e.F功能評估Id).HasColumnName("f功能評估ID");

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.F日期)
                    .HasMaxLength(50)
                    .HasColumnName("f日期");

                entity.Property(e => e.F身高)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("f身高");

                entity.Property(e => e.F體重)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("f體重");

                entity.HasOne(d => d.FidNavigation)
                    .WithMany(p => p.功能評估s)
                    .HasForeignKey(d => d.Fid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_功能評估_PatientInfo");
            });

            modelBuilder.Entity<功能評估個表>(entity =>
            {
                entity.HasKey(e => e.Id評估表);

                entity.ToTable("功能評估個表");

                entity.Property(e => e.Id評估表).HasColumnName("ID_評估表");

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.F功能評估Id).HasColumnName("f功能評估ID");

                entity.Property(e => e.F問題)
                    .HasMaxLength(100)
                    .HasColumnName("f問題");

                entity.Property(e => e.F復健目標)
                    .HasMaxLength(100)
                    .HasColumnName("f復健目標");

                entity.Property(e => e.F復健計畫)
                    .HasMaxLength(100)
                    .HasColumnName("f復健計畫");

                entity.Property(e => e.F現狀評估)
                    .HasMaxLength(100)
                    .HasColumnName("f現狀評估");

                entity.Property(e => e.F評估項目).HasColumnName("f評估項目");

                entity.HasOne(d => d.F功能評估)
                    .WithMany(p => p.功能評估個表s)
                    .HasForeignKey(d => d.F功能評估Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_功能評估個表_功能評估");

                entity.HasOne(d => d.F評估項目Navigation)
                    .WithMany(p => p.功能評估個表s)
                    .HasForeignKey(d => d.F評估項目)
                    .HasConstraintName("FK_功能評估個表_TypeNames");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
