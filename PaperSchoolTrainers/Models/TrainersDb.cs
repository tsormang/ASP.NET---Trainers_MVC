using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PaperSchoolTrainers.Models
{
    public class TrainersDb : DbContext
    {
        public TrainersDb(): base("name = Sundesmos") { }

        public DbSet<Trainer> Trainers { get; set; }

    }
}