﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(SocialNetworkDBContext))]
    partial class SocialNetworkDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataAccessLayer.Entity.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FileEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("MessageId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FileEntityId");

                    b.HasIndex("MessageId")
                        .IsUnique();

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Commentable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Commentable");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Conversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.FileEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<byte[]>("Data")
                        .HasColumnType("bytea");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.ToTable("FileEntities");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Gallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Galleries");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.GroupRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.ToTable("GroupRoles");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.JoinEntity.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("User1Id")
                        .HasColumnType("integer");

                    b.Property<int>("User2Id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("User1Id", "User2Id")
                        .IsUnique();

                    b.HasIndex("User2Id", "User1Id")
                        .IsUnique();

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.JoinEntity.ConversationParticipant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ConversationId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("UserId");

                    b.ToTable("ConversationParticipants");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.JoinEntity.EventParticipant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<int?>("ParticipationTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("ParticipationTypeId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("EventParticipants");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.JoinEntity.GroupMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<int>("GroupRoleId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("GroupRoleId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupMembers");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.JoinEntity.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("integer");

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ConversationId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ConversationId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.ParticipationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.ToTable("ParticipationTypes");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Postable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Postable");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AvatarId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Comment", b =>
                {
                    b.HasBaseType("DataAccessLayer.Entity.Commentable");

                    b.Property<int>("CommentableId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasIndex("CommentableId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Photo", b =>
                {
                    b.HasBaseType("DataAccessLayer.Entity.Commentable");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("FileEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("GalleryId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasIndex("FileEntityId");

                    b.HasIndex("GalleryId");

                    b.ToTable("Photo", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Post", b =>
                {
                    b.HasBaseType("DataAccessLayer.Entity.Commentable");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("PostableId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasIndex("PostableId");

                    b.HasIndex("UserId");

                    b.ToTable("Post", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Group", b =>
                {
                    b.HasBaseType("DataAccessLayer.Entity.Postable");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.ToTable("Group", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Profile", b =>
                {
                    b.HasBaseType("DataAccessLayer.Entity.Postable");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int?>("Sex")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profile", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Attachment", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.FileEntity", "FileEntity")
                        .WithMany()
                        .HasForeignKey("FileEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.Message", "Message")
                        .WithOne("Attachment")
                        .HasForeignKey("DataAccessLayer.Entity.Attachment", "MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FileEntity");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Conversation", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Event", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Gallery", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.Profile", "Profile")
                        .WithMany("Galleries")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.JoinEntity.Contact", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.User", "User1")
                        .WithMany("ContactsOf")
                        .HasForeignKey("User1Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.User", "User2")
                        .WithMany("Contacts")
                        .HasForeignKey("User2Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User1");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.JoinEntity.ConversationParticipant", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.Conversation", "Conversation")
                        .WithMany("ConversationParticipants")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.User", "User")
                        .WithMany("ConversationParticipants")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Conversation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.JoinEntity.EventParticipant", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.Event", "Event")
                        .WithMany("EventParticipants")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.ParticipationType", "ParticipationType")
                        .WithMany()
                        .HasForeignKey("ParticipationTypeId");

                    b.HasOne("DataAccessLayer.Entity.User", "User")
                        .WithMany("EventParticipants")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("ParticipationType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.JoinEntity.GroupMember", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.Group", "Group")
                        .WithMany("GroupMembers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.GroupRole", "GroupRole")
                        .WithMany()
                        .HasForeignKey("GroupRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.User", "User")
                        .WithMany("GroupMembers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("GroupRole");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.JoinEntity.UserRole", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Like", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Message", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Conversation");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.User", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.FileEntity", "Avatar")
                        .WithOne()
                        .HasForeignKey("DataAccessLayer.Entity.User", "AvatarId");

                    b.Navigation("Avatar");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Comment", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.Commentable", "Commentable")
                        .WithMany("Comments")
                        .HasForeignKey("CommentableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.Commentable", null)
                        .WithOne()
                        .HasForeignKey("DataAccessLayer.Entity.Comment", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Commentable");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Photo", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.FileEntity", "FileEntity")
                        .WithMany()
                        .HasForeignKey("FileEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.Gallery", "Gallery")
                        .WithMany("Photos")
                        .HasForeignKey("GalleryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.Commentable", null)
                        .WithOne()
                        .HasForeignKey("DataAccessLayer.Entity.Photo", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FileEntity");

                    b.Navigation("Gallery");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Post", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.Commentable", null)
                        .WithOne()
                        .HasForeignKey("DataAccessLayer.Entity.Post", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.Postable", "Postable")
                        .WithMany("Posts")
                        .HasForeignKey("PostableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Postable");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Group", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.Postable", null)
                        .WithOne()
                        .HasForeignKey("DataAccessLayer.Entity.Group", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Profile", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.Postable", null)
                        .WithOne()
                        .HasForeignKey("DataAccessLayer.Entity.Profile", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("DataAccessLayer.Entity.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("DataAccessLayer.Entity.Address", "Address", b1 =>
                        {
                            b1.Property<int>("ProfileId")
                                .HasColumnType("integer");

                            b1.Property<string>("City")
                                .HasMaxLength(64)
                                .HasColumnType("character varying(64)");

                            b1.Property<string>("HouseNumber")
                                .HasMaxLength(32)
                                .HasColumnType("character varying(32)");

                            b1.Property<string>("PostalCode")
                                .HasMaxLength(32)
                                .HasColumnType("character varying(32)");

                            b1.Property<string>("Region")
                                .HasMaxLength(64)
                                .HasColumnType("character varying(64)");

                            b1.Property<string>("State")
                                .HasMaxLength(64)
                                .HasColumnType("character varying(64)");

                            b1.Property<string>("Street")
                                .HasMaxLength(64)
                                .HasColumnType("character varying(64)");

                            b1.HasKey("ProfileId");

                            b1.ToTable("Profile");

                            b1.WithOwner()
                                .HasForeignKey("ProfileId");
                        });

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Commentable", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Conversation", b =>
                {
                    b.Navigation("ConversationParticipants");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Event", b =>
                {
                    b.Navigation("EventParticipants");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Gallery", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Message", b =>
                {
                    b.Navigation("Attachment");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Postable", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.User", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("ContactsOf");

                    b.Navigation("ConversationParticipants");

                    b.Navigation("EventParticipants");

                    b.Navigation("GroupMembers");

                    b.Navigation("Profile");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Group", b =>
                {
                    b.Navigation("GroupMembers");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Profile", b =>
                {
                    b.Navigation("Galleries");
                });
#pragma warning restore 612, 618
        }
    }
}
