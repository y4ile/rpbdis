/*
 *       @Author: yaile
 */

using lab6.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace lab6.Data;

public class PlansDbContext : DbContext
{
    public PlansDbContext(DbContextOptions<PlansDbContext> options) : base(options) { }
    
    public virtual DbSet<DevelopmentDirection> DevelopmentDirections { get; set; }

    public virtual DbSet<PlanStage> PlanStages { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<StudyPlan> StudyPlans { get; set; }

    public virtual DbSet<User> Users { get; set; }
}