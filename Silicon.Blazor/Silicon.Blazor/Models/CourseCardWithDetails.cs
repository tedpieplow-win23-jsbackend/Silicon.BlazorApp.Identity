﻿using System.Diagnostics.Metrics;

namespace Silicon.Blazor.Models;

public class CourseCardWithDetails
{
    public string Id { get; set; } = null!;
    public string? ImageUri { get; set; }
    public string? ImageHeaderUri { get; set; }
    public bool IsBestseller { get; set; }
    public bool IsDigital { get; set; }
    public string? Title { get; set; }
    public string? Ingress { get; set; }
    public decimal StarRating { get; set; }
    public string? Reviews { get; set; }
    public string? LikesInPercent { get; set; }
    public string? Likes { get; set; }
    public string? Hours { get; set; }
    public List<Author>? Authors { get; set; }
    public Content? Content { get; set; }
    public Prices? Prices { get; set; }
}

public class Author
{
    public string? Name { get; set; }
}

public class Content
{
    public string? Description { get; set; }
    public List<string>? Includes { get; set; }
    public List<ProgramDetail>? ProgramDetails { get; set; }
}

public class Prices
{
    public string? Currency { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}

public class ProgramDetail
{
    public int? Id { get; set; }
    public string? Description { get; set; }
    public string? Title { get; set; }
}
