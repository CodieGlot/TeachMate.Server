﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TeachMate.Domain;
public class Learner
{
    [Key]
    public Guid Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public List<LearningModule> EnrolledModules { get; set; } = new List<LearningModule>();
    [JsonIgnore]
    public AppUser AppUser { get; set; } = null!;
    public int GradeLevel { get; set; }
    public List<LearningModuleRequest> LearningModuleRequests { get; set; } = new List<LearningModuleRequest>();
    public List<LearningModulePaymentOrder> LearningModulePaymentOrders { get; set; } = new List<LearningModulePaymentOrder> { };
}


