namespace Puroguramu.Domains.Lesson.Data;

public interface ILesson
{
    Guid IdLesson { get; set; }

    string Name { get; set; }

    string Description { get; set; }

    public bool Publish { get; set; }
}
