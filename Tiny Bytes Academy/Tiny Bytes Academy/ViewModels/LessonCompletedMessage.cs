using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Tiny_Bytes_Academy.Messages;

public class LessonCompletedMessage : ValueChangedMessage<int>
{
    public LessonCompletedMessage(int lessonId) : base(lessonId) { }
}