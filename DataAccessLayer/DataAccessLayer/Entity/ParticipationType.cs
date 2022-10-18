﻿using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class ParticipationType : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(64)]
        public string Name { get; set; }
    }
}
