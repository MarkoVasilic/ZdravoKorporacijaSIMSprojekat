using System.Collections.Generic;
using Newtonsoft.Json;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Interfaces;

public interface IMeetingRepository
{
    public List<Meeting> FindAll();
    public void SaveMeeting(Meeting meetingToSave);
    public void RemoveMeeting(int meetingId);
    public Meeting? FindOneById(int meetingId);
    public void UpdateMeeting(Meeting meetingToModify);
}