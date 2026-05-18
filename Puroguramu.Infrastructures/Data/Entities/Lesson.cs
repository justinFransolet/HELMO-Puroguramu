using Puroguramu.Domains.Lesson.Data;

namespace Puroguramu.Infrastructures.Data.Entities;

public class Lesson : ILesson
{
    public Guid IdLesson { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool Publish { get; set; } = false;
}
