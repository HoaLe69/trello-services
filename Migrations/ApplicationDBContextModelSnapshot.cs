﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using trello_services.Data;

#nullable disable

namespace trello_services.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("trello_services.Entities.Activity", b =>
                {
                    b.Property<long>("activityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("activityId"));

                    b.Property<long>("cardId")
                        .HasColumnType("bigint");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("activityId");

                    b.HasIndex("cardId");

                    b.HasIndex("userId");

                    b.ToTable("activity", (string)null);
                });

            modelBuilder.Entity("trello_services.Entities.Board", b =>
                {
                    b.Property<Guid>("boardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("orderColumnIds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("star")
                        .HasColumnType("bit");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("workSpaceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("boardId");

                    b.HasIndex("workSpaceId");

                    b.ToTable("board", (string)null);
                });

            modelBuilder.Entity("trello_services.Entities.Card", b =>
                {
                    b.Property<long>("cardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("cardId"));

                    b.Property<long>("columnId")
                        .HasColumnType("bigint");

                    b.Property<string>("cover")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("endDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("startDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("cardId");

                    b.HasIndex("columnId");

                    b.ToTable("card", (string)null);
                });

            modelBuilder.Entity("trello_services.Entities.CardLabel", b =>
                {
                    b.Property<long>("cardId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("labelId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("cardId", "labelId");

                    b.HasIndex("labelId");

                    b.ToTable("card_label", (string)null);
                });

            modelBuilder.Entity("trello_services.Entities.CheckList", b =>
                {
                    b.Property<Guid>("checkListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("cardId")
                        .HasColumnType("bigint");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("checkListId");

                    b.HasIndex("cardId");

                    b.ToTable("checklist", (string)null);
                });

            modelBuilder.Entity("trello_services.Entities.CheckListDetail", b =>
                {
                    b.Property<long>("clDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("clDetailId"));

                    b.Property<Guid>("checkListId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("status")
                        .HasColumnType("bit");

                    b.HasKey("clDetailId");

                    b.HasIndex("checkListId");

                    b.ToTable("check_list_detail", (string)null);
                });

            modelBuilder.Entity("trello_services.Entities.Column", b =>
                {
                    b.Property<long>("columnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("columnId"));

                    b.Property<Guid>("boardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("orderCardIds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("columnId");

                    b.HasIndex("boardId");

                    b.ToTable("column", (string)null);
                });

            modelBuilder.Entity("trello_services.Entities.Comment", b =>
                {
                    b.Property<long>("commentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("commentId"));

                    b.Property<long>("cardId")
                        .HasColumnType("bigint");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("commentId");

                    b.HasIndex("cardId");

                    b.HasIndex("userId");

                    b.ToTable("comment", (string)null);
                });

            modelBuilder.Entity("trello_services.Entities.Label", b =>
                {
                    b.Property<Guid>("labelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("boardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("labelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("theme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("labelId");

                    b.HasIndex("boardId");

                    b.ToTable("label", (string)null);
                });

            modelBuilder.Entity("trello_services.Entities.User", b =>
                {
                    b.Property<Guid>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("avatar_path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("displayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("verify_email")
                        .HasColumnType("bit");

                    b.HasKey("userId");

                    b.HasIndex("email")
                        .IsUnique();

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("trello_services.Entities.UserBoard", b =>
                {
                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("boardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("role")
                        .HasColumnType("int");

                    b.HasKey("userId", "boardId");

                    b.HasIndex("boardId");

                    b.ToTable("user_board", (string)null);
                });

            modelBuilder.Entity("trello_services.Entities.UserCard", b =>
                {
                    b.Property<long>("cardId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("cardId", "userId");

                    b.HasIndex("userId");

                    b.ToTable("user_card", (string)null);
                });

            modelBuilder.Entity("trello_services.Entities.UserWorkspace", b =>
                {
                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("workSpaceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("role")
                        .HasColumnType("int");

                    b.HasKey("userId", "workSpaceId");

                    b.HasIndex("workSpaceId");

                    b.ToTable("user_worksapce", (string)null);
                });

            modelBuilder.Entity("trello_services.Entities.WorkSpace", b =>
                {
                    b.Property<Guid>("workSpaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("theme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("workSpaceId");

                    b.ToTable("workspace", (string)null);
                });

            modelBuilder.Entity("trello_services.Entities.Activity", b =>
                {
                    b.HasOne("trello_services.Entities.Card", "Card")
                        .WithMany("Activities")
                        .HasForeignKey("cardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("trello_services.Entities.User", "User")
                        .WithMany("Activities")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("User");
                });

            modelBuilder.Entity("trello_services.Entities.Board", b =>
                {
                    b.HasOne("trello_services.Entities.WorkSpace", "WorkSpace")
                        .WithMany("Boards")
                        .HasForeignKey("workSpaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkSpace");
                });

            modelBuilder.Entity("trello_services.Entities.Card", b =>
                {
                    b.HasOne("trello_services.Entities.Column", "Column")
                        .WithMany("Cards")
                        .HasForeignKey("columnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Column");
                });

            modelBuilder.Entity("trello_services.Entities.CardLabel", b =>
                {
                    b.HasOne("trello_services.Entities.Card", "Card")
                        .WithMany("CardLabels")
                        .HasForeignKey("cardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("trello_services.Entities.Label", "Label")
                        .WithMany("CardLabels")
                        .HasForeignKey("labelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Label");
                });

            modelBuilder.Entity("trello_services.Entities.CheckList", b =>
                {
                    b.HasOne("trello_services.Entities.Card", "Card")
                        .WithMany("CheckLists")
                        .HasForeignKey("cardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("trello_services.Entities.CheckListDetail", b =>
                {
                    b.HasOne("trello_services.Entities.CheckList", "CheckList")
                        .WithMany("CheckListDetails")
                        .HasForeignKey("checkListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckList");
                });

            modelBuilder.Entity("trello_services.Entities.Column", b =>
                {
                    b.HasOne("trello_services.Entities.Board", "Board")
                        .WithMany("Columns")
                        .HasForeignKey("boardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("trello_services.Entities.Comment", b =>
                {
                    b.HasOne("trello_services.Entities.Card", "Card")
                        .WithMany("Comments")
                        .HasForeignKey("cardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("trello_services.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("User");
                });

            modelBuilder.Entity("trello_services.Entities.Label", b =>
                {
                    b.HasOne("trello_services.Entities.Board", "Board")
                        .WithMany("Labels")
                        .HasForeignKey("boardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("trello_services.Entities.UserBoard", b =>
                {
                    b.HasOne("trello_services.Entities.Board", "Board")
                        .WithMany("UserBoards")
                        .HasForeignKey("boardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("trello_services.Entities.User", "User")
                        .WithMany("UserBoards")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("User");
                });

            modelBuilder.Entity("trello_services.Entities.UserCard", b =>
                {
                    b.HasOne("trello_services.Entities.Card", "Card")
                        .WithMany("UserCards")
                        .HasForeignKey("cardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("trello_services.Entities.User", "User")
                        .WithMany("UserCards")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("User");
                });

            modelBuilder.Entity("trello_services.Entities.UserWorkspace", b =>
                {
                    b.HasOne("trello_services.Entities.User", "User")
                        .WithMany("UserOfWorkspace")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("trello_services.Entities.WorkSpace", "WorkSpace")
                        .WithMany("UserWorkspaces")
                        .HasForeignKey("workSpaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WorkSpace");
                });

            modelBuilder.Entity("trello_services.Entities.Board", b =>
                {
                    b.Navigation("Columns");

                    b.Navigation("Labels");

                    b.Navigation("UserBoards");
                });

            modelBuilder.Entity("trello_services.Entities.Card", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("CardLabels");

                    b.Navigation("CheckLists");

                    b.Navigation("Comments");

                    b.Navigation("UserCards");
                });

            modelBuilder.Entity("trello_services.Entities.CheckList", b =>
                {
                    b.Navigation("CheckListDetails");
                });

            modelBuilder.Entity("trello_services.Entities.Column", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("trello_services.Entities.Label", b =>
                {
                    b.Navigation("CardLabels");
                });

            modelBuilder.Entity("trello_services.Entities.User", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Comments");

                    b.Navigation("UserBoards");

                    b.Navigation("UserCards");

                    b.Navigation("UserOfWorkspace");
                });

            modelBuilder.Entity("trello_services.Entities.WorkSpace", b =>
                {
                    b.Navigation("Boards");

                    b.Navigation("UserWorkspaces");
                });
#pragma warning restore 612, 618
        }
    }
}
